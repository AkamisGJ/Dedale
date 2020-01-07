using UnityEngine;
using Prof.Utils;

public class CameraManager : Singleton<CameraManager>
{
    #region Fields
    private Camera _cameraPlayer;
    private Camera _cameraEnviro;
    private Camera _cameraUI;
    private Camera _cameraObject;
    #endregion Fields

    #region Properties
    public Camera CameraPlayer { get { return _cameraPlayer; } set { _cameraPlayer = value; } }
    public Camera CameraEnviro { get { return _cameraEnviro; } set { _cameraEnviro = value; } }
    public Camera CameraUI { get { return _cameraUI; } set { _cameraUI = value; } }
    public Camera CameraObject { get { return _cameraObject; } set { _cameraObject = value; } }
    #endregion Properties

    protected override void Start()
    {
        base.Start();
    }
}