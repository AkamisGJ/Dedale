using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb2D = null;
    [SerializeField] private float _speed = 1;

    private void Start()
    {
        _rb2D.velocity = -_speed * transform.up;
    }
}
