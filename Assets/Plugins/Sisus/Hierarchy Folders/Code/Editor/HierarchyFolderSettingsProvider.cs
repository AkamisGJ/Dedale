using System;
using UnityEditor;
using UnityEngine;
using JetBrains.Annotations;

namespace Sisus.HierarchyFolders
{
	/// <summary>
	/// Handles drawing preferences view for Hierarchy Folders in window that can be opened via Edit > Preferences.
	/// </summary>
	public static class HierarchyFolderSettingsProvider
	{
		private static Editor defaultEditor;

		#if UNITY_2019_1_OR_NEWER
		[SettingsProvider, UsedImplicitly]
		private static SettingsProvider CreateHierarchyFolderDrawer()
		{
			var provider = new SettingsProvider("Preferences/Hierarchy Folders", SettingsScope.User)
			{
				label = "Hierarchy Folders",
				guiHandler = DrawPreferencesGUI,

				// Populate the search keywords to enable smart search filtering and label highlighting
				keywords = new System.Collections.Generic.HashSet<string>(new[]
				{
					"defaultName",
					"namePrefix",
					"nameSuffix",
					"removeWhenEnterPlayMode",
					"removeWhenMakingBuild",
					"infoBoxText"
				})
			};

			return provider;
		}
		
		private static void DrawPreferencesGUI(string searchContext)
		{
			DrawPreferencesGUI();
		}
		#endif

		#if !UNITY_2019_1_OR_NEWER
		[PreferenceItem("Hierarchy Folders"), UsedImplicitly]
		#endif
		private static void DrawPreferencesGUI()
		{
			var labelWidthWas = EditorGUIUtility.labelWidth;
			var indentLevelWas = EditorGUI.indentLevel;
			EditorGUIUtility.labelWidth = 255f;
			EditorGUI.indentLevel = 1;

			var preferences = HierarchyFolderPreferences.Get();

			Editor.CreateCachedEditor(preferences, null, ref defaultEditor);

			string previousNamePrefix = preferences.namePrefix;
			string previousNameSuffix = preferences.nameSuffix;

			EditorGUI.BeginChangeCheck();

			// hide the script field
			GUILayout.Space(-20f);

			defaultEditor.OnInspectorGUI();

			if(EditorGUI.EndChangeCheck())
			{
				if(!string.Equals(previousNamePrefix, preferences.namePrefix, StringComparison.OrdinalIgnoreCase))
				{
					preferences.previousNamePrefix = previousNamePrefix;
				}
				if(!string.Equals(previousNameSuffix, preferences.nameSuffix, StringComparison.OrdinalIgnoreCase))
				{
					preferences.previousNameSuffix = previousNameSuffix;
				}
			}

			GUILayout.Space(15f);

			GUILayout.BeginHorizontal();
			{
				GUILayout.Space(15f);

				const float buttonHeight = 25f;

				if(GUILayout.Button("Apply Changes", GUILayout.Height(buttonHeight)))
				{
					preferences.SaveState();

					if(preferences.playModeBehaviour != StrippingType.None)
					{
						var hierarchyFolderScript = FileUtility.FindScriptAssetForType(typeof(HierarchyFolder));
						if(MonoImporter.GetExecutionOrder(hierarchyFolderScript) >= 0)
						{
							MonoImporter.SetExecutionOrder(hierarchyFolderScript, -1000);
						}
					}
				}

				GUILayout.Space(15f);

				if(GUILayout.Button("Discard Changes", GUILayout.Height(buttonHeight)))
				{
					preferences.DiscardChanges();
				}

				GUILayout.Space(15f);

				if(GUILayout.Button("Reset To Defaults", GUILayout.Height(buttonHeight)))
				{
					preferences.ResetToDefaults();
				}

				GUILayout.Space(15f);
			}
			GUILayout.EndHorizontal();

			EditorGUIUtility.labelWidth = labelWidthWas;
			EditorGUI.indentLevel = indentLevelWas;
		}
	}
}