using System.Collections;


using UnityEngine;
using UnityEngine.SceneManagement;


using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

namespace Com.TpicGaming.A
{
    public class GameManager : MonoBehaviourPunCallbacks
    {

       public static GameManager Instance;
       public static int fuelcounter;
       public static int ammocounter;
       public static int aspirincounter;
       public GameObject[] spawns;
       public GameObject[] fuelSpawns;
       public GameObject healerSpawn;
       public GameObject[] ammoSpawns;
       public GameObject[] aspirinSpawns;
       //public GameObject fuelspawns;
       //public GameObject fuelspawns2;
        public float Timer = 20;
        public float ammoTimer = 20;
        public float aspirinTimer = 25;
        public Text infoText;
        public Text infomText;
        public Text fuelText;
        public Text fuelNumberText;
        public Text ammoNumberText;
        public Text aspirinNumberText;
        public Text ammoText;
        public Text aspirinText;



        public static bool spawnkont;

        #region Photon Callbacks

        void Start()
        {
            //InvokeRepeating("FuelLoot", 3f, 0.5f);

            //PhotonNetwork.Instantiate("FuelLoot", fuelspawns.transform.position, transform.rotation, 0);
            //PhotonNetwork.Instantiate("FuelLoot", fuelspawns2.transform.position, transform.rotation, 0);

            //fuelcounter = 0;
            //spawnkont = true;
            StartCoroutine(fuelTimeText());
            StartCoroutine(textTime());
            StartCoroutine(healthTimer());
            StartCoroutine(ammoTextTime());
            StartCoroutine(aspirinTextTime());
            if (CharacterControl.LocalPlayerInstance == null)
            {
                int i = Random.Range(0, spawns.Length);

                PhotonNetwork.Instantiate("Character", spawns[i].transform.position, transform.rotation, 0);

            }
            else
            {
                Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
            }
            Instance = this;


        }

        IEnumerator healthTimer()
        {
            yield return new WaitForSeconds(45);
            PhotonNetwork.InstantiateSceneObject("healer", healerSpawn.transform.position, transform.rotation, 0);
            infoText.text = "HEALER IS DROPPED";
            Destroy(infoText, 2);
        }
        IEnumerator textTime()
        {
            yield return new WaitForSeconds(35);
            infomText.text = "10 SECONDS REMAIN TO GET HEALER";
            Destroy(infomText, 2);
        }

        IEnumerator fuelTimeText()
        {
            yield return new WaitForSeconds(5);
            fuelText.text = "FUEL WILL BE ACTIVATED IN 5 SECONDS";
            Destroy(fuelText, 2);
        }
        IEnumerator ammoTextTime()
        {
            yield return new WaitForSeconds(10);
            ammoText.text = "AMMO WILL BE ACTIVATED SOON";
            Destroy(ammoText, 2);
        }
        IEnumerator aspirinTextTime()
        {
            yield return new WaitForSeconds(20);
            aspirinText.text = "5 SECONDS REMAIN TO GET ASPIRIN";
            Destroy(aspirinText, 2);

        }


        void Update()
        {
            Debug.Log(ammocounter);
            Debug.Log(fuelcounter);
            Debug.Log(aspirincounter);
           
            fuelNumberText.text = ""+ fuelcounter;
            ammoNumberText.text = ""+ ammocounter;
            aspirinNumberText.text = ""+ aspirincounter;
            int h = Random.Range(0, fuelSpawns.Length);
            //int x = Random.Range(-15,-18);
            //int z = Random.Range(-17, 23);
            //Vector3 pos = new Vector3(x,1,z);
            Timer -= Time.deltaTime;
            ammoTimer -= Time.deltaTime;
            aspirinTimer -= Time.deltaTime;
            Debug.Log("alınan: " + fuelcounter);

            if (Timer <= 0)
            {

                PhotonNetwork.InstantiateSceneObject("fuel", fuelSpawns[h].transform.position, transform.rotation, 0);
                fuelcounter++;

                Timer = 20;
            }

            if (ammoTimer <= 0)
            {
                int j = Random.Range(0, ammoSpawns.Length);
                PhotonNetwork.InstantiateSceneObject("Ammo", ammoSpawns[j].transform.position, transform.rotation, 0);
                ammocounter++;
                ammoTimer = 15;
            }

            if (aspirinTimer <= 0)
            {
                PhotonNetwork.InstantiateSceneObject("Aspirin", aspirinSpawns[h].transform.position, transform.rotation, 0);
                aspirincounter++;
                aspirinTimer = 25;
            }




            //    Debug.Log(fuelcounter);
            //    if (spawnkont)
            //    {

            //        PhotonNetwork.Instantiate("FuelLoot", fuelspawns.transform.position, transform.rotation, 0);
            //        PhotonNetwork.Instantiate("FuelLoot", fuelspawns2.transform.position, transform.rotation, 0);
            //        spawnkont = false;
            //        fuelcounter += 2;

            //    }
            //    if (fuelcounter == 0)
            //    {
            //        StartCoroutine(spawntime());
            //    }
            //
        }

        //IEnumerator spawntime()
        //{
        //    yield return new WaitForSeconds(30);
        //    spawnkont = true;

        //}

        public override void OnPlayerEnteredRoom(Player other)
        {


            Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting


            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


                LoadArena();


            }
        }


        public override void OnPlayerLeftRoom(Player other)
        {
            Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects


            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


                LoadArena();
            }
        }


        public override void OnLeftRoom()         // local player oyundan cıkarsa
        {
            SceneManager.LoadScene(0);
        }


        #endregion


        #region Public Methods


        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }


        #endregion

        #region Private Methods


        void LoadArena()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
            }


            Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
            PhotonNetwork.LoadLevel("Room for " + PhotonNetwork.CurrentRoom.PlayerCount);


        }


        #endregion

    }

}