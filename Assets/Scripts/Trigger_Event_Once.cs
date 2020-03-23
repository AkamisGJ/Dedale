using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers;

public class Trigger_Event_Once : TriggerEvent
{
    protected override void OnTriggerEnter(Collider other)
        {
            if (!IsInTagMask(other.tag)) return;
            onTriggerEnter.Invoke(other.gameObject);
            Destroy(this);
        }
}
