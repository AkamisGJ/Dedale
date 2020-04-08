using UnityEngine;
using UnityEngine.UI;

public class ImageInteract : MonoBehaviour
{
    [SerializeField] private GameObject _LinkedObject = null;
    [SerializeField] private Canvas _canvas = null;
    private RawImage _currentImage = null;
    [SerializeField] private RawImage _imageInteraction = null;
    [SerializeField] private RawImage _inputImage = null;
    private bool _isFocus = false;
    private PlayerData _playerData = null;

    public bool IsFocus { get => _isFocus; set => _isFocus = value; }

    private void Start()
    {
        _playerData = PlayerManager.Instance.PlayerController.PlayerData;
        _currentImage = _imageInteraction;
        Color color = _inputImage.color;
        color.a = 0;
        _inputImage.color = color;
        Color colorCurrent = _currentImage.color;
        color.a = 0;
        _currentImage.color = color;
        _isFocus = false;
    }

    void Update()
    {
        _canvas.transform.position = _LinkedObject.transform.position;
        _canvas.transform.LookAt(PlayerManager.Instance.CameraUI.transform.position);
        if(Vector3.Angle(_currentImage.transform.forward, PlayerManager.Instance.CameraUI.transform.forward) > (180 - _playerData.AngleInteractionImage) && Vector3.Angle(_currentImage.transform.forward, PlayerManager.Instance.CameraUI.transform.forward) < (180 + _playerData.AngleInteractionImage) && PlayerManager.Instance.PlayerController.CurrentState == PlayerAgentController.MyState.MOVEMENT && Vector3.Distance(_currentImage.transform.position, PlayerManager.Instance.CameraUI.transform.position) < _playerData.DistanceInteractionImage)
        {
            Color color = _currentImage.color;
            color.a = 1;
            _currentImage.color = color;
        }
        else
        {
            Color color = _currentImage.color;
            color.a = 0;
            _currentImage.color = color;
        }
        if(IsFocus == true)
        {
            ShowImageInput();
        }
        else
        {
            ShowImageInteraction();
        }
    }

    public void ShowImageInteraction()
    {
        if (_currentImage != _imageInteraction)
        {
            Color color = _currentImage.color;
            color.a = 0;
            _currentImage.color = color;
            _currentImage = _imageInteraction;
            Color nextColor = _currentImage.color;
            color.a = 1;
            _currentImage.color = nextColor;
        }
    }

    public void ShowImageInput()
    {
        if (_currentImage != _inputImage)
        {
            Color color = _currentImage.color;
            color.a = 0;
            _currentImage.color = color;
            _currentImage = _inputImage;
            Color nextColor = _currentImage.color;
            color.a = 1;
            _currentImage.color = nextColor;
        }
    }
}