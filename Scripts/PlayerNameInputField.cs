using UnityEngine;
using UnityEngine.UI;


using Photon.Pun;
using Photon.Realtime;


using System.Collections;


namespace Com.TpicGaming.A
{
  
    [RequireComponent(typeof(InputField))]
    public class PlayerNameInputField : MonoBehaviour
    {
        #region Private Constants


       
        const string playerNamePrefKey = "PlayerName";


        #endregion


        #region MonoBehaviour CallBacks


      
        void Start()
        {


            string defaultName = string.Empty;
            InputField _inputField = this.GetComponent<InputField>();
            if (_inputField != null)
            {
                if (PlayerPrefs.HasKey(playerNamePrefKey))
                {
                    defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                    _inputField.text = defaultName;
                }
            }


            PhotonNetwork.NickName = defaultName;
        }


        #endregion


        #region Public Methods


        /// <param name="value">The name of the Player</param>
        public void SetPlayerName(string value)
        {
            // #Important
            if (string.IsNullOrEmpty(value))
            {
                Debug.LogError("Player Name is null or empty");
                return;
            }
            PhotonNetwork.NickName = value;


            PlayerPrefs.SetString(playerNamePrefKey, value);
        }


        #endregion
    }
}