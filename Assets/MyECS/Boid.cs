using UnityEngine;
using System.Collections.Generic;

public class Boid : MonoBehaviour
{
	public bool itsTarget = true;
	public Simulation simulation { get; set; }
	public Param param { get; set; }
	public Vector3 pos { get; private set; }
	public Vector3 velocity { get; private set; }
	public Vector3 accel = Vector3.zero;
	List<Boid> neighbors = new List<Boid>();

	void Start(){
		pos = transform.position;
		velocity = transform.forward * param.initSpeed;
	}

	void Update(){
		UpdateNeighbors();
		UpdateWalls();
		UpdateSeparation();

		if (itsTarget){
			UpdateAlignment();
			UpdateCohesion();
		}

		UpdateMove();
	}


	/// <summary>
	/// 近隣の個体を探してneighborsリストを更新
	/// </summary>
	private void UpdateNeighbors(){
		neighbors.Clear();

		if (!simulation) return;

		var prodThresh = Mathf.Cos(param.neighborFov * Mathf.Deg2Rad);
		var distThresh = param.neighborDistance;

		foreach (var other in simulation.boids)
		{
			if (other == this) continue;

			var to = other.pos - pos;
			var dist = to.magnitude;
			if (dist < distThresh)
			{
				var dir = to.normalized;
				var fwd = velocity.normalized;
				var prod = Vector3.Dot(fwd, dir);
				if (prod > prodThresh)
				{
					neighbors.Add(other);
				}
			}
		}
	}

	/// <summary>
	/// 壁に当たりそうになったら向きを変える
	/// </summary>
	private void UpdateWalls(){
		if (!simulation) return;

		var scale = simulation.Scale * 0.5f;
		accel +=
			CalcAccelAgainstWall(-scale.x - pos.x, Vector3.right) +
			CalcAccelAgainstWall(-scale.y - pos.y, Vector3.up) +
			CalcAccelAgainstWall(-scale.z - pos.z, Vector3.forward) +
			CalcAccelAgainstWall(+scale.x - pos.x, Vector3.left) +
			CalcAccelAgainstWall(+scale.y - pos.y, Vector3.down) +
			CalcAccelAgainstWall(+scale.z - pos.z, Vector3.back);
	}

	Vector3 CalcAccelAgainstWall(float distance, Vector3 dir)
	{
		if (distance < param.wallDistance)
		{
			return dir * (param.wallWeight / Mathf.Abs(distance / param.wallDistance));
		}
		return Vector3.zero;
	}

	/// <summary>
	/// 近隣の個体から離れる
	/// </summary>
	private void UpdateSeparation(){
		if (neighbors.Count == 0) return;

		Vector3 force = Vector3.zero;
		foreach (var neighbor in neighbors)
		{
			force += (pos - neighbor.pos).normalized;
		}
		force /= neighbors.Count;

		accel += force * param.separationWeight;
	}


	/// <summary>
	/// 近隣の個体と速度を合わせる
	/// </summary>
	private void UpdateAlignment(){
		if (neighbors.Count == 0) return;

		var averageVelocity = Vector3.zero;
		foreach (var neighbor in neighbors)
		{
			averageVelocity += neighbor.velocity;
		}
		averageVelocity /= neighbors.Count;

		accel += (averageVelocity - velocity) * param.alignmentWeight;
	}

	/// <summary>
	/// 近隣の個体の中心に移動する
	/// </summary>
	private void UpdateCohesion(){
		if (neighbors.Count == 0) return;

		var averagePos = Vector3.zero;
		foreach (var neighbor in neighbors)
		{
			averagePos += neighbor.pos;
		}
		averagePos /= neighbors.Count;

		accel += (averagePos - pos) * param.cohesionWeight;
	}

	/// <summary>
	/// 上記4つの結果更新されたaccelをvelocityに反映して位置を動かす
	/// </summary>
	private void UpdateMove(){
		var dt = Time.deltaTime;

		velocity += accel * dt;
		var dir = velocity.normalized;
		var speed = velocity.magnitude;
		velocity = Mathf.Clamp(speed, param.minSpeed, param.maxSpeed) * dir;
		pos += velocity * dt;

		var rot = Quaternion.LookRotation(velocity);
		transform.SetPositionAndRotation(pos, rot);

		accel = Vector3.zero;
	}
}
