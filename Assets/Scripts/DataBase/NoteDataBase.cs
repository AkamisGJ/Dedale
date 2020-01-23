using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NoteData", menuName = "DataBase/NoteData")]
public class NoteDataBase : ScriptableObject
{
    [SerializeField] private int _startLine = 0;
    [SerializeField] private float _timeToAppear = 1;
    [SerializeField] private int _LineToSwitch = 1;

    public int StartLine { get => _startLine; }
    public float TimeToAppear { get => _timeToAppear; }
    public int LineToSwitch { get => _LineToSwitch; }
}
