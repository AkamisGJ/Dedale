using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName="ColorSaver", menuName = "DataBase/ColorSaver")]
public class ColorSaver : ScriptableObject
{
    [SerializeField] private Color[] colors;
}
