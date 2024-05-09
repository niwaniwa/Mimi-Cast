using System;
using MimiCast.Scripts.Entity;
using UniRx;
using UnityEditor.VersionControl;
using UnityEngine;

namespace MimiCast.Scripts.Infrastructure
{
    public abstract class Device : MonoBehaviour
    {
        public IObservable<JinsData> OnJinsDataReceive => OnMessageReceiveSubject;
        public IObservable<Unit> OnListenStop => OnListenStopSubject;

        internal readonly Subject<JinsData> OnMessageReceiveSubject = new Subject<JinsData>();
        internal readonly Subject<Unit> OnListenStopSubject = new Subject<Unit>();

        public abstract void StartListen();
        public abstract void StopListen();

    }
}