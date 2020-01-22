using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NoteData", menuName = "DataBase/NoteData")]
public class NoteDataBase : ScriptableObject
{
    [SerializeField] private int _startLine = 0;
    [SerializeField] private float _timeToAppear = 1;
    [SerializeField] private bool _canSwitchLine = false;
    [SerializeField] private int _LineToSwitch = 1;
    [SerializeField] private float _timeToSwitch = 1;

    public int StartLine { get => _startLine; }
    public float TimeToAppear { get => _timeToAppear; }
    public bool CanSwitchLine { get => _canSwitchLine; }
    public int LineToSwitch { get => _LineToSwitch; }
    public float TimeToSwitch { get => _timeToSwitch; }
}
