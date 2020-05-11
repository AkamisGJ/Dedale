using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate_Ridgibody : MonoBehaviour
{
    public float _mass = 1f;
    public float _drag = 0f;
    public float _angulardrag = 0.05f;

    public float _explosionForce = 1f;
    public float _explosionRadius = 1f;
    public GameObject _explosionPoint;

    public void ActivateAllChildren(){
        print("Triggers");
        Rigidbody[] rigidbodies = transform.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody lego in rigidbodies)
        {
            lego.mass = _mass;
            lego.drag = _drag;
            lego.angularDrag = _angulardrag;
            lego.isKinematic = false;
            lego.useGravity = true;

            lego.AddExplosionForce(_explosionForce, _explosionPoint.transform.position, _explosionRadius);
        }
    }
}
