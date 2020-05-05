using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventOnKeyDown : MonoBehaviour
{

public KeyCode m_key;
[SerializeField] private UnityEvent _unityEvent = null;

    void Update()
    {
        if (Input.GetKeyDown(m_key))
        {
            _unityEvent.Invoke();
        }
    }
}
