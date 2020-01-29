using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QTEData", menuName = "DataBase/QTEData")]
public class QTEDataBase : ScriptableObject
{
    [SerializeField] private bool _spamQTE = false;
    [SerializeField] private KeyCode _keyCode = KeyCode.A;
    [SerializeField] private float _timeToAppear = 1;
    [SerializeField] private float _timeToMake = 1;
    [SerializeField] private float _maxGauge = 10;
    [SerializeField] private float _gainGaugePerTouch = 1;

    public float TimeToAppear { get => _timeToAppear; }
    public float TimeToMake { get => _timeToMake; }
    public float MaxGauge { get => _maxGauge; }
    public float GainGaugePerTouch { get => _gainGaugePerTouch; }
    public bool SpamQTE { get => _spamQTE; }
    public KeyCode KeyCode { get => _keyCode; }
}
