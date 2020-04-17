using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapEtiquette : MonoBehaviour
{
    [SerializeField] private Material _etiquette = null;
    [SerializeField] private float lerpDuration = 1f;
    private float lerpValue = 0f;

    public void StartLerping(){
        _etiquette.SetFloat("Vector1_9532F5BD", 0f);
        GameLoopManager.Instance.GameLoopPlayer += OnUpdate;
    }

    private void OnUpdate(){
        _etiquette.SetFloat("Vector1_9532F5BD", lerpValue);
        lerpValue += Time.deltaTime / lerpDuration;
        if(lerpValue >= 1f){
            GameLoopManager.Instance.GameLoopPlayer -= OnUpdate;
        }
    }
}
