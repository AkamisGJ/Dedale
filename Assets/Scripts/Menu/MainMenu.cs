using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    #region Fields
    [SerializeField] private GameObject _mainMenu = null;
    [SerializeField] private GameObject _tutorielMenu = null;
    [SerializeField] private GameObject _settings = null;
    [SerializeField] private string _selectedStage;
    #endregion Fields

    private void Start()
    {
        _mainMenu.SetActive(true);
        _tutorielMenu.SetActive(false);
        _settings.SetActive(false);
    }

    public void SelectStage()
    {
        GameManager.Instance.ChangeState(GameManager.MyState.Game, _selectedStage);
    }

    public void OnClickStart()
    {
        GameManager.Instance.ChangeState(GameManager.MyState.Game, "Level1");
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