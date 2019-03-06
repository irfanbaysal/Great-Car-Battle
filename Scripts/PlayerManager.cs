using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

 
 
public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
{

    PhotonView PV;
    Vector3 position;
    Quaternion rotation;
    float delay = 10;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }


    void Start()
    {

    }

    void Update()
    {
        if (!PV.IsMine)
        {
            transform.position = Vector3.Lerp(transform.position, position, delay * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, delay * Time.deltaTime);
        }
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            position = (Vector3)stream.ReceiveNext();
            rotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
