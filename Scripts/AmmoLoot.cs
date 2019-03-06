using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Com.TpicGaming.A
{
    public class AmmoLoot : MonoBehaviour
    {

        PhotonView pv;
        public static GameObject inst;
        void Start()
        {

            pv = GetComponent<PhotonView>();
            if (pv.IsMine)
            {
                HealthLoot.inst = this.gameObject;
            }
            DontDestroyOnLoad(this.gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                GameManager.ammocounter--;
                PhotonNetwork.Destroy(gameObject);             
                CharacterControl.bulletCounter += 30;

            }

        }
    }
}