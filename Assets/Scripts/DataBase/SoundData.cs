using UnityEngine;

[CreateAssetMenu(fileName = "SoundData", menuName = "DataBase/SoundData")]
public class SoundData : ScriptableObject
{
    #region Fields
    [SerializeField] private string _soundKey = null;
    [SerializeField] private AudioClip _audioClip = null;
    #endregion Fields

    #region Properties
    public string SoundKey { get { return _soundKey; } set { _soundKey = value; } }
    public AudioClip AudioClip { get { return _audioClip; } set { _audioClip = value; } }
    #endregion Properties
}
