using UnityEngine;
using UnityEngine.UI;

public class ImageInteract : MonoBehaviour
{
    [SerializeField] private GameObject _uiPosition = null;
    private Canvas _canvas = null;
    private RawImage _currentImage = null;
    private RawImage _imageInteraction = null;
    private RawImage _unlockImage = null;
    private RawImage _inputImage = null;
    private RawImage _lockImage = null;
    private bool _isFocus = false;
    private PlayerData _playerData = null;
    private float _distance = 0;
    private bool _canShowLock = false;
    private Camera _playerCamera = null;
    private Door _door = null;
    public bool IsFocus { get => _isFocus; set => _isFocus = value; }
    public GameObject UiPosition { get => _uiPosition; }

    private void Awake()
    {
        if(_uiPosition == null)
        {
            _uiPosition = gameObject;
        }
    }

    private void Start()
    { 
        _playerData = PlayerManager.Instance.PlayerController.PlayerData;
        if (_uiPosition.gameObject.layer == LayerMask.NameToLayer("ObserveObject"))
        {
            SpawnCanvas();
            _imageInteraction = Instantiate(_playerData.ObservableObjectHelper, _canvas.transform, true);
            _distance = _playerData.DistanceHelperObservableObject;
        }
        else if (_uiPosition.gameObject.layer == LayerMask.NameToLayer("InteractObject"))
        {
            SpawnCanvas();
            if(GetComponent<Door>() != null && GetComponent<Door>().IsLocked == true)
            {
                _lockImage = Instantiate(_playerData.Lock, _canvas.transform, true);
                _unlockImage = Instantiate(_playerData.InteractionHelper, _canvas.transform, true);
                _imageInteraction = _lockImage;
                Color colorUnlock = _unlockImage.color;
                colorUnlock.a = 0;
                _inputImage.color = colorUnlock;
                _canShowLock = true;
                _door = GetComponent<Door>();
            }
            else if(GetComponent<Door>() != null)
            {
                _imageInteraction = Instantiate(_playerData.InteractionHelper, _canvas.transform, true);
                _lockImage = Instantiate(_playerData.Lock, _canvas.transform, true);
                Color colorLock = _lockImage.color;
                colorLock.a = 0;
                _canShowLock = false;
                _door = GetComponent<Door>();
            }
            _distance = _playerData.DistanceHelperInteraction;
        }
        else if (_uiPosition.gameObject.layer == LayerMask.NameToLayer("Ladder"))
        {
            SpawnCanvas();
            _imageInteraction = Instantiate(_playerData.LadderHelper, _canvas.transform, true);
            _distance = _playerData.DistanceHelperInteraction;
        }
        else if (_uiPosition.gameObject.layer == LayerMask.NameToLayer("NarrowWay"))
        {
            SpawnCanvas();
            _imageInteraction = Instantiate(_playerData.NarrowWayHelper, _canvas.transform, true);
            _distance = _playerData.DistanceHelperInteraction;
        }
        else if (_uiPosition.gameObject.layer == LayerMask.NameToLayer("Liana"))
        {
            SpawnCanvas();
            _imageInteraction = Instantiate(_playerData.LianaHelper, _canvas.transform, true);
            _distance = _playerData.DistanceHelperInteraction;
        }
        else
        {
            Debug.LogError("No layer on GameObject : "  + _uiPosition);
            this.enabled = false;
            return;
        }
        _currentImage = _imageInteraction;
        _currentImage.transform.localPosition = Vector3.zero;
        if(_lockImage != null)
        {
            _lockImage.transform.localPosition = Vector3.zero;
        }
        Color color = _inputImage.color;
        color.a = 0;
        _inputImage.color = color;
        Color colorCurrent = _currentImage.color;
        color.a = 0;
        _currentImage.color = color;
        _isFocus = false;
        _playerCamera = PlayerManager.Instance.CameraPlayer;
        GameLoopManager.Instance.GameLoopPortal += OnUpdate;
    }

    void SpawnCanvas()
    {
        _canvas = Instantiate(_playerData.CanvasHelper, _uiPosition.transform, true);
        _canvas.scaleFactor = 1;
        _inputImage = Instantiate(_playerData.InputHelper, _canvas.transform, true);
        _canvas.transform.position = _uiPosition.transform.position;
    }

    void OnUpdate()
    {
        if (PlayerManager.Instance.CameraUI != null && _playerCamera != null)
        {
            if(_door != null && _door.IsLocked == true)
            {
                _canShowLock = _door.IsLocked;
            }
            if (_canShowLock == true && _imageInteraction != _lockImage)
            {
                LockedDoor();
            }
            _canvas.transform.LookAt(PlayerManager.Instance.CameraUI.transform.position);
            RaycastHit raycastHit;
            Physics.Raycast(_playerCamera.transform.position, UiPosition.transform.position - _playerCamera.transform.position, out raycastHit, Vector3.Distance(_playerCamera.transform.position, UiPosition.transform.position), _playerData.CantSeeInteractionHelperBehindThis);
            if (Vector3.Angle(_currentImage.transform.forward, PlayerManager.Instance.CameraUI.transform.forward) > (180 - _playerData.AngleHelper) && PlayerManager.Instance.PlayerController.CurrentState == PlayerAgentController.MyState.MOVEMENT && Vector3.Distance(_currentImage.transform.position, PlayerManager.Instance.CameraUI.transform.position) < _distance && raycastHit.collider == null)
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
            if (_isFocus == true)
            {
                ShowImageInput();
            }
            else
            {
                ShowImageInteraction();
            }
        }
    }

    void LockedDoor()
    {
        _imageInteraction = _lockImage;
        _lockImage.transform.localPosition = Vector3.zero;
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
        if (_canShowLock == false && _currentImage != _inputImage)
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

    private void OnDestroy()
    {
        if(GameLoopManager.Instance != null)
        {
            GameLoopManager.Instance.GameLoopPortal -= OnUpdate;
        }
    }
}