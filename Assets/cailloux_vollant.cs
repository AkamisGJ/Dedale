using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class cailloux_vollant : MonoBehaviour
{
    [InfoBox("Put the curve between time -1 and 1")]
    [SerializeField] AnimationCurve curve;
    private Vector3 initial_position;
    private Vector3 new_position;
    float time;
    float random_startTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        initial_position = transform.position;
        random_startTime = Random.Range(-1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        time = Mathf.Sin(Time.time + random_startTime);
        new_position = initial_position;
        new_position.y += curve.Evaluate(time);
        transform.position = new_position;
    }
}
