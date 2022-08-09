using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

namespace NRISVTE {
    public class VizSamplesButton : MonoBehaviour {
        #region members
        Button _button;
        public Button button {
            get {
                if (_button == null) {
                    _button = GetComponent<Button>();
                }
                return _button;
            }
        }
        #endregion
        #region unity
        void OnEnable() {
            if (ConnectionManager.instance.IsConnected) {
                button.interactable = true;
            }
            else {
                button.interactable = false;
            }
            button.onClick.AddListener(OnClick);
            ConnectionManager.instance.OnServerConnected.AddListener(OnServerConnected);
        }
        void OnDisable() {
            button.onClick.RemoveListener(OnClick);
            ConnectionManager.instance.OnServerConnected.RemoveListener(OnServerConnected);
        }
        #endregion
        #region public
        #endregion
        #region private
        void OnServerConnected() {
            button.interactable = true;
        }
        void OnClick() {
            ServerJSONManager.instance.RequestPhysicalVizPoint();
        }
        #endregion
    }
}
