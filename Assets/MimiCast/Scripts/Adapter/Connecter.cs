using System;
using MimiCast.Scripts.AvatarControll;
using MimiCast.Scripts.Entity;
using MimiCast.Scripts.Infrastructure;
using UniRx;
using UnityEngine;
using VrmLib;

namespace MimiCast.Scripts.Adapter
{
    public class Connecter : MonoBehaviour
    {
        [SerializeField] private Device device;
        [SerializeField] private MimiAvatar _avatar;
        [SerializeField] private MimiFaceAngleController faceAngleController;
        [SerializeField] private MimiFaceToBodyController faceToBodyController;
        
        public bool IsEndCalibration { get; private set; }
        
        private void Start()
        {
            device.OnJinsDataReceive.Subscribe(SynchronizeAvatar);
            device.StartListen();
        }

        private void SynchronizeAvatar(JinsData data)
        {
            if (_avatar == null) return;
            if (!IsEndCalibration) return;
            var neck = _avatar.Neck;
            var chest = _avatar.Chest;
            // Debug.Log($"{data.pitch}, {data.yaw}, {data.roll}");
            faceAngleController.UpdateAngle(data);
            faceToBodyController.UpdateAngle(data, faceAngleController.FaceAngle);
            
            
            neck.rotation = faceAngleController.FaceAngle;
            chest.rotation = faceToBodyController.BodyAngle;
            // Debug.Log($"{neck.rotation.x}, {neck.rotation.y}, {neck.rotation.z}");
        }

        public void FixedUpdate()
        {
            
        }

        public void ApplyAvatar(MimiAvatar avatar)
        {
            _avatar = avatar;
        }

        public void Calibration()
        {
            IsEndCalibration = true;
        }

        public void Dispose()
        {
            
        }
        
    }
}