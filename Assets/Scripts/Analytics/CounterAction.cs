using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class CounterAction : MonoBehaviour
{
    [SerializeField] private string _actionCounter = null;
    private int _counter = 0;
    private bool _asStop = false;

    void Start()
    {
        _counter = 0;
        _asStop = false;
    }

    public void Incrementation()
    {
        _counter += 1;
    }

    public void Stop()
    {
        _asStop = true;
        AnalyticsEvent.Custom(_actionCounter, new Dictionary<string, object>
        {
            {_actionCounter + " number of action ", _counter }
        });
    }

    private void OnDestroy()
    {
        if (_asStop == false)
        {
            AnalyticsEvent.Custom(_actionCounter, new Dictionary<string, object>
            {
                {_actionCounter + " don't pass and stop playing at ", _actionCounter }
            });
        }
    }
}
