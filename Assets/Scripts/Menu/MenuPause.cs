using UnityEngine;

public class MenuPause : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu = null;
    [SerializeField] private GameObject _settings = null;

    public void OnStart()
    {
        _pauseMenu.SetActive(true);
        _settings.SetActive(false);
    }

    public void OnClickContinue()
    {
        _pauseMenu.SetActive(false);
        GameLoopManager.Instance.IsPaused = false;
    }

    public void OnClickOption()
    {
        _pauseMenu.SetActive(false);
        _settings.SetActive(true);
    }

    public void OnClickQuit()
    {
        _pauseMenu.SetActive(false);
        GameManager.Instance.ChangeState(GameManager.MyState.MAINMENU);
    }

    public void OnClickReturn()
    {
        _pauseMenu.SetActive(true);
        _settings.SetActive(false);
    }

    public void OnPressEscape()
    {
        _pauseMenu.SetActive(false);
        _settings.SetActive(false);
    }
}
