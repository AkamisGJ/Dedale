using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour
{
    [SerializeField] private Vector3 _rotationWhenLooked = Vector3.zero;
    [SerializeField] private  float _distanceWithCameraWehnLooked = 5.0f;
    [SerializeField] private AudioClip _OnTakeObject = null;
    [SerializeField] private AudioClip _OnThrowObject = null;

    public AudioClip OnThrowObject { get { return _OnThrowObject; } }
    public AudioClip OnTakeObject { get { return _OnTakeObject; } }

    public Vector4 Interact()
    {
        return (new Vector4(_rotationWhenLooked.x, _rotationWhenLooked.y, _rotationWhenLooked.z, _distanceWithCameraWehnLooked));
    }
}
