using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityNATSNetwork
{
    public enum RemoteMessageCode
    {
        TestCommand,
        TestEvent
    };

    public struct RemoteMessage
    {
        public int RemoteMessageCode;
        public int ObjectID;
        public Vector3 ObjectPosition;
        public Vector3 WaypointPosition;

        public RemoteMessage(int messageCode, int objectID) : this()
        {
            RemoteMessageCode = messageCode;
            ObjectID = objectID;
        }
    }
}
