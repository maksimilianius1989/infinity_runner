using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject ResultObj;
    public PlayerMovement PM;
    public RoadSpawner RS;

    public Text PointsTxt,
                CoinsTxt;
    private float Points;

    public int Coins = 0;

    public bool CanPlay = true;

    public float MoveSpeed;
    public float PointsBaseValue, PointsMultiplier;

    public void StartGame()
    {
        ResultObj.SetActive(false);
        RS.StartGame();
        CanPlay = true;
        PM.ac.SetTrigger("respawn");

        Points = 0;
    }

    private void Update()
    {
        if (CanPlay)
        {
            Points += PointsBaseValue * PointsMultiplier *  Time.deltaTime * 3;
            PointsMultiplier += .05f * Time.deltaTime;
            PointsMultiplier = Mathf.Clamp(PointsMultiplier, 1, 10);

            MoveSpeed += 0.1f * Time.deltaTime;
            MoveSpeed = Mathf.Clamp(MoveSpeed, 1, 20);
        }

        PointsTxt.text = ((int) Points).ToString();
    }

    public void ShowResult()
    {
        ResultObj.SetActive(true);
    }

    public void AddCoins(int number)
    {
        Coins += number;
        CoinsTxt.text = Coins.ToString();
    }
}
