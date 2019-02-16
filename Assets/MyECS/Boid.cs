using UnityEngine;
using System.Collections.Generic;

public class Boid : MonoBehaviour
{
	public Simulation simulation { get; set; }
	public Param param { get; set; }
	public Vector3 pos { get; private set; }
	public Vector3 velocity { get; private set; }
	Vector3 accel = Vector3.zero;
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

	}

}
