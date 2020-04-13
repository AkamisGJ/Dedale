using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SliderWeightVolume : MonoBehaviour
{
    [SerializeField] private float _timeToBlend = 1f;
    private float _increasseAmout = 0f;
    private bool blending = false;
    private Volume _volume;
    
    void Start()
    {
        _volume = GetComponent<Volume>();
        _increasseAmout = Time.deltaTime / _timeToBlend;
    }

    public void StartBlending()
    {
        blending = true;
    }

    private void Update()
    {
        if (blending)
        {
            _volume.weight += _increasseAmout;

            if(_volume.weight > 1)
            {
                blending = false;
            }
        }
    }
}
