using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDataBase : Singleton<SoundDataBase>
{
    #region Fields
    private Dictionary<string, AudioClip> _audioClips = null;
    [SerializeField] private List<SoundData> _scriptableObjectsSound = null;
    #endregion Fields


    protected override void Awake()
    {
        base.Awake();
        _audioClips = new Dictionary<string, AudioClip>();
        for (int i = 0; i < _scriptableObjectsSound.Count ; i++)
        {
            _audioClips.Add(_scriptableObjectsSound[i].SoundKey, _scriptableObjectsSound[i].AudioClip);
        }
    }

    public AudioClip SelectAudioClip(string audioClipString)
    {
        return _audioClips[audioClipString];
    }
}
