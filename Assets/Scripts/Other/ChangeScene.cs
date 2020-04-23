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
}