using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NRISVTE {
    public class ActiveSampleOptionToggleButtonManager : MonoBehaviour {
        #region members
        Toggle _toggle;
        Toggle toggle {
            get {
                if (_toggle == null) {
                    _toggle = GetComponent<Toggle>();
                }
                return _toggle;
            }
        }
        InteractionManager _interactionManager;
        InteractionManager InteractionManager_ {
            get {
                if (_interactionManager == null) {
                    _interactionManager = InteractionManager.instance;
                }
                return _interactionManager;
            }
        }
        #endregion
        #region unity
        void OnEnable() {
            toggle.onValueChanged.AddListener(OnValueChanged);
        }
        void OnDisable() {
            toggle.onValueChanged.RemoveListener(OnValueChanged);
        }
        #endregion
        #region public
        #endregion
        #region private
        void OnValueChanged(bool isOn) {
            if (isOn) {
                InteractionManager_.CurrentSampleType = InteractionManager.SampleTypes.active;
            }
            else {
                InteractionManager_.CurrentSampleType = InteractionManager.SampleTypes.random;
            }
        }
        #endregion
    }
}
