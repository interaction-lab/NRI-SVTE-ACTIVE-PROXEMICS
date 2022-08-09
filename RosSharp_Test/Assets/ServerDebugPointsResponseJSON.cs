using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRISVTE {
    [System.Serializable]
    public class ServerDebugPointsResponseJSON {
        public string point_id;
        public List<List<float>> points;
    }
}
