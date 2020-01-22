using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : MonoBehaviour
{
    protected GameObject _noteGameObject = null;
    [SerializeField] private KeyCode keyCode = KeyCode.A;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Note"))
        {
            _noteGameObject = other.gameObject;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            OnPressedTouch();
        }
    }

    private void OnPressedTouch()
    {
        if(_noteGameObject != null)
        {
            Destroy(_noteGameObject);
            OnDestroyNote();
        }
        else
        {
            OnFailDestroy();
        }
    }

    private void OnDestroyNote()
    {
        Debug.Log("lol");
    }

    private void OnFailDestroy()
    {
        Debug.Log("Fail");
    }
}
