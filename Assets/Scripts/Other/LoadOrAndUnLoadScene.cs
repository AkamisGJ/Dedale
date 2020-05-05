using UnityEngine;
using UnityEngine.SceneManagement;
using GameplayIngredients;
using System;
using System.Collections;

public class LoadOrAndUnLoadScene : Singleton<LoadOrAndUnLoadScene>
{

    [SerializeField] private string[] gameLevels;
    private AsyncOperation _asyncOperationLoad;
    public LoadSceneMode _loadSceneMode = LoadSceneMode.Additive;

    

    
    public void PreloadScene(string new_level)
    {
        
        _asyncOperationLoad = SceneManager.LoadSceneAsync(new_level, _loadSceneMode);
        _asyncOperationLoad.allowSceneActivation = false;
        print("PreloadScene");
        
    }

    public void PreloadScene(int new_level)
    {
        
        _asyncOperationLoad = SceneManager.LoadSceneAsync(new_level, _loadSceneMode);
        _asyncOperationLoad.allowSceneActivation = false;
        print("PreloadScene");
        
    }
    
    public void FinalLoadScene(){
        if (_asyncOperationLoad != null)
        {
            _asyncOperationLoad.allowSceneActivation = true;
        }else{
            Debug.Log("Can't load the next scene");
        }
        print("FinalLoadScene");
    }

    public void UnLoadScene(string level){
        if (SceneManager.GetSceneByName(level).isLoaded)
        {
            SceneManager.UnloadSceneAsync(level);
        }
        print("UnLoadScene");
    }

    public void UnLoadScene(int level){
        if (SceneManager.GetSceneByBuildIndex(level).isLoaded)
        {
            SceneManager.UnloadSceneAsync(level);
        }
        print("UnLoadScene");
    }
    

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         LoadorUnloadScene();
    //     }
    // }

    // public void LoadorUnloadScene()
    // {
    //     if(_alreadyUse == false)
    //     {
    //         if (_previousSceneName != string.Empty)
    //         {
    //             AsyncOperation scene = SceneManager.UnloadSceneAsync(_previousSceneName);
    //         }
    //         if (_nextSceneName != string.Empty)
    //         {
    //             _asyncOperationLoad.allowSceneActivation = true;
    //         }
    //         _alreadyUse = true;
    //     }
    // }

}