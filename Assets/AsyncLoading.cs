using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsyncLoading : MonoBehaviour
{
    public void PreloadScene(string new_level)
    {
        LoadOrAndUnLoadScene.Instance.PreloadScene(new_level);
    }

    public void PreloadScene(int new_level)
    {
        LoadOrAndUnLoadScene.Instance.PreloadScene(new_level);
    }
    
    public void FinalLoadScene(){
        LoadOrAndUnLoadScene.Instance.FinalLoadScene();
    }

    public void UnLoadScene(string level){
        LoadOrAndUnLoadScene.Instance.UnLoadScene(level);
    }

    public void UnLoadScene(int level){
        LoadOrAndUnLoadScene.Instance.UnLoadScene(level);
    }
}
