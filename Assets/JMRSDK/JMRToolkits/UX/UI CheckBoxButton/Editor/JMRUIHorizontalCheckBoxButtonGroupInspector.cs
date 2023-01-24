﻿// Copyright (c) 2020 JioGlass. All Rights Reserved.

#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace JMRSDK.Toolkit.UI.Inspector
{
    [CustomEditor(typeof(JMRUIPrimaryCheckBoxButtonGroup))]
    public class JMRUIHorizontalCheckBoxButtonGroupInspector : Editor
    {
        #region Editor Actions

        const string PrefabGUID = "d4939373f04fe764282be0b05598e030";

        private static string PrefabPath => AssetDatabase.GUIDToAssetPath(PrefabGUID);

        [MenuItem("JioMixedReality/Toolkits/V2/Horizontal Checkbox Group")]
        static void InstantiatePrefab()
        {
            GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath(PrefabPath, typeof(GameObject));

            if(prefab != null)
            {
                Transform selectedObject = Selection.activeTransform;
                if(selectedObject != null)
                {
                    Selection.activeObject = PrefabUtility.InstantiatePrefab(prefab as GameObject, selectedObject);
                }
                else
                {
                    Selection.activeObject = PrefabUtility.InstantiatePrefab(prefab as GameObject);
                }

                if (Selection.activeObject != null)
                {
                    PrefabUtility.UnpackPrefabInstance(Selection.activeGameObject, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
                    //Force position the instantiated prefab if pos are not set currectly on prefab settings.
                    Selection.activeTransform.localPosition = Vector3.zero;

                    Undo.RegisterCreatedObjectUndo(Selection.activeObject, $"Create {prefab.name} Object");
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