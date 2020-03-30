using UnityEngine;

public class StartDialogue : MonoBehaviour
{
    [SerializeField] private DialogueScript _dialogueScript = null;

    void DialogueStart()
    {
        _dialogueScript.OnStart();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DialogueStart();
        }
    }
}