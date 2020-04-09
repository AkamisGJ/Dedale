using UnityEngine;
using UnityEngine.UI;

public class InteractObject : MonoBehaviour
{
    [SerializeField] private Vector3 _rotationWhenLooked = Vector3.zero;
    [SerializeField] private  float _distanceWithCameraWehnLooked = 0.5f;
    [SerializeField] private AudioClip _OnTakeObject = null;
    [SerializeField] private AudioClip _OnThrowObject = null;
    [SerializeField] private bool _isKey = false;
    public AudioClip OnThrowObject { get { return _OnThrowObject; } }
    public AudioClip OnTakeObject { get { return _OnTakeObject; } }
    public bool IsKey { get => _isKey; }


    private void Awake()
    {
        if(GetComponent<ImageInteract>() == null)
        {
            Debug.Log("Image Interact script is missing on " + gameObject.name);
        }
    }

    public Vector4 Interact()
    {
        return (new Vector4(_rotationWhenLooked.x, _rotationWhenLooked.y, _rotationWhenLooked.z, _distanceWithCameraWehnLooked));
    }
}
