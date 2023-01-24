﻿// Copyright (c) 2020 JioGlass. All Rights Reserved.

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace JMRSDK.Toolkit.UI.Inspector
{
    [CustomEditor(typeof(JMRUITertiaryButtonGroup))]
    public class JMRUITertiaryButtonGroupInspector : Editor
    {
        #region Editor Actions

        const string PrefabGUID = "3094c0c8c9d34de4aaf3cc0c19c86228";//"907db0e31f92d6843ac597e0d0de6fcc";//"7ecd6301ec5f89a468e705b721a1cf37";

        private static string PrefabPath => AssetDatabase.GUIDToAssetPath(PrefabGUID);

        [MenuItem("JioMixedReality/Toolkits/V2/Tertiary Button Group", false)]
        static void InstantiatePrefab()
        {
            GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath(PrefabPath, typeof(GameObject));

            if (prefab != null)
            {
                Transform selectedObject = Selection.activeTransform;

                Selection.activeObject = !selectedObject ? PrefabUtility.InstantiatePrefab(prefab) : PrefabUtility.InstantiatePrefab(prefab, selectedObject);

                if (Selection.activeObject != null)
                {
                    Undo.RegisterCreatedObjectUndo(Selection.activeObject, $"Create {prefab.name} Object");
                }

                GameObject clone = Selection.activeGameObject;
                Selection.activeGameObject = null;
                if (clone != null)
                {
                    PrefabUtility.UnpackPrefabInstance(clone, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
                    //PrefabUtility.ReconnectToLastPrefab(clone);
                    //PrefabUtility.RevertPrefabInstance(clone);

                    //Force position the instantiated prefab if pos are not set currectly on prefab settings.
                    clone.transform.localPosition = Vector3.zero;
                }

            }
        }
        #endregion

        public override void OnInspectorGUI()
        {
            //Add the default stuff
            DrawDefaultInspector();
        }
    }
}
#endif