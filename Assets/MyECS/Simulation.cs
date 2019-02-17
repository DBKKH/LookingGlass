using UnityEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public class Simulation : MonoBehaviour
{
	[SerializeField] Color gizmoColor = Color.blue;

	[SerializeField] int boidCount = 100;

	[SerializeField] GameObject boidPrefab;

	[SerializeField] Param param;

	[SerializeField] public Vector3 Sccale = new Vector3(10, 10, 10);
	
	List<Boid> boids_ = new List<Boid>();
	public ReadOnlyCollection<Boid> boids
	{
		get { return boids_.AsReadOnly(); }
	}

	/// <summary>
	/// Create boid.prefab
	/// </summary>
	void AddBoid()
	{
		var go = Instantiate(boidPrefab, Random., Random.rotation);
		go.transform.SetParent(transform);
		var boid = go.GetComponent<Boid>();
		boid.simulation = this;
		boid.param = param;
		boids_.Add(boid);
	}

	/// <summary>
	/// 
	/// </summary>
	void RemoveBoid()
	{
		if (boids_.Count == 0) return;

		var lastIndex = boids_.Count - 1;
		var boid = boids_[lastIndex];
		Destroy(boid.gameObject);
		boids_.RemoveAt(lastIndex);
	}

	void Update()
	{
		while (boids_.Count < boidCount)
		{
			AddBoid();
		}
		while (boids_.Count > boidCount)
		{
			RemoveBoid();
		}
	}

	void OnDrawGizmos()
	{
		if (!param) return;
		Gizmos.color = gizmoColor;
		Gizmos.DrawWireCube(this.transform.position, this.Sccale);
	}
}