using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NRISVTE {
    public class ParticipantNumSlider : MonoBehaviour {
        #region members
        Slider _slider;
        Slider slider {
            get {
                if (_slider == null) {
                    _slider = GetComponent<Slider>();
                }
                return _slider;
            }
        }
        #endregion
        #region unity
        void OnEnable() {
            slider.onValueChanged.AddListener(OnValueChanged);
        }
        void OnDisable() {
            slider.onValueChanged.RemoveListener(OnValueChanged);
        }
        #endregion
        #region public
        #endregion
        #region private
        void OnValueChanged(float value) {
            UserIDManager.participantNumber = Mathf.Round(value).ToString();
        }
        #endregion
    }
}
