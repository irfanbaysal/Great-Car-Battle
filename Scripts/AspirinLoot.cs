using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
namespace Com.TpicGaming.A
{

    public class AspirinLoot : MonoBehaviour
    {

        PhotonView pv;
        public static GameObject instance;
        void Start()
        {

            pv = GetComponent<PhotonView>();
            if (pv.IsMine)
            {
                Loot.instance = this.gameObject;
            }

            DontDestroyOnLoad(this.gameObject);

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                GameManager.aspirincounter--;
                PhotonNetwork.Destroy(gameObject);
                CharacterControl.health += 10;


            }
        }
    }
}