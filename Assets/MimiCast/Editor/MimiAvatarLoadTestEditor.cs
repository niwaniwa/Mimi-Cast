using System;
using MimiCast.Scripts.UseCase;
using MimiCast.Scripts.Utility;
using UnityEditor;
using UnityEngine;

namespace MimiCast.Editor
{
    [CustomEditor(typeof(MimiAvatarLoadTester))]
    public class MimiAvatarLoadTestEditor : UnityEditor.Editor
    {
        private MimiAvatarLoadTester _loader;

        private SerializedProperty _path;

        public void OnEnable()
        {
            _loader = target as MimiAvatarLoadTester;;
            _path = serializedObject.FindProperty(nameof(MimiAvatarLoadTester.path));
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            serializedObject.Update();

            if (!EditorApplication.isPlaying)
            {
                EditorGUILayout.HelpBox("Playモードのみでの実行可能です。", MessageType.Error);
                return;
            }
            
            if (GUILayout.Button("Select Avatar")){
                //パスの取得
                var path = EditorUtility.OpenFilePanel("Open vrm", "", "vrm");
                if (string.IsNullOrEmpty(path))
                    return;
                Debug.Log(path);
                _path.stringValue = path;
                serializedObject.ApplyModifiedProperties();
                _loader.Load();
            }  
            
            
        }
        
    }
}