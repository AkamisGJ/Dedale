using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    #region Fields
    [SerializeField] private GameObject _mainMenu = null;
    [SerializeField] private GameObject _selectLevel = null;
    [SerializeField] private GameObject _tutorielMenu = null;
    [SerializeField] private GameObject _settings = null;
    [SerializeField] private Slider _sliderSoundDesign = null;
    [SerializeField] private Slider _sliderMusic = null;
    [SerializeField] private Slider _sliderDialogue = null;
    [SerializeField] private Slider _sliderMouseSensitivity = null;
    #endregion Fields

    private void Start()
    {
        _mainMenu.SetActive(true);
        _tutorielMenu.SetActive(false);
        _settings.SetActive(false);
        _selectLevel.SetActive(false);
        _sliderSoundDesign.value = SoundManager.Instance.InitAudioMixer("Bus:/Sound Design");
        _sliderDialogue.value = SoundManager.Instance.InitAudioMixer("Bus:/Dialogue et voix");
        _sliderMusic.value = SoundManager.Instance.InitAudioMixer("Bus:/Music");
        _sliderMouseSensitivity.value = PlayerManager.Instance.MouseSensitivityMultiplier;
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

    public void OnChangeVolumeDialogue()
    {
        SoundManager.Instance.MixerDialogue(_sliderDialogue.value);
    }

    public void OnChangeVolumeSoundDesign()
    {
        SoundManager.Instance.MixerSoundDesign(_sliderSoundDesign.value);
    }

    public void OnChangeVolumeMusic()
    {
        SoundManager.Instance.MixerMusic(_sliderMusic.value);
    }

    public void OnChangeMouseSensitivity()
    {
        PlayerManager.Instance.MouseSensitivityMultiplier = _sliderMouseSensitivity.value;
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

    public void LoadLevel(string level){
        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }
}