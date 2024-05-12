using System;
using MimiCast.Scripts.Adapter;
using UnityEngine;
using UnityEngine.UIElements;

namespace MimiCast.Scripts.UseCase
{
    public class UserInterfaceController : MonoBehaviour
    {
        private Button loadAvatarButton;
        private Button helpButton;
        private Button calibrationButton;
        private Button connectSettingButton;

        [SerializeField] private MimiAvatarConnector connecter;
        [SerializeField] private UserAvatarLoader loader;
        [SerializeField] private UIDocument uiDocument;
        
        public void Start()
        {
            loadAvatarButton = uiDocument.rootVisualElement.Q<Button>("LoadButton");
            // helpButton = uiDocument.rootVisualElement.Q<Button>("LoadButton");
            calibrationButton = uiDocument.rootVisualElement.Q<Button>("CalibrationButton");
            // connectSettingButton = uiDocument.rootVisualElement.Q<Button>("LoadButton");
            
            loadAvatarButton.clickable.clicked += LoadAvatar;
            // helpButton.clickable.clicked += ShowHelp;
            calibrationButton.clickable.clicked += Calibrate;
            // connectSettingButton.clickable.clicked += ConnectSettings;
            
            connecter.StartProcess();
        }

        private async void LoadAvatar()
        {
            Debug.Log("load avatar button");
            var avatar = await loader.LoadAvatar();
            connecter.ApplyAvatar(avatar);
        }

        private void ShowHelp()
        {

        }

        private void Calibrate()
        {
            Debug.Log("calibrate avatar button");
            connecter.Calibration();
        }

        private void ConnectSettings()
        {

        }

        void OnDestroy()
        {
            loadAvatarButton.clickable.clicked -= LoadAvatar;
            // helpButton.clickable.clicked -= ShowHelp;
            calibrationButton.clickable.clicked -= Calibrate;
            // connectSettingButton.clickable.clicked -= ConnectSettings;
        }
    }
}