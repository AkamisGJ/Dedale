using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_flashing : MonoBehaviour
{
    [SerializeField] private Color _color1 = Color.white;
    [SerializeField] private Color _color2 = Color.black;
    [SerializeField] private float speed = 1f;
    private Color _tmpColor;
    private float time;
    private RawImage imageComponent;

    private void Start() {
        imageComponent = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        var Timevalue = Mathf.PingPong(Time.time * speed, 1f);
        _tmpColor = Color.Lerp(_color1, _color2, Timevalue);
        imageComponent.color = _tmpColor;
        //Debug.Log(Timevalue);
    }
}
