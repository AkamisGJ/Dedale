using UnityEngine;

public class StartDialogue : MonoBehaviour
{
    [SerializeField] private DialogueScript _dialogueScript = null;
    [SerializeField] private bool _onStart = false;

    private void Start() {
        if(_onStart){
            DialogueStart();
        }
    }

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