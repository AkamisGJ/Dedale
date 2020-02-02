// Copyright (c) Pixel Crushers. All rights reserved.

using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace PixelCrushers.Wrappers
{
    public class  Random_Timed_Event : MonoBehaviour
    {

    [PropertyOrder(1)] public float _firstRandom = 0f;
    [PropertyOrder(2)] public float _secondRandom = 0f;
    [Space()]

    [ReadOnly, PropertyOrder(3)]
    public float RandomValue = 0f;

    [PropertyOrder(4)] public UnityEvent OnTimeReach;
    [PropertyOrder(0)] public bool activeOnStart = true;

    private bool active = true;
    private Animator m_animator;


    private void Start() {
        m_animator = GetComponent<Animator>();
        CalculateTime();
        active = activeOnStart;
    }

    private void CalculateTime(){
        RandomValue = Random.Range(_firstRandom, _secondRandom);
    }

    private void Update() {
        if(active && m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !m_animator.IsInTransition(0)){
            if(RandomValue <= 0f){
                OnTimeReach.Invoke();
                CalculateTime();
            }else{
                RandomValue -= Time.deltaTime;
            }
        }
    }

    public void ActivateTimer(){
        active = true;
    }

    public void DesactivateTimer(){
        active = false;
    }


    }


    

}
