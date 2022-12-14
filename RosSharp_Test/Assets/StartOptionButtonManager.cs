using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NRISVTE {
    public class StartOptionButtonManager : Singleton<StartOptionButtonManager> {
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
        KuriBTEventRouter _kuriBTEventRouter;
        KuriBTEventRouter kuriBTEventRouter {
            get {
                if (_kuriBTEventRouter == null) {
                    _kuriBTEventRouter = KuriManager.instance.GetComponent<KuriBTEventRouter>();
                }
                return _kuriBTEventRouter;
            }
        }
        #endregion
        #region unity
        void Awake() {
            kuriBTEventRouter.AddEvent(EventNames.StartButtonPressed, button.onClick);
            // check if participant number is valid (between 0-40)
            // subscriber to connection manager's OnServerConnected event
            if (ConnectionManager.instance.IsConnected) {
                OnServerConnected();
            }
            else {
                button.interactable = false;
            }
            ConnectionManager.instance.OnServerConnected.AddListener(OnServerConnected);
            button.onClick.AddListener(OnClick);
        }
        private void OnDestroy() {
            ConnectionManager.instance.OnServerConnected.RemoveListener(OnServerConnected);
        }
        #endregion
        #region public
        #endregion
        #region private
        private void OnServerConnected() {
            button.interactable = true;
        }
        private void OnClick() {
            InteractionManager.instance.StartInteraction();
            // turn off options button
            OptionsButtonManager.instance.button.interactable = false;
        }
        #endregion
    }
}
