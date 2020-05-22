using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers;

public class Trigger_Event_Once : TriggerEvent
{
    [SerializeField] private bool _debug = false;
    protected override void OnTriggerEnter(Collider other)
        {
            if (!IsInTagMask(other.tag)) return;
            onTriggerEnter.Invoke(other.gameObject);
            if (_debug) Debug.Log("Trigger event " + gameObject.name);
            Destroy(this);
        }
}
