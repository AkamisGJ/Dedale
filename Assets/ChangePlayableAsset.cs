using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class ChangePlayableAsset : MonoBehaviour
{
    [SerializeField] private PlayableDirector _playableDirector;
    [SerializeField] private TimelineAsset _newTimelineAsset;

    // Start is called before the first frame update
    void Start()
    {
        _playableDirector.Play(_newTimelineAsset);
    }

}
