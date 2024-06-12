using MimiCast.Scripts.Entity;
using UnityEngine;

namespace MimiCast.Scripts.AvatarControll
{
    public class MimiBodyController : MonoBehaviour
    {
        
        public Quaternion LeftShoulderAngle { get; private set; }
        public Quaternion RightShoulderAngle { get; private set; }
        
        public void Start()
        {
        }
        
        public void UpdateAngle()
        {
            LeftShoulderAngle = Quaternion.Euler(new Vector3(0,0,50));
            RightShoulderAngle = Quaternion.Euler(new Vector3(0,0,-50));
        }
    }
}