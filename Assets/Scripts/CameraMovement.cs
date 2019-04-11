using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	public Transform Target;

	private Vector3 startDistance, moveVec;
	

	// Use this for initialization
	void Start ()
	{
		startDistance = transform.position - Target.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		moveVec = Target.position + startDistance;

		moveVec.z = 0;
		moveVec.y = startDistance.y;
		
		transform.position = moveVec;
	}
}
