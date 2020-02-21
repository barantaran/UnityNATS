using UnityEngine;
using UnityNATSNetwork;

namespace UnityNATSNetwork
{
    public class TestMessage : MonoBehaviour
    {
        private int testID;
 
        void Start()
        {
            testID = Random.Range(1, 10000);
            RemoteMessage testEvent = new RemoteMessage((int)RemoteMessageCode.TestEvent, testID);
            ConnectionClient.S.Publish(testEvent, ConnectionClient.S.RemoteEventSubject);
        }

        void Update()
        {
            if (ConnectionClient.S.EventsReceived.Count < 1) return;

            RemoteMessage currentMessage = ConnectionClient.S.EventsReceived.Dequeue();
            if(currentMessage.RemoteMessageCode == (int)RemoteMessageCode.TestEvent && currentMessage.ObjectID == testID)
            {
                Debug.Log("Test message received!");
            }
        }
    }
}
