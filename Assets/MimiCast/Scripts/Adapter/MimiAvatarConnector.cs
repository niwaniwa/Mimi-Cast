using System;
using MimiCast.Scripts.AvatarControll;
using MimiCast.Scripts.Entity;
using MimiCast.Scripts.Infrastructure;
using UniRx;
using UnityEngine;
using VrmLib;

namespace MimiCast.Scripts.Adapter
{
    public class MimiAvatarConnector : MonoBehaviour
    {
        [SerializeField] private Device device;
        [SerializeField] private MimiFaceAngleController faceAngleController;
        [SerializeField] private MimiFaceToBodyController faceToBodyController;
        
        private MimiAvatar _avatar;
        private bool isProcessing = true;
        private JinsData _lastData, _calibrationData;
        
        public bool IsEndCalibration { get; private set; }

        public JinsData CalibrationData
        {
            get => _calibrationData;
            private set
            {
                _calibrationData = value;
                faceAngleController.AnchorData = _calibrationData;
                faceToBodyController.AnchorData = _calibrationData;
            }
        }
  
        private void Start()
        {
            device.OnJinsDataReceive.Subscribe(SynchronizeAvatar).AddTo(this);
            device.OnListenStopSubject.Subscribe(_ => Dispose()).AddTo(this);
            device.OnListenStopSubject.Subscribe(_ =>
            {
                faceAngleController.ResetDefaultAngle(2, 
                    Quaternion.Euler(_lastData.pitch, _lastData.yaw, _lastData.roll),
                    Quaternion.identity, AnimationCurve.EaseInOut(0f, 0f, 1f, 1f),
                    new ());
            }).AddTo(this);
        }

        // Jinsmemeからのデータを同期
        private void SynchronizeAvatar(JinsData data)
        {

            _lastData = data;
            
            if (_avatar == null) return;
            if (!IsEndCalibration) return;

            faceAngleController.UpdateAngle(data);
            faceToBodyController.UpdateAngle(data, faceAngleController.FaceAngle);
            
            _avatar.Neck.rotation = faceAngleController.FaceAngle;
            _avatar.Chest.rotation = faceToBodyController.BodyAngle;

        }

        public void FixedUpdate()
        {
            if (isProcessing) return;
        }

        public void ApplyAvatar(MimiAvatar avatar)
        {
            _avatar = avatar;
        }

        public bool Calibration()
        {
            if (_lastData == null) return false;
            CalibrationData = _lastData;
            IsEndCalibration = true;
            return true;
        }

        public void StartProcess()
        {
            device.StartListen();
        }

        public void StopProcess()
        {
            device.StopListen();
        }

        public void Dispose()
        {
            _avatar.Dispose();
            _avatar = null;
        }
        
    }
}