using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRISVTE {
    public class ObjToPickUpJSONFileManager : Singleton<ObjToPickUpJSONFileManager> {
        #region members
        static string resourcePathToJSONs = "ObjectToPickUpFolder/Dialogue/";
        static string resourcePathToAudio = "ObjectToPickUpFolder/Audio/";
        Dictionary<string, DialogueJSON> _dialogueJSONs;
        // filename (lowercase) -> DialogueJSON
        Dictionary<string, DialogueJSON> DialogueJSONs {
            get {
                if (_dialogueJSONs == null) {
                    SetUpDialogueJSONs();
                }
                return _dialogueJSONs;
            }
        }
        Dictionary<string, AudioClip> _audioClips;
        Dictionary<string, AudioClip> AudioClips {
            get {
                if (_audioClips == null) {
                    SetUpAudioClips();
                }
                return _audioClips;
            }
        }
        #endregion
        #region unity
        private void Update() {
            if (Input.GetKeyDown(KeyCode.R)) {
                CheckAllJSONExists();
                Debug.Log("Checked all JSONs");
            }
        }
        #endregion
        #region public
        public DialogueJSON GetDialogueJSON(string objname) {
            // lowercase the objname and remove spaces
            objname = objname.ToLower().Replace(" ", "");
            if (DialogueJSONs.ContainsKey(objname)) {
                return DialogueJSONs[objname];
            }
            Debug.LogError("Did not find dialogue json for " + objname);
            return null;
        }
        public AudioClip GetAudioClip(string objname) {
            objname = objname.ToLower().Replace(" ", "");
            if (AudioClips.ContainsKey(objname)) {
                return AudioClips[objname];
            }
            Debug.LogWarning("Did not find audio clip for " + objname);
            return null;
        }

        #endregion
        #region private
        private void SetUpDialogueJSONs() {
            _dialogueJSONs = new Dictionary<string, DialogueJSON>();
            TextAsset[] files = Resources.LoadAll<TextAsset>(resourcePathToJSONs);
            foreach (TextAsset file in files) {
                _dialogueJSONs.Add(file.name.ToLower().Replace(" ", ""),
                    Newtonsoft.Json.JsonConvert.DeserializeObject<DialogueJSON>(file.text));
            }
        }
        private void SetUpAudioClips() {
            _audioClips = new Dictionary<string, AudioClip>();
            AudioClip[] files = Resources.LoadAll<AudioClip>(resourcePathToAudio);
            foreach (AudioClip file in files) {
                _audioClips.Add(file.name.ToLower().Replace(" ", ""), file);
            }
        }

        private void CheckAllJSONExists() {
            // get name of all children gameobjects
            List<string> objNames = new List<string>();
            foreach (Transform child in transform) {
                objNames.Add(child.name.ToLower().Replace(" ", ""));
                Debug.Log(child.name.ToLower().Replace(" ", ""));
            }
            // check that all json files exist as keys in the dictionary
            foreach (string objName in objNames) {
                if (!DialogueJSONs.ContainsKey(objName)) {
                    Debug.LogError("Did not find dialogue json for " + objName);
                }
            }
            // check all audio clips exist as keys in the dictionary
            foreach (string objName in objNames) {
                // check that audio for optiona, optionb, question, responsea, and responseb exist
                if (!AudioClips.ContainsKey(objName + "optiona")) {
                    Debug.LogError("Did not find audio clip for " + objName + "_optiona");
                }
                if (!AudioClips.ContainsKey(objName + "optionb")) {
                    Debug.LogError("Did not find audio clip for " + objName + "_optionb");
                }
                if (!AudioClips.ContainsKey(objName + "question")) {
                    Debug.LogError("Did not find audio clip for " + objName + "_question");
                }
                if (!AudioClips.ContainsKey(objName + "responsea")) {
                    Debug.LogError("Did not find audio clip for " + objName + "_responsea");
                }
                if (!AudioClips.ContainsKey(objName + "responseb")) {
                    Debug.LogError("Did not find audio clip for " + objName + "_responseb");
                }
            }
        }
        #endregion
    }
}