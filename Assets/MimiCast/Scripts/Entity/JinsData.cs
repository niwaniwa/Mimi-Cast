using System;

namespace MimiCast.Scripts.Entity
{
    [Serializable]
    public class JinsData
    {
        public int sequenceNumber;
        public bool noiseStatus;
        public float eyeMoveRight;
        public float eyeMoveLeft;
        public float eyeMoveDown;
        public float eyeMoveUp;
        public float yaw;
        public float roll;
        public float pitch;
        public bool walking;
        public float fitError;
        public float powerLeft;
        public float blinkStrength;
        public float blinkSpeed;
        public float accX;
        public float accY; // UnityとJinsだと軸が違うよ
        public float accZ;

    }
}