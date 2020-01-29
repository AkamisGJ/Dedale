using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager>
{
    #region Fields
    [SerializeField] private AudioSource _audioSourceAmbient = null;
    [SerializeField] private AudioSource _audioSourceEffect = null;
    [SerializeField] private AudioMixer _audioMixer = null;
    public AudioMixer AudioMixer { get { return _audioMixer; } }
    #endregion Fields

    protected override void Awake()
    {
        base.Awake();
    }

    public float InitAudioMixer(string audioGroup)
    {
        float value;
        _audioMixer.GetFloat(audioGroup, out value);
        return Mathf.Pow(2f, value / 20.0f); ;
    }
    
    public void GameStartSound()
    {
        _audioSourceAmbient.clip = SoundDataBase.Instance.SelectAudioClip("Game");
        _audioSourceAmbient.Play();
    }

    public void MainMenuStart()
    {
        _audioSourceAmbient.clip = SoundDataBase.Instance.SelectAudioClip("Main_Menu");
        _audioSourceAmbient.Play();
    }

    public void ClickSound()
    {
        _audioSourceEffect.clip = SoundDataBase.Instance.SelectAudioClip("ClickMenu");
        _audioSourceEffect.Play();
    }

    public void StartLevel()
    {
        _audioSourceEffect.clip = SoundDataBase.Instance.SelectAudioClip("Launch_Level");
        _audioSourceEffect.Play();
    }

    public void BurningSound()
    {
        _audioSourceEffect.PlayOneShot(SoundDataBase.Instance.SelectAudioClip("Burning"));
    }

    public void MixerMaster(float _volume)
    {
        if(_volume > 0)
            _audioMixer.SetFloat("VolumeMaster", Mathf.Log(_volume,2) * 20);
    }

    public void MixerAmbient(float _volume)
    {
        if (_volume > 0)
            _audioMixer.SetFloat("VolumeAmbient", Mathf.Log(_volume,2) * 20);
    }

    public void MixerEffect(float _volume)
    {
        if (_volume > 0)
            _audioMixer.SetFloat("VolumeEffect", Mathf.Log(_volume,2) * 20);
    }
}
