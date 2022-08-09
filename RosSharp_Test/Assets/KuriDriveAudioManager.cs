using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRISVTE {
    public class KuriDriveAudioManager : Singleton<KuriDriveAudioManager> {
        #region members
        AudioSource _src;
        AudioSource audioSRC {
            get {
                if (_src == null) {
                    _src = GetComponent<AudioSource>();
                }
                return _src;
            }
        }
        #endregion
        #region unity
        #endregion
        #region public
        public void Play() {
            audioSRC.loop = true;
            audioSRC.Play();
        }
        public void Stop() {
            audioSRC.Stop();
        }
        #endregion
        #region private
        #endregion
    }
}
