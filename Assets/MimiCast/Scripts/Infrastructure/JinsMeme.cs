using System;
using System.Linq;
using MimiCast.Scripts.Entity;
using UniRx;
using UnityEngine;
using UnityEngine.Networking.PlayerConnection;
using WebSocketSharp;
using WebSocketSharp.Server;
using MessageEventArgs = WebSocketSharp.MessageEventArgs;

namespace MimiCast.Scripts.Infrastructure
{
    public class JinsMeme : Device
    {
        private WebSocketServer _webSocketServer;

        [SerializeField] private int port;
        
        private Subject<string> _subject = new Subject<string>();

        public bool IsServerActive
        {
            get
            {
                if (_webSocketServer == null)
                    return false;
                return _webSocketServer.IsListening;
            }
        }

        public void Start()
        {
            Debug.Log("start");
            _webSocketServer = new WebSocketServer(port);
            _webSocketServer.AddWebSocketService<JinsEcho>("/", echo =>
            {
                echo.Subject = _subject;
            });

            _subject.ObserveOnMainThread()
                .Subscribe(message =>
                {
                    var data = DeSerializeJsonString(message);
                    Receiver.OnNext(data);
                }).AddTo(this);
            _webSocketServer.Log.Level = LogLevel.Debug;
            Debug.Log("end");
        }

        public override void StartListen()
        {
            Debug.Log("StartListen");
            _webSocketServer.Start();
        }

        public override void StopListen()
        {
            _webSocketServer.Stop();
        }
        
        public void OnDestroy()
        {
            StopListen();
        }

        private JinsData DeSerializeJsonString(string jsonString)
        {
            if (jsonString == null)
                return null;
            var jinsData = JsonUtility.FromJson<JinsData>(jsonString);
            return jinsData;
        }
    }


    internal class JinsEcho : WebSocketBehavior
    {

        public Subject<string> Subject
        {
            private get;
            set;
        }

        protected override void OnOpen()
        {
            Debug.Log($"open");
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            Subject.OnNext(e.Data);
        }
    }
}