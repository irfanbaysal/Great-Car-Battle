using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BulletControl : MonoBehaviour {

     void Start()
    {
              
    }
    private void OnTriggerEnter(Collider other)
    {
        PhotonNetwork.Destroy(gameObject);     
    }

   private IEnumerator bulletTimer()
    {
        yield return new WaitForSeconds(8);
        PhotonNetwork.Destroy(gameObject);
    }

     void Update()
    {
        StartCoroutine(bulletTimer());
        
    }


}

