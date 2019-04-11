using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
	private CharacterController cc;
	private Animator ac;
	private Vector3 moveVec, gravity;

	public float Speed = 5;
	public float JumpSpeed = 12;

	private int laneNumber = 1,
				lanesCount = 2;

	public float	FirstLanePos,
					LaneDistance,
					SideSpeed;

	private bool isRolling = false;

	private Vector3 ccCenterNorm = new Vector3(0, 0, 0),
					ccCenterRoll = new Vector3(0, -60f, 0);

	private float	ccHeightNorm = 170f,
					ccHeightRoll = 50f;

	void Start ()
	{
		cc = GetComponent<CharacterController>();
		ac = GetComponent<Animator>();
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
					ac.SetTrigger("jumping");
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
		isRolling = true;
		ac.SetBool("rolling", true);
		cc.center = ccCenterRoll;
		cc.height = ccHeightRoll;
		
		yield return new WaitForSeconds(1.5f);
		isRolling = false;
		ac.SetBool("rolling", false);
		cc.center = ccCenterNorm;
		cc.height = ccHeightNorm;
	}
}
