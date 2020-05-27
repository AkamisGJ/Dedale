using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class cailloux_vollant : MonoBehaviour
{
    //Position
    [InfoBox("Put the curve between time -1 and 1")]
    [SerializeField] private AnimationCurve _curve = null;
    private Vector3 _initialPosition;
    private Vector3 _newPosition = Vector3.zero;
    private float _timePosition = 0;
    private float _randomStartTime = 0f;

    //Rotation
    private float _speedRotation = 0.2f;
    private Quaternion _initialRotation;
    private Quaternion _newRotation = Quaternion.identity;
    private float _lerpRotationValue = 0f;
    private float _timeRotation = 0;

    private void Awake()
    {
        _randomStartTime = SetRandomTime();
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
        _timePosition = 0;
        _timeRotation = 0;
        GameLoopManager.Instance.GameLoopPortal += OnUpdate;
    }

    float SetRandomTime()
    {
        return Random.Range(-1f, 1f);
    }

    void OnUpdate()
    {
        MovePosition();
        ChangeRotation();
    }

    void ChangeRotation()
    {
        if (transform.rotation == _newRotation || _lerpRotationValue > 1f)
        {
            _newRotation = Random.rotation;
            _initialRotation = transform.rotation;
            _lerpRotationValue = 0f;
        }
        _lerpRotationValue += Time.deltaTime * _speedRotation;
        transform.rotation = Quaternion.Lerp(_initialRotation, _newRotation, _lerpRotationValue);
    }

    void MovePosition()
    {
        _timeRotation += Time.deltaTime;
        _timePosition = Mathf.Sin(_timeRotation + _randomStartTime);
        Vector3 new_position = _initialPosition;
        new_position.y += _curve.Evaluate(_timePosition);
        transform.position = new_position;
    }

    private void OnDestroy()
    {
        if(GameLoopManager.Instance != null)
        {
            GameLoopManager.Instance.GameLoopPortal -= OnUpdate;
        }
    }
}
