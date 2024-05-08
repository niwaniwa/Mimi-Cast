using System;
using MimiCast.Scripts.Infrastructure;
using UniRx;
using UnityEngine;

namespace MimiCast.Scripts.Adapter
{
    public class Connecter : MonoBehaviour
    {
        [SerializeField] private Device device;
        
        
        private void Start()
        {
            device.OnJinsDataReceive.Subscribe(data => Debug.Log($"{data.pitch}"));
            device.StartListen();
        }
        
    }
}