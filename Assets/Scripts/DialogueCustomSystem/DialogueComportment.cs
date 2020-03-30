using UnityEngine;

public class DialogueComportment : MonoBehaviour
{
    private DialogueManager _dialogueManager = null;

    void Start()
    {
        _dialogueManager = GetComponentInParent<DialogueManager>();
    }

    void Update()
    {
        
    }

    private void OnDestroy()
    {
        _dialogueManager.Dialogues.Remove(this.gameObject);
    }
}