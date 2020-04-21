using UnityEngine;

public class CouloirInfinieManager : MonoBehaviour
{
    [SerializeField] private Couloir _couloirMidOnStart = null;
    [SerializeField] private Couloir _couloirFrontOnStart = null;
    [SerializeField] private Couloir _couloirBackOnStart = null;
    [SerializeField] private GameObject _frontFin = null;
    private Couloir _couloirMid = null;
    private Couloir _couloirFront = null;
    private Couloir _couloirBack = null;
    private Couloir _switchCouloir = null;

    private void Start()
    {
        _couloirBack = _couloirBackOnStart;
        _couloirFront = _couloirFrontOnStart;
        _couloirMid = _couloirMidOnStart;
        _frontFin.transform.position = _couloirFront.FrontFin.transform.position;
    }

    public void PlayerGoInFront()
    {
        _switchCouloir = _couloirBack;
        _switchCouloir.transform.position = _couloirFront.PositionFront.transform.position;
        _couloirBack = _couloirMid;
        _couloirMid = _couloirFront;
        _couloirFront = _switchCouloir;
        _frontFin.transform.position = _couloirFront.FrontFin.transform.position;
    }
}
