using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#if UNITY_EDITOR
[HierarchyIcon("interact", false)]
#endif

public class InteractObject : MonoBehaviour
{
    [SerializeField] private Vector3 _rotationWhenLooked = Vector3.zero;
    [SerializeField] private  float _distanceWithCameraWehnLooked = 0.5f;
    [SerializeField] private UnityEvent _OnTakeObject = null;
    [SerializeField] private UnityEvent _OnThrowObject = null;
    [SerializeField] private bool _isKey = false;
    public UnityEvent OnThrowObject { get { return _OnThrowObject; } }
    public UnityEvent OnTakeObject { get { return _OnTakeObject; } }
    public bool IsKey { get => _isKey; }

    private void Awake()
    {
        if(GetComponent<ImageInteract>() == null)
        {
            Debug.Log("Image Interact script is missing on " + transform.name);
        }

        if (GetComponent<Collider>() == null)
        {
            Debug.LogWarning("There is no Collider on " + gameObject.name);
        }
    }

    public Vector4 Interact()
    {
        return (new Vector4(_rotationWhenLooked.x, _rotationWhenLooked.y, _rotationWhenLooked.z, _distanceWithCameraWehnLooked));
    }
}
