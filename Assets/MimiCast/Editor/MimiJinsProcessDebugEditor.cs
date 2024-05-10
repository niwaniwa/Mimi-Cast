using MimiCast.Scripts.UseCase;
using UnityEditor;
using UnityEngine;

namespace MimiCast.Editor
{
    [CustomEditor(typeof(MimiJinsProcessDebugger))]
    public class MimiJinsProcessDebugEditor : UnityEditor.Editor
    {
        private MimiJinsProcessDebugger _debugger;

        private SerializedProperty _path;

        public void OnEnable()
        {
            _debugger = target as MimiJinsProcessDebugger;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            serializedObject.Update();

            if (!EditorApplication.isPlaying)
            {
                EditorGUILayout.HelpBox("以下の項目はPlayモードのみでの実行可能です。", MessageType.Warning);
            }
            
            EditorGUI.BeginDisabledGroup(!EditorApplication.isPlaying);
            
            if (GUILayout.Button("start listen")){
                _debugger._avatarConnector.StartProcess();
            } 
            
            if (GUILayout.Button("stop listen")){
                _debugger._avatarConnector.StopProcess();
            } 
            
            if (GUILayout.Button("キャリブレーション")){
                _debugger._avatarConnector.Calibration();
            } 
            
            EditorGUI.EndDisabledGroup();
            
            
        }
    }
}