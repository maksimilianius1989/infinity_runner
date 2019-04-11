using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject ResultObj;
    public PlayerMovement PM;
    public RoadSpawner RS;

    public Text PointsTxt;
    private float Points;

    public void StartGame()
    {
        ResultObj.SetActive(false);
        RS.StartGame();
        PM.CanPlay = true;
        PM.ac.SetTrigger("respawn");

        Points = 0;
    }

    private void Update()
    {
        if (PM.CanPlay)
            Points += Time.deltaTime * 3;

        PointsTxt.text = ((int) Points).ToString();
    }

    public void ShowResult()
    {
        ResultObj.SetActive(true);
    }
}
