using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private CharacterController cc;
	private Vector3 moveVec, gravity;

	private float speed = 5, jumpSpeed = 12;

	private int laneNumber = 1,
				lanesCount = 2;

	public float	FirstLanePos,
					LaneDistance,
					SideSpeed;

	private bool didChangeLastFrame = false;

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

			if (Input.GetAxisRaw("Vertical") > 0)
			{
				gravity.y = jumpSpeed;
			}
		}
		else
		{
			gravity += Physics.gravity * Time.deltaTime * 3;
		}
		
		moveVec.x = speed;
		moveVec += gravity;
		moveVec *= Time.deltaTime;

		float input = Input.GetAxis("Horizontal");

		if (Mathf.Abs(input) > .1f)
		{
			if (!didChangeLastFrame)
			{
				didChangeLastFrame = true;
				laneNumber += (int) Mathf.Sign(input);
				laneNumber = Mathf.Clamp(laneNumber, 0, lanesCount);
			}

		}
		else
		{
			didChangeLastFrame = false;
		}

		Vector3 newPos = transform.position;
		newPos.z = Mathf.Lerp(newPos.z, FirstLanePos + (laneNumber * LaneDistance), Time.deltaTime * SideSpeed);
		transform.position = newPos;
		
		cc.Move(moveVec);
	}
}
