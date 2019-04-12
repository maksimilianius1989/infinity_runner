using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public Animator ac;

	public GameManager GM;
	private CapsuleCollider selfCollider;
	private Rigidbody rb;

	public float JumpSpeed = 12;

	private int laneNumber = 1,
				lanesCount = 2;

	public float	FirstLanePos,
					LaneDistance,
					SideSpeed;

	private bool isRolling = false;

	private Vector3 ccCenterNorm = new Vector3(0, 0, 0),
					ccCenterRoll = new Vector3(0, -40, 50);

	private float	ccHeightNorm = 170f,
					ccHeightRoll = 40f;

	private bool wannaJump = false;

	private Vector3 startPosition;

	void Start ()
	{
		selfCollider = GetComponent<CapsuleCollider>();
		rb = GetComponent<Rigidbody>();

		startPosition = transform.position;
	}

	private void FixedUpdate()
	{
		rb.AddForce(new Vector3(0, Physics.gravity.y * 4, 0), ForceMode.Acceleration);

		if (wannaJump && isGrounded())
		{
			ac.SetTrigger("jumping");
			rb.AddForce(new Vector3(0, JumpSpeed, 0), ForceMode.Impulse);
			wannaJump = false;
		}
	}

	void Update ()
	{
		if (isGrounded())
		{
			if (GM.CanPlay)
			{
				if (!isRolling)
				{
					if (Input.GetAxisRaw("Vertical") > 0)
						wannaJump = true;
					else if (Input.GetAxisRaw("Vertical") < 0)
						StartCoroutine(DoRoll());
				}
			}
		}
		else if (rb.velocity.y < -2)
		{
			ac.SetTrigger("falling");
		}
		
		CheckInput();

		Vector3 newPos = transform.position;
		newPos.z = Mathf.Lerp(newPos.z, FirstLanePos + (laneNumber * LaneDistance), Time.deltaTime * SideSpeed);
		transform.position = newPos;
	}

	bool isGrounded()
	{
		return Physics.Raycast(transform.position, Vector3.down, 1.02f);
	}

	void CheckInput()
	{
		int sign = 0;
		
		if (!GM.CanPlay || isRolling)
		{
			return;
		}

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
		selfCollider.center = ccCenterRoll;
		selfCollider.height = ccHeightRoll;
		
		yield return new WaitForSeconds(1.5f);
		
		ac.SetBool("rolling", false);
		selfCollider.center = ccCenterNorm;
		selfCollider.height = ccHeightNorm;
		
		yield return new WaitForSeconds(0.3f);
		
		isRolling = false;
	}

	private void OnCollisionEnter(Collision other)
	{
		if (!other.gameObject.CompareTag("Trap") &&
		     !other.gameObject.CompareTag("DeathPlane") ||
		    !GM.CanPlay)
			return;
		
		StartCoroutine(Death());
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!other.CompareTag("Coin"))
			return;
		
		GM.AddCoins(1);
		Destroy(other.gameObject);
	}

	IEnumerator Death()
	{
		GM.CanPlay = false;
		
		ac.SetTrigger("death");
		
		yield return new WaitForSeconds(2);
		
		ac.SetTrigger("respawn");
		GM.ShowResult();
	}

	public void ResetPosition()
	{
		transform.position = startPosition;
		laneNumber = 1;
	}
}
