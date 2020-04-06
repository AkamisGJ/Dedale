using UnityEngine;
using UnityEngine.UI;

public class ImageInteract : MonoBehaviour
{
    [SerializeField] private GameObject _LinkedObject = null;
    [SerializeField] private Canvas _canvas = null;
    [SerializeField] private RawImage _image = null;
    [SerializeField] private float _angle = 45;
    [SerializeField] private float _distance = 5;

    void Update()
    {
        _canvas.transform.position = _LinkedObject.transform.position;
        _canvas.transform.LookAt(PlayerManager.Instance.CameraUI.transform.position);
        if(Vector3.Angle(_image.transform.forward, PlayerManager.Instance.CameraUI.transform.forward) > (180 - _angle) && Vector3.Angle(_image.transform.forward, PlayerManager.Instance.CameraUI.transform.forward) < (180 + _angle) && PlayerManager.Instance.PlayerController.CurrentState == PlayerAgentController.MyState.MOVEMENT && Vector3.Distance(_image.transform.position, PlayerManager.Instance.CameraUI.transform.position) < _distance)
        {
            Color color = _image.color;
            color.a = 1;
            _image.color = color;
        }
        else
        {
            Color color = _image.color;
            color.a = 0;
            _image.color = color;
        }
    }
}
