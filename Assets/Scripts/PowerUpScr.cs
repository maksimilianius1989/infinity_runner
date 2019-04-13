using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PowerUpScr : MonoBehaviour
{
    public Image ProgressBar;
    public Color[] Colors;

    public void SetData(PowerUpController.PowerUp.Type type)
    {
        ProgressBar.color = Colors[(int)type];
    }

    public void SetProgress(float progress)
    {
        ProgressBar.fillAmount = progress;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
