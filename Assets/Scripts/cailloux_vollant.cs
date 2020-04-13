using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class cailloux_vollant : MonoBehaviour
{
    //Position
    [InfoBox("Put the curve between time -1 and 1")]
    [SerializeField] AnimationCurve curve;
    private Vector3 initial_position;
    private Vector3 new_position = Vector3.zero;
    float time;
    float random_startTime = 0f;

    //Rotation
    private Quaternion initialRotation;
    private Quaternion newRotation = Quaternion.identity;
    private float lerpRotationValue = 0f; 
    [SerializeField] private float speedRotation = 0.2f;


    void Start()
    {
        initial_position = transform.position;
        initialRotation = transform.rotation;
        random_startTime = Random.Range(-1f, 1f);
    }


    void Update()
    {
        MovePosition();
        ChangeRotation();
    }

    void ChangeRotation(){
        if(transform.rotation == newRotation || lerpRotationValue > 1f){
            newRotation = Random.rotation;
            initialRotation = transform.rotation;
            lerpRotationValue = 0f;
        }
        lerpRotationValue += Time.deltaTime * speedRotation;
        transform.rotation = Quaternion.Lerp(initialRotation, newRotation, lerpRotationValue);
    }

    void MovePosition(){
        time = Mathf.Sin(Time.time + random_startTime);
        new_position = initial_position;
        new_position.y += curve.Evaluate(time);
        transform.position = new_position;
    }
}
