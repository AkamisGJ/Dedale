using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private string _nextScene;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameManager.Instance.ChangeState(GameManager.MyState.Game, _nextScene);
        }
    }
}