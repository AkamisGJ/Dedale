using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;

public class SoundManager : Singleton<SoundManager>
{
    private FMOD.Studio.Bus _bus;
    private float _volumeBusSoundDesign = 0;
    private float _volumeBusDialogue = 0;

    void Awake()
    {
        base.Awake();
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
        }
        return value;
    }

    public void MixerSoundDesign(float volume)
    {
        if (volume >= 0)
        {
            FMOD.Studio.Bus _busSoundDesign;
            _busSoundDesign = FMODUnity.RuntimeManager.GetBus("Bus:/Sound Design");
            _busSoundDesign.setVolume(volume * _volumeBusSoundDesign);
        }
    }

    public void MixerDialogue(float volume)
    {
        if (volume >= 0)
        {
            FMOD.Studio.Bus _busDialogue;
            _busDialogue = FMODUnity.RuntimeManager.GetBus("Bus:/Dialogue et voix");
            _busDialogue.setVolume(volume * _volumeBusDialogue);
            float value;
            _busDialogue.getVolume(out value);
            UnityEngine.Debug.Log(value);
        }
    }
}