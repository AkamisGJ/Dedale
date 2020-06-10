using UnityEngine;
using UnityEngine.UI;
using FMOD;

public class MenuPause : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu = null;
    [SerializeField] private GameObject _settings = null;
    [SerializeField] private Slider _sliderSoundDesign = null;
    [SerializeField] private Slider _sliderDialogue = null;
    [SerializeField] private Slider _sliderMusic = null;
    [SerializeField] private Slider _sliderMouseSensitivity = null;
    private Animator _animator = null;

    private void Start()
    {
        _sliderSoundDesign.value = SoundManager.Instance.InitAudioMixer("Bus:/Sound Design");
        _sliderDialogue.value = SoundManager.Instance.InitAudioMixer("Bus:/Dialogue et voix");
        _sliderMusic.value = SoundManager.Instance.InitAudioMixer("Bus:/Music");
        _sliderMouseSensitivity.value = PlayerManager.Instance.MouseSensitivityMultiplier;
        _animator = PlayerManager.Instance.PlayerController.Animator;
    }

    public void OnStart()
    {
        _pauseMenu.SetActive(true);
        _settings.SetActive(false);
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

    public void OnClickContinue()
    {
        _animator.enabled = true;
        _pauseMenu.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
