
using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MimiCast.Scripts.Entity;
using UnityEngine;

namespace MimiCast.Scripts.AvatarControll
{
    /// <summary>
    /// Jins meme(現状)から流れてきたデータを正規化する。またキャリブレーションも担当する
    /// </summary>
    public class MimiFaceAngleController : MonoBehaviour

    {

        private float _elapsedTime = 0;
        private bool isProcessingDefaultAngle = false;

        public Quaternion FaceAngle { get; private set; }

        public JinsData AnchorData { get; set; } 

        private CancellationTokenSource _cancellationTokenSource;

        public void Start()
        {
            AnchorData = JinsData.Default;
        }

        /// <summary>
        /// FaceAngleに対してJinsDataを用いて角度を計算し同期させる処理
        /// 範囲外に移動した場合は0に戻す
        /// </summary>
        /// <param name="data"></param>
        public void UpdateAngle(JinsData data)
        {
            // use calibration
            float adjustedYaw = NormalizeYawAngle(data.yaw - AnchorData.yaw);
            float adjustedPitch = NormalizePitchAngle(data.pitch - AnchorData.pitch);
            float adjustedRoll = NormalizeRollAngle(data.roll - AnchorData.roll);
            
            Quaternion currentAngle = Quaternion.Euler(adjustedPitch, adjustedYaw, adjustedRoll);
            Quaternion targetAngle = Quaternion.Euler(0, 0, 0);
            
            // 一定以上後ろを向くと自動的に前を向くように
            if (adjustedYaw is >= 80 and <= 300)
            {
                if (!isProcessingDefaultAngle)
                {
                    isProcessingDefaultAngle = true;
                    _cancellationTokenSource = new ();
                    _ = ResetDefaultAngle(0.5f, currentAngle, targetAngle, AnimationCurve.EaseInOut(0f, 0f, 1f, 1f), _cancellationTokenSource.Token);
                }
                    
            }
            else
            {
                if (isProcessingDefaultAngle)
                {
                    isProcessingDefaultAngle = false;
                    _cancellationTokenSource?.Cancel();
                    _cancellationTokenSource?.Dispose();
                }
                FaceAngle = currentAngle;
            }
        }

        /// <summary>
        /// 初期位置に戻すメソッド。cancellationTokenでキャンセルする
        /// </summary>
        /// <param name="time">fade time(seconds).</param>
        /// <param name="currentAngle">初期位置</param>
        /// <param name="targetAngle">移動させたい位置</param>
        /// <param name="curve">fade type</param>
        /// <param name="cancellationToken">cancel token</param>
        public async UniTask ResetDefaultAngle(float time, Quaternion currentAngle, Quaternion targetAngle, AnimationCurve curve, CancellationToken cancellationToken)
        {
            var startTime = Time.time;
            while (Time.time < startTime + time)
            {
                if (cancellationToken.IsCancellationRequested) return;
                
                var ration = curve.Evaluate(Mathf.Lerp(0f, 1f, (Time.time - startTime) / time));
                FaceAngle = Quaternion.Slerp(currentAngle, targetAngle, ration);
                await UniTask.DelayFrame(1, cancellationToken: cancellationToken);
            }
        }
        
        private float NormalizeYawAngle(float angle)
        {
            return (angle + 360) % 360;
        }
        
        private float NormalizePitchAngle(float angle)
        {
            return angle;
        }
        
        private float NormalizeRollAngle(float angle)
        {
            return (angle + 180) % 360 - 180;
        }
    }
}