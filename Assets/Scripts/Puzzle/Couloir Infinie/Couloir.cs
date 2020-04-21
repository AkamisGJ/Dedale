using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Couloir : MonoBehaviour
{
    [SerializeField] private GameObject _positionFront = null;
    [SerializeField] private GameObject _positionBack = null;
    [SerializeField] private GameObject _triggerBack = null;
    [SerializeField] private GameObject _triggerFront = null;
    [SerializeField] private GameObject _frontFin = null;

    public GameObject PositionFront { get => _positionFront; }
    public GameObject PositionBack { get => _positionBack; }
    public GameObject TriggerBack { get => _triggerBack; }
    public GameObject TriggerFront { get => _triggerFront; }
    public GameObject FrontFin { get => _frontFin; }
}