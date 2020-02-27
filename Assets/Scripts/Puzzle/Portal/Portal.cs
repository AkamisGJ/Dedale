using UnityEngine;
using System.Collections.Generic;

public class Portal : MonoBehaviour
{
    [SerializeField] private Portal _linkedPortal = null;
    [SerializeField] private MeshRenderer _screen = null;
    private Camera _playerCamera = null;
    [SerializeField] private Camera _portalCamera = null;
    private RenderTexture _viewTexture = null;
    private List<PortalTraveller> _trackedTravellers = null;

    void Start()
    {
        _playerCamera = PlayerManager.Instance.CameraPlayer;
        _portalCamera.enabled = false;
        _trackedTravellers = new List<PortalTraveller>();
    }

    void Update()
    {
        
    }

    private void LateUpdate()
    {
        for (int i = 0; i < _trackedTravellers.Count; i++)
        {
            PortalTraveller traveller = _trackedTravellers[i];

            Transform travellerT = traveller.transform;

            Vector3 offsetFromPortal = travellerT.position - transform.position;
            int portalSide = System.Math.Sign(Vector3.Dot(offsetFromPortal, transform.forward));
            int portalSideOld = System.Math.Sign(Vector3.Dot(traveller.PreviousOffsetFromPortal, transform.forward));
            if(portalSide != portalSideOld)
            {
                Matrix4x4 m = _linkedPortal.transform.localToWorldMatrix * transform.worldToLocalMatrix * travellerT.localToWorldMatrix;
                traveller.Teleport(transform, _linkedPortal.transform, m.GetColumn(3), m.rotation);
                _linkedPortal.OnTravellerEnterPortal(traveller);
                _trackedTravellers.RemoveAt(i);
                i--;
            }
            else
            {
                traveller.PreviousOffsetFromPortal = offsetFromPortal;
            }
        }
        Render();
    }

    void CreateViewTexture()
    {
        if(_viewTexture == null || _viewTexture.width != Screen.width || _viewTexture.height != Screen.height)
        {
            if(_viewTexture != null)
            {
                _viewTexture.Release();
            }
            _viewTexture = new RenderTexture(Screen.width, Screen.height, 0);
            _portalCamera.targetTexture = _viewTexture;
            _linkedPortal._screen.material.SetTexture("_MainTex", _viewTexture);
        }
    }

    static bool VisibleFromCamera(Renderer renderer, Camera camera)
    {
        Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(frustumPlanes, renderer.bounds);
    }

    public void Render()
    {
        if(!VisibleFromCamera(_linkedPortal._screen, _playerCamera))
        {
            var testTexture = new Texture2D(1, 1);
            testTexture.SetPixel(0, 0, Color.red);
            testTexture.Apply();
            _linkedPortal._screen.material.SetTexture("_MainTex", testTexture);
            return;
        }
        _linkedPortal._screen.material.SetTexture("_MainTex", _viewTexture);
        _screen.enabled = false;
        CreateViewTexture();

        Matrix4x4 m = transform.localToWorldMatrix * _linkedPortal.transform.worldToLocalMatrix * _playerCamera.transform.localToWorldMatrix;
        _portalCamera.transform.SetPositionAndRotation(m.GetColumn(3), m.rotation);

        _portalCamera.Render();

        _screen.enabled = true;
    }

    void OnTravellerEnterPortal(PortalTraveller traveller)
    {
        if (!_trackedTravellers.Contains(traveller))
        {
            traveller.EnterPortalThreshold();
            traveller.PreviousOffsetFromPortal = traveller.transform.position - transform.position;
            _trackedTravellers.Add(traveller);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PortalTraveller traveller = other.GetComponent<PortalTraveller>();
        if (traveller)
        {
            OnTravellerEnterPortal(traveller);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PortalTraveller traveller = other.GetComponent<PortalTraveller>();
        if(traveller && _trackedTravellers.Contains(traveller))
        {
            traveller.ExitPortalThreshold();
            _trackedTravellers.Remove(traveller);
        }
    }
}
