using System.Collections;
using System.Collections.Generic;
using Prof.Utils;
using UnityEngine;

public class SoundDataBase : Singleton<SoundDataBase>
{
    #region Fields
    private Dictionary<string, AudioClip> _audioClips = null;
    [SerializeField] private List<SoundData> _scriptableObjectsSound = null;
    #endregion Fields


    protected override void Awake()
    {
        _audioClips = new Dictionary<string, AudioClip>();
        for (int i = 0; i < _scriptableObjectsSound.Count ; i++)
        {
            _audioClips.Add(_scriptableObjectsSound[i].SoundKey, _scriptableObjectsSound[i].AudioClip);
        }
    }

    protected override void Start()
    {
        base.Start();
    }

    public AudioClip SelectAudioClip(string audioClipString)
    {
        return _audioClips[audioClipString];
    }
}
