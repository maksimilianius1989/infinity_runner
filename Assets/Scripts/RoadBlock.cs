using UnityEngine;

public class RoadBlock : MonoBehaviour
{
	private GameManager GM;
	private Vector3 moveVec;

	public GameObject CoinsObj;

	public int CoinChance;

	// Use this for initialization
	void Start () {
		GM = FindObjectOfType<GameManager>();
		moveVec = new Vector3(-1, 0, 0);
		
		CoinsObj.SetActive(Random.Range(0, 101) <= CoinChance);
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
		if (GM.CanPlay)
			transform.Translate(moveVec * Time.deltaTime * GM.CurrentMoveSpeed);
	}
}
