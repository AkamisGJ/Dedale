using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class TimeToPassLevel : MonoBehaviour
{
    private float _time = 0;
    [SerializeField] private string _nameOfPassage = null;
    private bool _playerPassTheLevel = false;

    public void StartTimer()
    {
        _time = 0;
        _playerPassTheLevel = false;
        GameLoopManager.Instance.GameLoopPortal += OnUpdate;
    }

    void OnUpdate()
    {
        _time += Time.deltaTime;
    }

    public void StopTimer()
    {
        _playerPassTheLevel = true;
        GameLoopManager.Instance.GameLoopPortal -= OnUpdate;
        Analytics.CustomEvent(_nameOfPassage, new Dictionary<string, object>
        {
            {_nameOfPassage + " pass in ", _time }
        });
    }

    private void OnDestroy()
    {
        if(_playerPassTheLevel == false)
        {
            AnalyticsEvent.Custom(_nameOfPassage, new Dictionary<string, object>
            {
                {_nameOfPassage + " don't pass and stop playing at ", _time }
            });
        }
    }
}
