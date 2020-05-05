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
        GameManager.Instance.ChangeState(GameManager.MyState.GAME, _selectedStage);
    }

    public void OnClickStart()
    {
        GameManager.Instance.ChangeState(GameManager.MyState.LOADINGSCREEN);
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
    }

    public void OnClickSettings()
    {
        _mainMenu.SetActive(false);
        _settings.SetActive(true);
    }
}