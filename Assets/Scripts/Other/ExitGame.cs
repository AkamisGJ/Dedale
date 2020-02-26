using UnityEngine.SceneManagement;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void Exit(GameObject gameObject)
    {
        Destroy(gameObject);
        GameManager.Instance.ChangeState(GameManager.MyState.MAINMENU);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Exit(other.gameObject);
        }
    }
}
