using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollMover : MonoBehaviour
{
	public Vector3 rotation;
	public Space space;

	void Update()
	{
		float d = Time.deltaTime;
		transform.Rotate(rotation * d, space);
	}
}