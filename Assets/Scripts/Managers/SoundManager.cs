using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;

public class SoundManager : Singleton<SoundManager>
{
    private FMOD.Studio.Bus _bus;

    void Awake()
    {
        base.Awake();
    }

    public float InitAudioMixer(string audioGroup)
    {
        float value;
        _bus = FMODUnity.RuntimeManager.GetBus(audioGroup);
        _bus.getVolume(out value);
        return Mathf.Pow(2f, value / 20.0f); ;
    }

    public void MixerSoundDesign(float _volume)
    {
        if (_volume > 0)
        {
            FMOD.Studio.Bus _busSoundDesign;
            _busSoundDesign = FMODUnity.RuntimeManager.GetBus("Sound Design");
            _busSoundDesign.setVolume(Mathf.Log(_volume, 2) * 20);
        }
    }

    public void MixerDialogue(float _volume)
    {
        if (_volume > 0)
        {
            FMOD.Studio.Bus _busDialogue;
            _busDialogue = FMODUnity.RuntimeManager.GetBus("Dialogue et voix");
            _busDialogue.setVolume(Mathf.Log(_volume, 2) * 20);
        }
    }
}