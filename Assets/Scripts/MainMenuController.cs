using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameManager GM;
    
    public Sprite SoundsOn, SoundsOff;
    public Image SoundsBtnImg;
    
    public void PlayBtn()
    {
        gameObject.SetActive(false);
        GM.StartGame();
    }

    public void OpenMenu()
    {
        gameObject.SetActive(true);
    }

    public void CloseBtn()
    {
        Application.Quit();
    }

    public void SoundBtn()
    {
        GM.IsSound = !GM.IsSound;
        SoundsBtnImg.sprite = GM.IsSound ? SoundsOn : SoundsOff;
        AudioManager.Instance.RefreshSoundState();
    }
}
