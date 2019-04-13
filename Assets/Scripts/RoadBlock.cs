using System.Collections.Generic;
using UnityEngine;

public class RoadBlock : MonoBehaviour
{
	private GameManager GM;
	private Vector3 moveVec;

	public GameObject CoinsObj;

	public int CoinChance;
	private bool coinsSpawn;
	private bool powerUpSpawn;

	public List<GameObject> PowerUps;

	// Use this for initialization
	void Start ()
	{
		PowerUpController.CoinsPowerUpEvent += CoinsEvent;
		
		GM = FindObjectOfType<GameManager>();
		moveVec = new Vector3(-1, 0, 0);

		coinsSpawn = Random.Range(0, 101) <= CoinChance;
		CoinsObj.SetActive(coinsSpawn);
		
		powerUpSpawn = Random.Range(0, 101) <= 10 && !coinsSpawn;
		if (powerUpSpawn)
			PowerUps[Random.Range(0, PowerUps.Count)].SetActive(true);
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
		if (GM.CanPlay)
			transform.Translate(moveVec * Time.deltaTime * GM.CurrentMoveSpeed);
	}

	void CoinsEvent(bool active)
	{
		if (active)
		{
			CoinsObj.SetActive(true);
			return;
		}

		if (!coinsSpawn)
			CoinsObj.SetActive(false);
	}

	private void OnDestroy()
	{
		PowerUpController.CoinsPowerUpEvent -= CoinsEvent;
	}
}
