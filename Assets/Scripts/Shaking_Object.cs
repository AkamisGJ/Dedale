using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaking_Object : MonoBehaviour
{
    public bool activate = false;
    [SerializeField] public float maxAmplitude = 1f;
    [SerializeField] public float speed = 1f;
    public float amplitude;
    public Vector3 startingPos = Vector3.zero;
    public Vector3 newPos = Vector3.zero;

    void Awake()
    {
        startingPos = transform.position;
    }
}
