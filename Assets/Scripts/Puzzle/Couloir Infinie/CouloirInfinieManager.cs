using UnityEngine;

public class CouloirInfinieManager : MonoBehaviour
{
    [SerializeField] private Couloir _couloirMidOnStart = null;
    [SerializeField] private Couloir _couloirMidFrontOnStart = null;
    [SerializeField] private Couloir _couloirMidBackOnStart = null;
    [SerializeField] private Couloir _couloirFrontOnStart = null;
    [SerializeField] private Couloir _couloirBackOnStart = null;
    [SerializeField] private GameObject _frontFin = null;
    [SerializeField] private GameObject _BackFin = null;
    private Couloir _couloirMid = null;
    private Couloir _couloirMidFront = null;
    private Couloir _couloirMidBack = null;
    private Couloir _couloirFront = null;
    private Couloir _couloirBack = null;
    private Couloir _switchCouloir = null;

    private void Start()
    {
        _couloirBack = _couloirBackOnStart;
        _couloirFront = _couloirFrontOnStart;
        _couloirMid = _couloirMidOnStart;
        _couloirMidBack = _couloirMidBackOnStart;
        _couloirMidFront = _couloirMidFrontOnStart;
        _couloirMid.TriggerBack.SetActive(true);
        _couloirMid.TriggerFront.SetActive(true);
        _couloirFront.TriggerFront.SetActive(false);
        _couloirFront.TriggerBack.SetActive(false);
        _couloirBack.TriggerBack.SetActive(false);
        _couloirBack.TriggerFront.SetActive(false);
        _couloirMidFront.TriggerFront.SetActive(false);
        _couloirMidFront.TriggerBack.SetActive(false);
        _couloirMidBack.TriggerFront.SetActive(false);
        _couloirMidBack.TriggerBack.SetActive(false);
        _frontFin.transform.position = _couloirFront.FrontFin.transform.position;
        _BackFin.transform.position = _couloirBack.BackFin.transform.position;
    }

    public void PlayerGoInFront()
    {
        _couloirMid.TriggerBack.SetActive(false);
        _couloirMid.TriggerFront.SetActive(false);
        _couloirMidFront.TriggerFront.SetActive(true);
        _couloirMidFront.TriggerBack.SetActive(true);
        _switchCouloir = _couloirBack;
        _switchCouloir.transform.position = _couloirFront.PositionFront.transform.position;
        _couloirBack = _couloirMidBack;
        _couloirMidBack = _couloirMid;
        _couloirMid = _couloirMidFront;
        _couloirMidFront = _couloirFront;
        _couloirFront = _switchCouloir;
        _frontFin.transform.position = _couloirFront.FrontFin.transform.position;
        _BackFin.transform.position = _couloirBack.BackFin.transform.position;
    }

    public void PlayerGoInBack()
    {
        _couloirMid.TriggerBack.SetActive(false);
        _couloirMid.TriggerFront.SetActive(false);
        _couloirMidBack.TriggerFront.SetActive(true);
        _couloirMidBack.TriggerBack.SetActive(true);
        _switchCouloir = _couloirFront;
        _switchCouloir.transform.position = _couloirBack.PositionBack.transform.position;
        _couloirFront = _couloirMidFront;
        _couloirMidFront = _couloirMid;
        _couloirMid = _couloirMidBack;
        _couloirMidBack = _couloirBack;
        _couloirBack = _switchCouloir;
        _frontFin.transform.position = _couloirFront.FrontFin.transform.position;
        _BackFin.transform.position = _couloirBack.BackFin.transform.position;
    }
}
