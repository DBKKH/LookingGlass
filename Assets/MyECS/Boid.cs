using UnityEngine;
using System.Collections.Generic;

public class Boid : MonoBehaviour
{
	public bool itsTarget;
	public Simulation simulation { get; set; }
	public Param param { get; set; }
	public Vector3 pos { get; private set; }
	public Vector3 velocity { get; private set; }
	public Vector3 accel = Vector3.zero;
	List<Boid> neighbors = new List<Boid>();

	void Start()
	{
		pos = transform.position;
		velocity = transform.forward * param.initSpeed;
	}

	void Update()
	{
		UpdateNeighbors();

		UpdateWalls();

		UpdateSeparation();

		UpdateAlignment();

		UpdateCohesion();

		UpdateMove();
	}


	/// <summary>
	/// 近隣の個体を探してneighborsリストを更新
	/// </summary>
	private void UpdateNeighbors(){

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

	}

	/// <summary>
	/// 近隣の個体と速度を合わせる
	/// </summary>
	private void UpdateAlignment(){

	}

	/// <summary>
	/// 近隣の個体の中心に移動する
	/// </summary>
	private void UpdateCohesion(){

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
