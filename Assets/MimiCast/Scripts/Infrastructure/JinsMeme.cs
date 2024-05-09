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
        
        private readonly Subject<string> _onMessage = new();
        private readonly Subject<Unit> _onClose = new();

        public bool IsServerActive
        {
            get
            {
                if (_webSocketServer == null)
                    return false;
                return _webSocketServer.IsListening;
            }
        }

        public void Awake()
        {
            _webSocketServer = new WebSocketServer(port);
            _webSocketServer.AddWebSocketService<JinsEcho>("/", echo =>
            {
                echo.OnMessageSubject = _onMessage;
                echo.OnCloseSubject = _onClose;
            });

            _onMessage.ObserveOnMainThread()
                .Subscribe(message =>
                {
                    var data = DeSerializeJsonString(message);
                    OnMessageReceiveSubject.OnNext(data);
                }).AddTo(this);
            
            _onClose.ObserveOnMainThread()
                .Subscribe(OnListenStopSubject.OnNext).AddTo(this);
            _webSocketServer.Log.Level = LogLevel.Debug;
        }

        public override void StartListen()
        {
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

        public Subject<string> OnMessageSubject
        {
            private get;
            set;
        }
        
        public Subject<Unit> OnCloseSubject
        {
            private get;
            set;
        }

        protected override void OnOpen()
        {
            Debug.Log($"open");
        }

        protected override void OnClose(CloseEventArgs e)
        {
            OnCloseSubject.OnNext(Unit.Default);
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            OnMessageSubject.OnNext(e.Data);
        }
    }
}