using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Com.TpicGaming.A;

public class CharacterControl : MonoBehaviourPunCallbacks {
    public static float speed=15;
    public float rotaSpeed = 10;
    public static float fuel = 100;
    float x;
    float z;
    public static GameObject LocalPlayerInstance;
    PhotonView pv;
    public GameObject Bullet;
    public Transform bulletPos;
    public static float health = 100;
    public Text text;
    public Text fuelText;
    public Text deathText;
    public Text bulletText;
    public static int bulletCounter = 30;
    public ParticleSystem muzzle;
    // public static bool bulletReady;
   
   
    void Awake()
    {
        pv = GetComponent<PhotonView>();
     
        if(pv.IsMine)
{
            CharacterControl.LocalPlayerInstance = this.gameObject;
        }
       
        DontDestroyOnLoad(this.gameObject);
    }
    void Start ()
    {
      
	}

    void OnTriggerEnter(Collider other)
    {
        if (!photonView.IsMine)
        {
            return;
        }
      
        if (!other.name.Contains("bullet"))
        {
            return;
        }

        health -= 10f;



    
      
    }
   

    void Update ()
    {
        if (pv.IsMine)
        {
            CharMovement();
            Fire();
          
            fuelControl();
            text.text = "health   " + health;
            fuelText.text =  " fuel " + (int)fuel;
            bulletText.text = "bullet " + bulletCounter;
           
           
            
        }
        if (health <= 0 || fuel <=0)
        {
            
            deathText.text = "YOU HAVE BEEN SLAIN ";
           
            health = 0;
            fuel = 0;

            x = Input.GetAxis("Horizontal") * Time.deltaTime * 0f;
            z = Input.GetAxis("Vertical") * Time.deltaTime * 0f;

            transform.Rotate(0, x, 0);
            transform.Translate(0, 0, z);
           StartCoroutine(deathTextTime());
        }
       
          
        
      
        
      
        if (fuel >= 100)
        {
            fuel = 100;
        }
        if (health >= 100)
        {
            health = 100;
        }
       

        

   
    }

    private IEnumerator deathTextTime()
    {
        yield return new WaitForSeconds(3);
        GameManager.Instance.LeaveRoom();
      
       
    }
    

    void fuelControl()
    {
        if (Input.GetKey(KeyCode.W))
        {
            fuel-=1.5f * Time.deltaTime;
            
        }
    }

    void CharMovement()
    {
        Camera.main.transform.position = this.transform.position - this.transform.up * -20f;

        
         x = Input.GetAxis("Horizontal") * Time.deltaTime * 150f;
         z = Input.GetAxis("Vertical") * Time.deltaTime * 15f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z); 


    }
  [PunRPC]
    void Fire()
    {
        if (Input.GetMouseButtonDown(0) && bulletCounter!=0)
        {
            bulletCounter--;
            GetComponent<PhotonView>().RPC("muzzleEffects", RpcTarget.All , null);
            Debug.LogWarning("bulletcounter" + bulletCounter);
            GameObject Bullet = PhotonNetwork.Instantiate("bullet", bulletPos.position,transform.rotation, 0);
            Bullet.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * 30f);
           
        }
    }
    

    [PunRPC]
    void muzzleEffects()
    {
        muzzle.Play();
    }
}
