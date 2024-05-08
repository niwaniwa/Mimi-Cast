using System;
using MimiCast.Scripts.Entity;
using UniRx;
using UnityEngine;

namespace MimiCast.Scripts.Infrastructure
{
    public abstract class Device : MonoBehaviour
    {
        public IObservable<JinsData> OnJinsDataReceive => Receiver;

        protected readonly Subject<JinsData> Receiver = new Subject<JinsData>();

        public abstract void StartListen();
        public abstract void StopListen();

    }
}