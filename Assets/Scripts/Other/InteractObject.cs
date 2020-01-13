using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour
{
    [SerializeField] Vector3 _rotationWhenLooked = Vector3.zero;
    [SerializeField] float _distanceWithCameraWehnLooked = 5.0f;

    public Vector4 Interact()
    {
        return (new Vector4(_rotationWhenLooked.x, _rotationWhenLooked.y, _rotationWhenLooked.z, _distanceWithCameraWehnLooked));
    }
}
