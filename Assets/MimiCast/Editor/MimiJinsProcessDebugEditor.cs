using System.Threading.Tasks;
using MimiCast.Scripts.UseCase;
using UnityEditor;
using UnityEngine;

namespace MimiCast.Editor
{
    [CustomEditor(typeof(MimiJinsProcessDebugger))]
    public class MimiJinsProcessDebugEditor : UnityEditor.Editor
    {
        private MimiJinsProcessDebugger _debugger;

        public void OnEnable()
        {
            _debugger = target as MimiJinsProcessDebugger;
        }

        public override async void OnInspectorGUI()
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
            
            if (GUILayout.Button("Select Avatar"))
            {
                var avatar = await _debugger._userAvatarLoader.LoadAvatar();
                if (avatar == null) return;
                _debugger._avatarConnector.Dispose();
                _debugger._avatarConnector.ApplyAvatar(avatar);
            }
            
            EditorGUI.EndDisabledGroup();
            
            
        }
    }
}