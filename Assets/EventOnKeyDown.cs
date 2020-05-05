using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class scriptkeydialogue : MonoBehaviour
{

[SerializeField] private UnityEvent _unityEvent = null;

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            _unityEvent.Invoke();
        }
    }
}
