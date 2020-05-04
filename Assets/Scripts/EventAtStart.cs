using UnityEngine.Events;
using UnityEngine;

public class EventAtStart : MonoBehaviour
{
    [SerializeField] private UnityEvent _unityEvent = null;

    void Start()
    {
        if(_unityEvent != null)
        {
            _unityEvent.Invoke();
        }
    }
}
