using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : MonoBehaviour
{
    protected GameObject _noteGameObject = null;
    [SerializeField] GuitarHeroManager _guitarHeroManager = null;
    [SerializeField] private KeyCode keyCode = KeyCode.A;
    [SerializeField] private Color _color = Color.yellow;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _spriteRenderer.color = _color;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Note"))
        {
            _noteGameObject = other.gameObject;
            _spriteRenderer.color = Color.white;
        }
    }

    private void Update()
    {
        if (_spriteRenderer.color != _color && _noteGameObject == null)
        {
            _spriteRenderer.color = _color;
        }
        if (Input.GetKeyDown(keyCode))
        {
            OnPressedTouch();
        }
    }

    private void OnPressedTouch()
    {
        if(_noteGameObject != null)
        {
            Destroy(_noteGameObject);
            OnDestroyNote();
        }
        else
        {
            OnFailDestroy();
        }
    }

    private void OnDestroyNote()
    {
        _noteGameObject = null;
        _spriteRenderer.color = Color.green;
        _guitarHeroManager.TrustMeter = 1;
    }

    private void OnFailDestroy()
    {
        _spriteRenderer.color = Color.red;
        _guitarHeroManager.TrustMeter = -1;
    }
}
