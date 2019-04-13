using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
	public struct PowerUp
	{
		public enum Type
		{
			MULTIPLIER,
			IMMORTALITY,
			COINS_SPAWN
		}

		public Type PowerUpType;
		public float Duration;
	}

	public delegate void OnCoinsPowerUp(bool activate);

	public static event OnCoinsPowerUp CoinsPowerUpEvent;
	
	PowerUp[] powerUps = new PowerUp[3];
	Coroutine[] powerUpsCors = new Coroutine[3];

	public GameManager GM;
	public PlayerMovement PM;
	
	void Start ()
	{
		powerUps[0] = new PowerUp() { PowerUpType = PowerUp.Type.MULTIPLIER, Duration = 8 };
		powerUps[1] = new PowerUp() { PowerUpType = PowerUp.Type.IMMORTALITY, Duration = 5 };
		powerUps[2] = new PowerUp() { PowerUpType = PowerUp.Type.COINS_SPAWN, Duration = 7 };
		
		PlayerMovement.PowerUpUseEvent += PowerUpUse;
	}

	void PowerUpUse(PowerUp.Type type)
	{
		PowerUpReset(type);
		powerUpsCors[(int) type] = StartCoroutine(PowerUpCor(type));

		switch (type)
		{
			case PowerUp.Type.MULTIPLIER:
				GM.PowerUpMultiplier = 2;
				break;
			case PowerUp.Type.IMMORTALITY:
				PM.ImmortalityOn();
				break;
			case PowerUp.Type.COINS_SPAWN:
				if (CoinsPowerUpEvent != null)
					CoinsPowerUpEvent(true);
				break;
		}
	}
	
	void PowerUpReset(PowerUp.Type type)
	{
		if (powerUpsCors[(int) type] != null)
			StopCoroutine(powerUpsCors[(int) type]);
		else
			return;

		powerUpsCors[(int) type] = null;
		
		switch (type)
		{
			case PowerUp.Type.MULTIPLIER:
				GM.PowerUpMultiplier = 1;
				break;
			case PowerUp.Type.IMMORTALITY:
				PM.immortalityOff();
				break;
			case PowerUp.Type.COINS_SPAWN:
				if (CoinsPowerUpEvent != null)
					CoinsPowerUpEvent(false);
				break;
		}
	}

	public void ResetAllPowerUps()
	{
		for (int i = 0; i < powerUps.Length; i++)
			PowerUpReset(powerUps[i].PowerUpType);
	}
	
	IEnumerator PowerUpCor(PowerUp.Type type)
	{
		float duration = powerUps[(int) type].Duration;

		while (duration > 0)
		{
			duration -= Time.deltaTime;
			yield return null;
		}
		
		PowerUpReset(type);
	}
}
