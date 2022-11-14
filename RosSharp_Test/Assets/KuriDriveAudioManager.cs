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
        KuriTransformManager kuriTransformManager;
        KuriTransformManager KuriT{
            get {
                if (kuriTransformManager == null) {
                    kuriTransformManager = KuriManager.instance.GetComponent<KuriTransformManager>();
                }
                return kuriTransformManager;
            }
        }

        #endregion
        #region unity
        private void FixedUpdate() {
            if (MyVelocity() > 0.001f) {
                if (!audioSRC.isPlaying) {
                    Play();
                }
            }
            else {
                if (audioSRC.isPlaying) {
                    Stop();
                }
            }    
        }
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
        Vector3 lastPose = Vector3.zero;
        Vector3 thisPose = Vector3.zero;
        float MyVelocity(){
            thisPose = KuriT.Position;
            float vel = (thisPose - lastPose).magnitude;
            lastPose = thisPose;
            return vel;
        }
        #endregion
    }
}
