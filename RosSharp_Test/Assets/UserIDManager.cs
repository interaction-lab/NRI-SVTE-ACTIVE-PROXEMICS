using System;
using UnityEngine;

namespace NRISVTE {
    public class UserIDManager : Singleton<UserIDManager> {
        public static string EmbeddedDataForID = "SID";
        static string playerIDBackingVar = "";
        public static int participantNumber = 100;
        public static string PlayerId {
            get {
                if (playerIDBackingVar == "") {
                    playerIDBackingVar = Guid.NewGuid().ToString();
                }
                return playerIDBackingVar + "_" + participantNumber.ToString();
            }
        }
        public static string DeviceId {
            get {
                return SystemInfo.deviceUniqueIdentifier;
            }
        }
    }
}