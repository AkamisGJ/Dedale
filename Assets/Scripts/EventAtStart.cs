using UnityEngine.Events;
using UnityEngine;

public class EventAtStart : MonoBehaviour
{
    [SerializeField] private UnityEvent _unityEvent = null;
    private float _currentTime = 0;
    [SerializeField] private float _timer = 1;

    void Start()
    {
        GameLoopManager.Instance.GameLoopPortal += OnStart;
    }

    void OnStart()
    {
        _currentTime += Time.deltaTime;
        if (_unityEvent != null && _currentTime >= _timer)
        {
            _unityEvent.Invoke();
            GameLoopManager.Instance.GameLoopPortal -= OnStart;
        }
    }

    private void OnDestroy()
    {
        if(GameLoopManager.Instance != null)
        {
            GameLoopManager.Instance.GameLoopPortal -= OnStart;
        }
    }
}
