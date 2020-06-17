using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;

public class SoundManager : Singleton<SoundManager>
{
    private FMOD.Studio.Bus _bus;
    private float _volumeBusSoundDesign = 0;
    private float _volumeBusDialogue = 0;
    private float _volumeBusMusic = 0;
    private bool _wasInit = false;

    public bool WasInit { get => _wasInit; set => _wasInit = value; }

    void Awake()
    {
        base.Awake();
        _wasInit = false;
    }

    public float InitAudioMixer(string audioGroup)
    {
        float value;
        _bus = FMODUnity.RuntimeManager.GetBus(audioGroup);
        _bus.getVolume(out value);
        if (audioGroup == "Bus:/Sound Design")
        {
            _volumeBusSoundDesign = value;
        }
        else if(audioGroup == "Bus:/Dialogue et voix")
        {
            _volumeBusDialogue = value;
        }else if (audioGroup == "Bus:/Music")
        {
            _volumeBusMusic = value;
        }
        return value;
    }

    public void MixerSoundDesign(float volume)
    {
        if (volume >= 0)
        {
            FMOD.Studio.Bus _busSoundDesign;
            _busSoundDesign = FMODUnity.RuntimeManager.GetBus("Bus:/Sound Design");
            _busSoundDesign.setVolume(volume);
        }
    }

    public void MixerDialogue(float volume)
    {
        if (volume >= 0)
        {
            FMOD.Studio.Bus _busDialogue;
            _busDialogue = FMODUnity.RuntimeManager.GetBus("Bus:/Dialogue et voix");
            _busDialogue.setVolume(volume);
        }
    }

    public void MixerMusic(float volume)
    {
        if (volume >= 0)
        {
            FMOD.Studio.Bus _busMusic;
            _busMusic = FMODUnity.RuntimeManager.GetBus("Bus:/Music");
            _busMusic.setVolume(volume);
        }
    }
}