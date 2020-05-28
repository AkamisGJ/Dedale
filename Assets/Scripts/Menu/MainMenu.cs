using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    #region Fields
    [SerializeField] private GameObject _mainMenu = null;
    [SerializeField] private GameObject _selectLevel = null;
    [SerializeField] private GameObject _tutorielMenu = null;
    [SerializeField] private GameObject _settings = null;
    #endregion Fields

    private void Start()
    {
        _mainMenu.SetActive(true);
        _tutorielMenu.SetActive(false);
        _settings.SetActive(false);
        _selectLevel.SetActive(false);
    }

    public void OnClickSelectStage()
    {
        _selectLevel.SetActive(true);
        _mainMenu.SetActive(false);
    }

    public void OnClickStart()
    {
        GameManager.Instance.ChangeState(GameManager.MyState.LOADINGSCREEN, "1_Introduction Audio");
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

    public void OnClickTutorial()
    {
        _mainMenu.SetActive(false);
        _tutorielMenu.SetActive(true);
    }

    public void OnClickReturn()
    {
        _mainMenu.SetActive(true);
        _tutorielMenu.SetActive(false);
        _settings.SetActive(false);
        _selectLevel.SetActive(false);
    }

    public void OnClickSettings()
    {
        _mainMenu.SetActive(false);
        _settings.SetActive(true);
    }

    public void OnClickLevel(string level)
    {
        GameManager.Instance.ChangeState(GameManager.MyState.LOADINGSCREEN, level);
    }
}