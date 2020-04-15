using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaking_Object : MonoBehaviour
{
    public bool activate = false;
    [SerializeField] float maxAmplitude = 1f;
    [SerializeField] float speed = 1f;
    private float amplitude;
    Vector3 startingPos = Vector3.zero;
    Vector3 newPos = Vector3.zero;

    void Start()
    {
        startingPos = transform.position;
    }


    void Update()
    {
        if (activate)
        {
            newPos = startingPos;

            amplitude = Random.Range(0f, maxAmplitude);
            newPos.x += (Mathf.Sin(speed) * amplitude);

            amplitude = Random.Range(0f, maxAmplitude);
            newPos.y += (Mathf.Sin(speed) * amplitude);

            amplitude = Random.Range(0f, maxAmplitude);
            newPos.z += (Mathf.Sin(speed) * amplitude);

            transform.position = newPos;
        }
    }
}
