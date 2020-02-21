using UnityEngine;
using NATS.Client;
using System.Text;

namespace UnityNATSNetwork
{
    public abstract class Connection : MonoBehaviour
    {
        public string NATSURL = "nats://127.0.0.1:4222";
        public string RemoteEventSubject = "Events";
        public string RemoteCommandSubject = "Commands";

        protected IConnection NATSConnection;

        protected void Awake()
        {
            Connect();
            Subscribe();
        }

        protected void OnDestroy()
        {
            NATSConnection.Close();
        }

        protected void OnDisable()
        {
            NATSConnection.Close();
        }

        private void Connect()
        {
            Debug.Log("Connectiong to NATS at " + NATSURL);

            Options opts = ConnectionFactory.GetDefaultOptions();
            opts.Url = NATSURL;

            NATSConnection = new ConnectionFactory().CreateConnection(opts);
        }

        protected abstract void Subscribe();

        public void Publish(RemoteMessage remoteMessage, string subject, bool flushImmediately = true)
        {
            string messageSerialized = JsonUtility.ToJson(remoteMessage);
            byte[] messageEncoded = Encoding.UTF8.GetBytes(messageSerialized);

            NATSConnection.Publish(subject, messageEncoded);

            if (flushImmediately)
            {
                NATSConnection.Flush();
            }

            Debug.Log("Message sent " + messageSerialized + ", subject " + subject);
        }
    }
}
