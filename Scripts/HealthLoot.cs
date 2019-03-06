using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class HealthLoot : MonoBehaviour {

    PhotonView pv;
    public static GameObject inst;
    void Start ()
    {
       
        pv = GetComponent<PhotonView>();
        if (pv.IsMine)
        {
            HealthLoot.inst = this.gameObject;
        }
        DontDestroyOnLoad(this.gameObject);
    }

	void Update ()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            PhotonNetwork.Destroy(gameObject);          
            CharacterControl.health += 100;

        }
        
    }
}
