using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class cailloux_vollant_manager : MonoBehaviour
{
    /*
    //Manager
    [InfoBox("This script will make floating all the object on the scene")]
    cailloux_vollant[] floatingObjects = null;

    private void Awake() {
        floatingObjects = GameObject.FindObjectsOfType<cailloux_vollant>();

        foreach (cailloux_vollant item in floatingObjects)
        {
            item._randomStartTime = SetRandomTime();
            item._initialPosition = item.transform.position;
            item._initialRotation = item.transform.rotation;
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
            if(item.transform.rotation == item._newRotation || item._lerpRotationValue > 1f){
                item._newRotation = Random.rotation;
                item._initialRotation = item.transform.rotation;
                item._lerpRotationValue = 0f;
            }
            item._lerpRotationValue += Time.deltaTime * item._speedRotation;
            item.transform.rotation = Quaternion.Lerp(item._initialRotation, item._newRotation, item._lerpRotationValue);
        }
    }

    void MovePosition(){
        foreach (cailloux_vollant item in floatingObjects)
        {
            item._timePosition = Mathf.Sin(Time.time + item._randomStartTime);
            Vector3 new_position = item._initialPosition;
            new_position.y += item.curve.Evaluate(item._timePosition);
            item.transform.position = new_position;
        }
    }
    */
}
