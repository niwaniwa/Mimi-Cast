using System;
using MimiCast.Scripts.Adapter;
using UniRx;
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
        private Button exitButton;

        [SerializeField] private MimiAvatarConnector connecter;
        [SerializeField] private UserAvatarLoader loader;
        [SerializeField] private UIDocument uiDocument;
        [SerializeField] private Canvas splash;
        [SerializeField] private UnityEngine.UI.Image splashLogo;
        [SerializeField] private UnityEngine.UI.Image background;
        
        public void Start()
        {
            loadAvatarButton = uiDocument.rootVisualElement.Q<Button>("LoadButton");
            // helpButton = uiDocument.rootVisualElement.Q<Button>("LoadButton");
            calibrationButton = uiDocument.rootVisualElement.Q<Button>("CalibrationButton");
            // connectSettingButton = uiDocument.rootVisualElement.Q<Button>("LoadButton");
            exitButton = uiDocument.rootVisualElement.Q<Button>("ExitButton");
            
            loadAvatarButton.clickable.clicked += LoadAvatar;
            // helpButton.clickable.clicked += ShowHelp;
            calibrationButton.clickable.clicked += Calibrate;
            // connectSettingButton.clickable.clicked += ConnectSettings;
            exitButton.clickable.clicked += OnExitButton;
            
            connecter.StartProcess();

            StartScene();
        }

        private void StartScene()
        {
            splashLogo.color = new Color(splashLogo.color.r, splashLogo.color.g, splashLogo.color.b, 0);
            Observable.Timer(TimeSpan.FromSeconds(1))
                .SelectMany(_ => Observable.EveryUpdate()
                    .TakeUntil(Observable.Timer(TimeSpan.FromSeconds(1)))
                    .Select(_ => Time.deltaTime / 1) 
                    .Scan(0f, (acc, deltaTime) => Mathf.Min(acc + deltaTime, 1f)))
                .Subscribe(alpha =>
                {
                    splashLogo.color = new Color(splashLogo.color.r, splashLogo.color.g, splashLogo.color.b, alpha);
                })
                .AddTo(this);
            
            Observable.Timer(TimeSpan.FromSeconds(3))
                .SelectMany(_ => Observable.EveryUpdate()
                    .TakeUntil(Observable.Timer(TimeSpan.FromSeconds(1)))
                    .Select(_ => Time.deltaTime / 1) 
                    .Scan(1f, (acc, deltaTime) => Mathf.Max(acc - deltaTime, 0f)))
                .Subscribe(alpha =>
                {
                    background.color = new Color(background.color.r, background.color.g, background.color.b, alpha);
                })
                .AddTo(this);
            
            Observable.Timer(TimeSpan.FromSeconds(4))
                .SelectMany(_ => Observable.EveryUpdate()
                    .TakeUntil(Observable.Timer(TimeSpan.FromSeconds(1)))
                    .Select(_ => Time.deltaTime / 1) 
                    .Scan(1f, (acc, deltaTime) => Mathf.Max(acc - deltaTime, 0f)))
                .Subscribe(alpha =>
                {
                    splashLogo.color = new Color(splashLogo.color.r, splashLogo.color.g, splashLogo.color.b, alpha);
                })
                .AddTo(this);

            Observable.Timer(TimeSpan.FromSeconds(5))
                .SelectMany(_ => Observable.EveryUpdate())
                .Subscribe(_ => splash.gameObject.SetActive(false));
        }

        private async void LoadAvatar()
        {
            Debug.Log("load avatar button");
            var avatar = await loader.LoadAvatar();
            if (avatar == null) return;
            connecter.Dispose();
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
        
        private void OnExitButton()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
            Application.Quit();
#endif
        }

        void OnDestroy()
        {
            loadAvatarButton.clickable.clicked -= LoadAvatar;
            // helpButton.clickable.clicked -= ShowHelp;
            calibrationButton.clickable.clicked -= Calibrate;
            // connectSettingButton.clickable.clicked -= ConnectSettings;
            exitButton.clickable.clicked -= ConnectSettings;
        }
    }
}