using System.Collections.Generic;
using UnityEngine;
using System.Text;

namespace UnityNATSNetwork
{
    public class ConnectionClient : Connection
    {
        public Queue<RemoteMessage> EventsReceived;

        public static ConnectionClient S;

        private new void Awake()
        {
            base.Awake();
            EventsReceived = new Queue<RemoteMessage>();

            if (S == null) S = this;
            else Debug.LogError("Connection duplicate attempt");
        }

        private new void OnDestroy()
        {
            base.OnDestroy();
        }

        protected override void Subscribe()
        {
            Debug.Log("Subscribing on " + RemoteEventSubject);
            NATSConnection.SubscribeAsync(RemoteEventSubject, (sender, args) =>
            {
                string stringReceived = Encoding.UTF8.GetString(args.Message.Data);
                EventsReceived.Enqueue(JsonUtility.FromJson<RemoteMessage>(stringReceived));
            });
        }

        public void SendCommand(RemoteMessage command)
        {
            Publish(command, RemoteCommandSubject);
        }
    }
}