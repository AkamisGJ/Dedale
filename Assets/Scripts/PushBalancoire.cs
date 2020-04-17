﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBalancoire : MonoBehaviour
{
    [SerializeField] private float _force = 1;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Vector3 dir = new Vector3(collision.contacts[0].point.x, transform.position.y, collision.contacts[0].point.z) - transform.position;
            dir = -dir.normalized;
            GetComponent<Rigidbody>().AddForce(dir * _force);
        }
    }
}
