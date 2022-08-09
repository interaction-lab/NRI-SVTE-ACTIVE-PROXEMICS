using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        TextMeshProUGUI _textMeshProUGUI;
        TextMeshProUGUI textMeshProUGUI {
            get {
                if (_textMeshProUGUI == null) {
                    _textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
                }
                return _textMeshProUGUI;
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
            UserIDManager.participantNumber = (int)Mathf.Round(value);
            textMeshProUGUI.text = "Participant#" + UserIDManager.participantNumber.ToString();
        }
        #endregion
    }
}
