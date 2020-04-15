using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

namespace NullSave
{
    //[InitializeOnLoad]
    public class HierarchyIcons
    {

        #region Variables

        static Texture2D texturePanel;
        static List<int> markedObjects;
        static Dictionary<int, HierarchyData> clean;

        #endregion

        #region Constructor

        static HierarchyIcons()
        {
            clean = new Dictionary<int, HierarchyData>();
            EditorApplication.hierarchyWindowItemOnGUI += AddIcons;
            EditorApplication.hierarchyChanged += () =>
            {
                clean.Clear();
            };
            EditorApplication.projectChanged += () =>
            {
                clean.Clear();
            };
            Application.focusChanged += FocusChanged();
        }

        #endregion

        #region Private Methods

        private static void AddIcons(int instanceId, Rect selectionRect)
        {
            GameObject go = EditorUtility.InstanceIDToObject(instanceId) as GameObject;
            if (go == null) return;


            HierarchyData data;

            if (clean.ContainsKey(instanceId))
            {
                data = clean[instanceId];
                if (data.selectionRect == selectionRect)
                {
                    data = clean[instanceId];
                    for (int i = 0; i < data.colors.Count; i++)
                    {
                        GUI.DrawTexture(data.rects[i], data.icons[i], ScaleMode.ScaleToFit, true, 0, data.colors[i], 0, 0);
                    }
                    return;
                }
                else
                {
                    clean.Remove(instanceId);
                }
            }

            float offset = 18;
            MonoBehaviour[] behaviours = go.GetComponentsInChildren<MonoBehaviour>();
            data = new HierarchyData();

            if (NeedsMoreIcon(behaviours, go))
            {
                Texture2D t = GetTex("more_icons");
                Rect r = new Rect(selectionRect.x + selectionRect.width - offset, selectionRect.y, 16f, 16f);
                GUI.DrawTexture(r, t, ScaleMode.ScaleToFit, true, 0, Color.white, 0, 0);
                offset += 18;

                data.icons.Add(t);
                data.colors.Add(Color.white);
                data.rects.Add(r);
            }

            for (int i = behaviours.Length - 1; i > -1; i--)
            {
                if (behaviours[i] != null)
                {
                    HierarchyIconAttribute icon = behaviours[i].GetType().GetCustomAttribute(typeof(HierarchyIconAttribute)) as HierarchyIconAttribute;
                    if (icon != null)
                    {
                        if (behaviours[i].gameObject == go || icon.BubbleUp)
                        {
                            if (data.icons.Count <= 5)
                            {
                                Texture2D t = GetTex(icon.IconName);

                                Color c = icon.IconColor;

                                Rect r = new Rect(selectionRect.x + selectionRect.width - offset, selectionRect.y, 16f, 16f);
                                GUI.DrawTexture(r, t, ScaleMode.ScaleToFit, true, 0, c, 0, 0);
                                offset += 18;

                                data.icons.Add(t);
                                data.colors.Add(c);
                                data.rects.Add(r);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }

            clean.Add(instanceId, data);
        }

        private static bool NeedsMoreIcon(MonoBehaviour[] behaviours, GameObject go)
        {
            if (behaviours.Length < 6) return false;

            int count = 0;
            for (int i = 0; i < behaviours.Length; i++)
            {
                if (behaviours[i].gameObject == go)
                {
                    count += 1;
                    if (count >= 6) return true;
                }
                else
                {
                    HierarchyIconAttribute icon = behaviours[i].GetType().GetCustomAttribute(typeof(HierarchyIconAttribute)) as HierarchyIconAttribute;
                    if (icon != null && icon.BubbleUp)
                    {
                        count += 1;
                        if (count >= 6) return true;
                    }
                }
            }

            return false;
        }

        private static Texture2D GetTex(string name)
        {
            return (Texture2D)Resources.Load("Icons/" + name);
        }

        private static System.Action<bool> FocusChanged()
        {
            clean.Clear();
            return null;
        }

        #endregion

    }
}