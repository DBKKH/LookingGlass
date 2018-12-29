using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontRollMover : MonoBehaviour
{
	public Vector3 rotation;
	public Space space;

	void Update()
	{
		float d = Time.deltaTime;
		transform.Rotate(rotation * d, space);
	}
}

