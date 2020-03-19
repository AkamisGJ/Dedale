using UnityEngine;
using FMODUnity;

[CreateAssetMenu(fileName = "DialogueData", menuName = "DataBase/DIalogueData")]
public class DialogueData : ScriptableObject
{
    #region Fields
    [SerializeField] private string[] _dialogue = null;
    [EventRef]
    [SerializeField] private string _event = "";
    #endregion Fields

    #region Properties
    public string[] Dialogue { get { return _dialogue; } set { _dialogue = value; } }
    public string AudioClip { get { return _event; } set { _event = value; } }
    #endregion Properties
}
