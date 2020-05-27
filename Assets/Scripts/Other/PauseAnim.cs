using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseAnim : MonoBehaviour
{
    [SerializeField] Animator _animator = null;

    void Update()
    {
        if(_animator != null)
        {
            PauseAnimator();
        }
    }

    private void PauseAnimator()
    {
        if (GameLoopManager.Instance.IsPaused == true && _animator.enabled == true)
        {
            _animator.enabled = false;
        }
        else if(GameLoopManager.Instance.IsPaused == false && _animator.enabled == false)
        {
            _animator.enabled = true;
        }
    }
}
