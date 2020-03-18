using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTraveller : MonoBehaviour
{
    private Vector3 _previousOffsetFromPortal = Vector3.zero;

    public Vector3 PreviousOffsetFromPortal { get => _previousOffsetFromPortal; set => _previousOffsetFromPortal = value; }

    public virtual void Teleport(Transform fromPortal, Transform toPortal, Vector3 pos, Quaternion rot)
    {
        transform.position = pos;
        transform.rotation = rot;
    }

    public virtual void EnterPortalThreshold() { }
    public virtual void ExitPortalThreshold() { }
}
