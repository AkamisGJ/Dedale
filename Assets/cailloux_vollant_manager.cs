using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class cailloux_vollant_manager : MonoBehaviour
{
    //Manager
    [InfoBox("This script will make floating all the object on the scene")]
    cailloux_vollant[] floatingObjects = null;

    private void Awake() {
        floatingObjects = GameObject.FindObjectsOfType<cailloux_vollant>();

        foreach (cailloux_vollant item in floatingObjects)
        {
            item.random_startTime = SetRandomTime();
            item.initial_position = item.transform.position;
            item.initialRotation = item.transform.rotation;
        }
    }

    float SetRandomTime()
    {
        return Random.Range(-1f, 1f);
    }


    void Update()
    {
        MovePosition();
        ChangeRotation();
    }

    void ChangeRotation(){
        foreach (cailloux_vollant item in floatingObjects)
        {
            if(item.transform.rotation == item.newRotation || item.lerpRotationValue > 1f){
                item.newRotation = Random.rotation;
                item.initialRotation = item.transform.rotation;
                item.lerpRotationValue = 0f;
            }
            item.lerpRotationValue += Time.deltaTime * item.speedRotation;
            item.transform.rotation = Quaternion.Lerp(item.initialRotation, item.newRotation, item.lerpRotationValue);
        }
    }

    void MovePosition(){
        foreach (cailloux_vollant item in floatingObjects)
        {
            item.time = Mathf.Sin(Time.time + item.random_startTime);
            Vector3 new_position = item.initial_position;
            new_position.y += item.curve.Evaluate(item.time);
            item.transform.position = new_position;
        }
    }
}
