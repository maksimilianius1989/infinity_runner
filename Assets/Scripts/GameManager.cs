using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject ResultObj;
    public PlayerMovement PM;
    public RoadSpawner RS;

    public void StartGame()
    {
        ResultObj.SetActive(false);
        RS.StartGame();
        PM.CanPlay = true;
    }

    public void ShowResult()
    {
        ResultObj.SetActive(true);
    }
}
