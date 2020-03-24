using UnityEngine;
using FMODUnity;

[CreateAssetMenu(fileName = "DialogueData", menuName = "DataBase/DIalogueData")]
public class DialogueData : ScriptableObject
{
    #region Fields
    [SerializeField] private string[] _dialogue = null;
    #endregion Fields

    #region Properties
    public string[] Dialogue { get { return _dialogue; } set { _dialogue = value; } }
    #endregion Properties
}
