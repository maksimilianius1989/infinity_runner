using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
	private CharacterController cc;
	private Vector3 moveVec, gravity;

	public float Speed = 5;
	public float JumpSpeed = 12;

	private int laneNumber = 1,
				lanesCount = 2;

	public float	FirstLanePos,
					LaneDistance,
					SideSpeed;

	private bool isRolling = false;

	void Start ()
	{
		cc = GetComponent<CharacterController>();
		moveVec = new Vector3(1, 0, 0);
		gravity = Vector3.zero;
	}

	void Update ()
	{
		if (cc.isGrounded)
		{
			gravity = Vector3.zero;

			if (!isRolling)
			{
				if (Input.GetAxisRaw("Vertical") > 0)
				{
					gravity.y = JumpSpeed;
				}
				else if (Input.GetAxisRaw("Vertical") < 0)
				{
					StartCoroutine(DoRoll());
				}
			}
		}
		else
		{
			gravity += Physics.gravity * Time.deltaTime * 3;
		}
		
		moveVec.x = Speed;
		moveVec += gravity;
		moveVec *= Time.deltaTime;

		ChackInput();

		Vector3 newPos = transform.position;
		newPos.z = Mathf.Lerp(newPos.z, FirstLanePos + (laneNumber * LaneDistance), Time.deltaTime * SideSpeed);
		transform.position = newPos;
		
		cc.Move(moveVec);
	}

	void ChackInput()
	{
		int sign = 0;

		if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
		{
			sign = -1;
		}
		else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
		{
			sign = 1;
		}
		else
			return;
		
		laneNumber += sign;
		laneNumber = Mathf.Clamp(laneNumber, 0, lanesCount);
	}

	IEnumerator DoRoll()
	{
		Debug.Log("rolling");
		isRolling = true;
		
		yield return new WaitForSeconds(1.5f);
		
		isRolling = false;
		Debug.Log("not rolling");
	}
}
