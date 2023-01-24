﻿// Copyright (c) 2020 JioGlass. All Rights Reserved.

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace JMRSDK.Toolkit.UI.Inspector
{
    [CustomEditor(typeof(Slider))]
    public class JMRUISliderInspector : Editor
    {
        #region Editor Actions

        const string PrefabGUID = "b0dcc6e2292f83140ac41073df671d49";

        private static string PrefabPath => AssetDatabase.GUIDToAssetPath(PrefabGUID);

        [MenuItem("JioMixedReality/Toolkits/V1/Slider")]
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