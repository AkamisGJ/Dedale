using UnityEngine;

public class MainMenu : MonoBehaviour
{
    #region Fields
    [SerializeField] GameObject _mainMenu = null;
    [SerializeField] GameObject _tutorielMenu = null;
    [SerializeField] GameObject _settings = null;
    #endregion Fields

    private void Start()
    {
        _mainMenu.SetActive(true);
        _tutorielMenu.SetActive(false);
        _settings.SetActive(false);
    }

    public void OnClickStart()
    {
        GameManager.Instance.ChangeState(GameManager.MyState.Game);
        //SoundManager.Instance.GameStartSound();
        //SoundManager.Instance.StartLevel();
    }

    public void OnClickQuit()
    {
        SoundManager.Instance.ClickSound();
        Application.Quit();
    }

    public void OnClickTutorial()
    {
        SoundManager.Instance.ClickSound();
        _mainMenu.SetActive(false);
        _tutorielMenu.SetActive(true);
    }

    public void OnClickReturn()
    {
        SoundManager.Instance.ClickSound();
        _mainMenu.SetActive(true);
        _tutorielMenu.SetActive(false);
        _settings.SetActive(false);
    }

    public void OnClickSettings()
    {
        SoundManager.Instance.ClickSound();
        _mainMenu.SetActive(false);
        _settings.SetActive(true);
    }
}