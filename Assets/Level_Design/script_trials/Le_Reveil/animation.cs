using UnityEngine;

public class animation : MonoBehaviour
{
    [SerializeField] private Animator _animation;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag =="Player")
        {
            _animation.SetBool("down", true);
        }
    }
}
