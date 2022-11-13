using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using Newtonsoft.Json;

namespace NRISVTE {
    public class SetGoalPoseFromLastSrvMsg : ActionNode {
        ConnectionManager connectionManager;
        ConnectionManager ConnectionManager_ {
            get {
                if (connectionManager == null) {
                    connectionManager = ConnectionManager.instance;
                }
                return connectionManager;
            }
        }

        KuriTransformManager kuriT;
        KuriTransformManager KuriT_ {
            get {
                if (kuriT == null) {
                    kuriT = KuriManager.instance.GetComponent<KuriTransformManager>();
                }
                return kuriT;
            }
        }
        DebugTextManager debugTextManager;
        DebugTextManager DebugTextManager_ {
            get {
                if (debugTextManager == null) {
                    debugTextManager = DebugTextManager.instance;
                }
                return debugTextManager;
            }
        }
        protected override void OnStart() {
        }

        protected override void OnStop() {
        }

        protected override State OnUpdate() {
            Vector3 newGoal;
            List<float> kuriCordList = new List<float>();
            string lastMsg = ConnectionManager_.LatestMsg;
            if (lastMsg != null && !lastMsg.Contains("point_id")) {
                ServerPointResponseJSON serverPointResponseJSON = JsonConvert.DeserializeObject<ServerPointResponseJSON>(lastMsg);
                kuriCordList = serverPointResponseJSON.point;
                newGoal = new Vector3(kuriCordList[0], 0, kuriCordList[1]);
                // transform back into meters from cm
                newGoal *= 0.01f;
                // set on the ground
                newGoal.y = KuriT_.GroundYCord;
                blackboard.goalPosition = newGoal;
                DebugTextManager_.SetDebugText("Goal position: " + newGoal.ToString());
                return State.Success;
            }
            return State.Failure;
        }
    }
}
