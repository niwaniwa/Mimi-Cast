using MimiCast.Scripts.Entity;
using UnityEngine;

namespace MimiCast.Scripts.AvatarControll
{
    public class MimiFaceToBodyController : MonoBehaviour
    {
        // faceからどれだけ動きを伝播させるか
        private float factor = 0.3f;
        
        public JinsData AnchorData { get; set; } 
        
        public Quaternion BodyAngle
        {
            get;
            private set;
        }

        // faceの値とfactorの値でBody(Chest)をどれくらい動かすか決定
        public void UpdateAngle(JinsData data, Quaternion faceQuaternion)
        {
            BodyAngle = Quaternion.Slerp(Quaternion.identity, faceQuaternion, factor);
        }

        
    }
}