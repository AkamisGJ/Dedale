using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class machine_a_laver_Manager : MonoBehaviour
{
    Shaking_Object[] machines_a_laver = null;
    hublot[] hublots = null;
    tambour[] tambours = null;

    public bool activate_hublot = false;


    void Start()
    {
        machines_a_laver = GameObject.FindObjectsOfType<Shaking_Object>();
        hublots = GameObject.FindObjectsOfType<hublot>();
        tambours = GameObject.FindObjectsOfType<tambour>();
    }


    void Update()
    {
        ShakeObjects();
        UseHublots();
    }

    void ShakeObjects(){
        foreach (Shaking_Object item in machines_a_laver)
        {
            if (item.activate)
            {
                item.newPos = item.startingPos;

                item.amplitude = Random.Range(0f, item.maxAmplitude);
                item.newPos.x += (Mathf.Sin(item.speed) * item.amplitude);

                item.amplitude = Random.Range(0f, item.maxAmplitude);
                item.newPos.y += (Mathf.Sin(item.speed) * item.amplitude);

                item.amplitude = Random.Range(0f, item.maxAmplitude);
                item.newPos.z += (Mathf.Sin(item.speed) * item.amplitude);

                item.transform.position = item.newPos;
            }
        }
    }

    void UseHublots(){
        foreach (hublot item in hublots)
        {
            if (item._activate)
            {
                if (activate_hublot)
                {
                    item.GetComponent<Rigidbody>().AddTorque(item.transform.up * item.forceAmount * Time.deltaTime, item._forceMode);
                }
            }
        }
    }
}
