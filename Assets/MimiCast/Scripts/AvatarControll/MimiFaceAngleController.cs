
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

        public Quaternion FaceAngle { get; private set; }

        // 範囲外に移動した場合は0に戻す
        public void UpdateAngle(JinsData data)
        {
            Quaternion currentAngle = Quaternion.Euler(data.pitch, data.yaw, data.roll);
            Quaternion targetAngle = Quaternion.Euler(0, 0, 0);
            
            if (data.yaw is >= 80 and <= 300)
            {
                FaceAngle = Quaternion.Slerp(currentAngle, targetAngle, _elapsedTime);
                _elapsedTime += Time.deltaTime * 10;
            }
            else
            {
                FaceAngle = currentAngle;
                _elapsedTime = 0;
            }
        }
    }
}