using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRISVTE {
    public class InteractionManager : Singleton<InteractionManager> {
        #region members
        LoggingManager _loggingManager;
        LoggingManager loggingManager {
            get {
                if (_loggingManager == null) {
                    _loggingManager = LoggingManager.instance;
                }
                return _loggingManager;
            }
        }
        public enum SampleTypes {
            active,
            random
        }

        float interactionLengthInMinutes = 20f;
        public SampleTypes CurrentSampleType;
        public static string sampleTypeLogColumn = "sampleType";
        #endregion
        #region unity
        private void Start() {
            loggingManager.AddLogColumn(sampleTypeLogColumn, "");
            CurrentSampleType = SampleTypes.random; // toggle default is off
        }
        #endregion
        #region public

        #endregion
        #region private
        public void StartInteraction() {
            loggingManager.AddLogColumn(sampleTypeLogColumn, CurrentSampleType.ToString());
            StartCoroutine(SwitchSampleTypeAtTime());
        }
        IEnumerator SwitchSampleTypeAtTime() {
            Debug.Log("start interaction");
            float conditionTime = interactionLengthInMinutes * 60f / 2.0f;
            WaitForSecondsRealtime conditionWait = new WaitForSecondsRealtime(conditionTime);
            yield return conditionWait;
            Debug.Log("switch conditions");
            if (CurrentSampleType == SampleTypes.active) {
                CurrentSampleType = SampleTypes.random;
            }
            else {
                CurrentSampleType = SampleTypes.active;
            }
            loggingManager.UpdateLogColumn(sampleTypeLogColumn, CurrentSampleType.ToString());
            yield return conditionWait;
            Debug.Log("done");
            Application.Quit();
        }
        #endregion
    }
}
