using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class cailloux_vollant : MonoBehaviour
{
        //Position
        [InfoBox("Put the curve between time -1 and 1")]
        [SerializeField] public AnimationCurve curve;
        [HideInInspector] public Vector3 initial_position;
        [HideInInspector] public Vector3 new_position = Vector3.zero;
        [HideInInspector] public float time;
        [HideInInspector] public float random_startTime = 0f;

        //Rotation
        [SerializeField] public float speedRotation = 0.2f;
        [HideInInspector] public Quaternion initialRotation;
        [HideInInspector] public Quaternion newRotation = Quaternion.identity;
        [HideInInspector] public float lerpRotationValue = 0f; 
}
