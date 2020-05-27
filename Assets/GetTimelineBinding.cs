using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class GetTimelineBinding : MonoBehaviour
{
    public List<GameObject> trackList = new List<GameObject>();
    public Animator cameraAnimator;
    PlayableDirector timeline;
    TimelineAsset timelineAsset;
    public bool autobindingTracks = false;
    void Start()
    {
        //Get the Camera Animator from the instanciate player
        cameraAnimator = PlayerManager.Instance.CameraPlayer.GetComponent<Animator>();
        timeline = GetComponent<PlayableDirector>();
        

        if(autobindingTracks){
            BindCameraTracks();
        }
    }

    public void BindTimelineTracks(){
        Debug.Log("Binding Timeline Tracks!");
        timelineAsset = (TimelineAsset)timeline.playableAsset;
        // iterate through tracks and map the objects appropriately
        for( var i = 0; i < trackList.Count; i ++)
        {
            if( trackList[i] != null)
            {
                var track = timelineAsset.GetOutputTrack(i);
                timeline.SetGenericBinding(track, trackList[i]);
            }
        }
    }

    public void BindCameraTracks(){
        Debug.Log("Bind Camera tracks !");
        timelineAsset = (TimelineAsset)timeline.playableAsset;

        var track = timelineAsset.GetOutputTrack(3);
        timeline.SetGenericBinding(track, cameraAnimator);
    }
}
