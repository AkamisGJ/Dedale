using System;
using UnityEditor;
using UnityEngine;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class HierarchyIconAttribute : Attribute
{

    #region Variables

    public readonly string IconName;
    public readonly Color IconColor;
    public readonly bool BubbleUp;

    #endregion

    #region Constructors

    public HierarchyIconAttribute(string iconName)
    {
        IconName = iconName;
        BubbleUp = true;

        if (EditorGUIUtility.isProSkin)
        {
            IconColor = new Color(1, 1, 1, 1);
        }
        else
        {
            IconColor = new Color(0.2f, 0.2f, 0.2f, 1);
        }
    }

    public HierarchyIconAttribute(string iconName, bool bubbleUp)
    {
        IconName = iconName;
        BubbleUp = bubbleUp;

        if (EditorGUIUtility.isProSkin)
        {
            IconColor = new Color(1, 1, 1, 1);
        }
        else
        {
            IconColor = new Color(0.2f, 0.2f, 0.2f, 1);
        }
    }

    public HierarchyIconAttribute(string iconName, string iconColor)
    {
        IconName = iconName;
        BubbleUp = true;

        if (!ColorUtility.TryParseHtmlString(iconColor, out IconColor))
        {
            if (EditorGUIUtility.isProSkin)
            {
                IconColor = new Color(1, 1, 1, 1);
            }
            else
            {
                IconColor = new Color(0.2f, 0.2f, 0.2f, 1);
            }
        }
    }

    public HierarchyIconAttribute(string iconName, string iconColor, bool bubbleUp)
    {
        IconName = iconName;
        BubbleUp = bubbleUp;

        if (!ColorUtility.TryParseHtmlString(iconColor, out IconColor))
        {
            if (EditorGUIUtility.isProSkin)
            {
                IconColor = new Color(1, 1, 1, 1);
            }
            else
            {
                IconColor = new Color(0.2f, 0.2f, 0.2f, 1);
            }
        }
    }

    #endregion

}
