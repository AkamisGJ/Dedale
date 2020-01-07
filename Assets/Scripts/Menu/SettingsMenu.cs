using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    private AudioMixer _audioMixer = null;
    [SerializeField] private Slider _masterSlider = null;
    [SerializeField] private Slider _effectSlider = null;
    [SerializeField] private Slider _musicSlider = null;

    void Start()
    {
        _audioMixer = SoundManager.Instance.AudioMixer;
        _masterSlider.value = SoundManager.Instance.InitAudioMixer("VolumeMaster");
        _effectSlider.value = SoundManager.Instance.InitAudioMixer("VolumeEffect");
        _musicSlider.value = SoundManager.Instance.InitAudioMixer("VolumeAmbient");
    }

    public void MasterChange()
    {
        SoundManager.Instance.MixerMaster(_masterSlider.value);
    }

    public void EffectChange()
    {
        SoundManager.Instance.MixerEffect(_effectSlider.value);
    }

    public void MusicChange()
    {
        SoundManager.Instance.MixerAmbient(_musicSlider.value);
    }
}
