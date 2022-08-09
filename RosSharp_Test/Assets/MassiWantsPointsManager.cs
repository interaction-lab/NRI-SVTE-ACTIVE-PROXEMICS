using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace NRISVTE
{
    public class MassiWantsPointsManager : Singleton<MassiWantsPointsManager>
    {
        #region members
        // get the connection manager
        ConnectionManager _connectionManager;
        ConnectionManager connectionManager
        {
            get
            {
                if (_connectionManager == null)
                {
                    _connectionManager = ConnectionManager.instance;
                }
                return _connectionManager;
            }
        }
        KuriTransformManager kuriTransformManager;
        KuriTransformManager KuriT
        {
            get
            {
                if (kuriTransformManager == null)
                {
                    kuriTransformManager = KuriManager.instance.GetComponent<KuriTransformManager>();
                }
                return kuriTransformManager;
            }
        }
        #endregion
        #region unity
        private void Awake()
        {
            connectionManager.ReceivedMessageEvent.AddListener(OnReceivedMessage);
        }
        #endregion
        #region public
        #endregion
        #region private
        void OnReceivedMessage()
        {
            // get the message
            string msg = connectionManager.LatestMsg;
            if (!msg.Contains("point_id"))
            {
                return;
            }
            // parse the message
            ServerDebugPointsResponseJSON serverDebugPointsResponseJSON = JsonUtility.FromJson<ServerDebugPointsResponseJSON>(msg);
            // get the point id
            string point_id = serverDebugPointsResponseJSON.point_id;
            for (int i = 0; i < serverDebugPointsResponseJSON.points.Count; ++i)
            {
                float x = serverDebugPointsResponseJSON.points[i][0] / 100f + KuriT.Position.x;
                float y = KuriT.GroundYCord;
                float z = serverDebugPointsResponseJSON.points[i][1] / 100f + KuriT.Position.z;
                // create a sphere at the point
                // load massi_point prefab from Resources folder
                GameObject point = Instantiate(Resources.Load<GameObject>("massi_point"));
                // choose random color for the point
                Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                // set the color of the point
                point.GetComponent<Renderer>().material.color = color;
                point.transform.position = new Vector3(x, y, z);
                // get the text
                TextMeshProUGUI text = point.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                text.text = point_id + "-" + i;
                // set the text outline color to the color of the point
                text.outlineColor = color;
            }
        }
        #endregion
    }
}
