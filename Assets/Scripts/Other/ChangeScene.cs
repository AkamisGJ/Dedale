using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private string _nextScene = null;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            SwitchScene();
        }
    }

    public void SwitchScene(){
        GameManager.Instance.ChangeState(GameManager.MyState.GAME, _nextScene);
    }

    /*
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
    }*/
}