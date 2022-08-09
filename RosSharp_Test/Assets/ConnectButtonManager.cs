using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace NRISVTE {
    public class ConnectButtonManager : MonoBehaviour {
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
        ConnectionManager _connectionManager;
        ConnectionManager connectionManager {
            get {
                if (_connectionManager == null) {
                    _connectionManager = ConnectionManager.instance;
                }
                return _connectionManager;
            }
        }
        #endregion
        #region unity
        void OnEnable() {
            button.onClick.AddListener(OnClick);
            connectionManager.OnServerConnected.AddListener(OnServerConnected);
        }
        void OnDisable() {
            button.onClick.RemoveListener(OnClick);
            connectionManager.OnServerConnected.RemoveListener(OnServerConnected);
        }
        #endregion
        #region private
        private void OnClick() {
            ConnectionManager.instance.Connect();
        }
        private void OnServerConnected() {
            button.interactable = false;
            // change button text to connected
            button.GetComponentInChildren<TextMeshProUGUI>().text = "Connected";
            // change button normal color to green
            button.GetComponent<Image>().color = Color.green;
        }
        #endregion
    }
}
