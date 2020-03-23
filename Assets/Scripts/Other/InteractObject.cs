﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighlightingSystem;

public class InteractObject : MonoBehaviour
{
    [SerializeField] private Vector3 _rotationWhenLooked = Vector3.zero;
    [SerializeField] private  float _distanceWithCameraWehnLooked = 0.5f;
    [SerializeField] private AudioClip _OnTakeObject = null;
    [SerializeField] private AudioClip _OnThrowObject = null;
    [SerializeField] private bool _isKey = false;
    public AudioClip OnThrowObject { get { return _OnThrowObject; } }
    public AudioClip OnTakeObject { get { return _OnTakeObject; } }
    public bool IsKey { get => _isKey; }

    public Vector4 Interact()
    {
        return (new Vector4(_rotationWhenLooked.x, _rotationWhenLooked.y, _rotationWhenLooked.z, _distanceWithCameraWehnLooked));
    }
}
