using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] Transform _transformImage = null;

    void Update()
    {
        _transformImage.Rotate(0, 0, 1);
    }
}
