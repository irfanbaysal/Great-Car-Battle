using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Com.TpicGaming.A;
public class Loot : MonoBehaviour
{
    PhotonView pv;
    public static GameObject instance;
    void Start()
    {
        transform.Rotate(0, 0, 40);
        pv = GetComponent<PhotonView>();
        if (pv.IsMine)
        {
            Loot.instance = this.gameObject;
        }

        DontDestroyOnLoad(this.gameObject);

    }


    void Update()
    {
        transform.Rotate(0,0.5f, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PhotonNetwork.Destroy(gameObject);

            GameManager.fuelcounter--;
            CharacterControl.fuel += 5;         
            
        }
    }
   
    

}
