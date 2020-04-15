using UnityEngine;

public class Anim_Hopital : MonoBehaviour
{
    [SerializeField] private Animator _animation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _animation.SetBool("down", true);
        }
        else
        {
            _animation.SetBool("down", false);
        }
    }
}

