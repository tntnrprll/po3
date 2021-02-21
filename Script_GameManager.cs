using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace PUBLIC_FUNC
{

    public class Script_GameManager : MonoBehaviourPunCallbacks, IPunObservable
    {
        public static Script_GameManager instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = FindObjectOfType<Script_GameManager>();
                }

                return m_instance;
            }
        }

        private static Script_GameManager m_instance;

        public bool Is_declare_time = false;
        public const int declare_time = 3;
        public int remain_declare_time=3;
        public int declare_stage;
        private int now_stage;
        private int count_time;
        public bool isGameover { get; private set; }
        public GameObject playerPrefab;

        


        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(count_time);
                stream.SendNext(declare_stage);
                stream.SendNext(remain_declare_time);
                stream.SendNext(Is_declare_time);
            }
            else
            {
                count_time = (int)stream.ReceiveNext();
                declare_stage = (int)stream.ReceiveNext();
                remain_declare_time = (int)stream.ReceiveNext();
                Is_declare_time = (bool)stream.ReceiveNext();
            }
        }

        private void Awake()
        {


            if (instance != this)
            {
                Destroy(gameObject);
            }
            Invoke("StartGame", 1.0f);
        }


        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(this.CreatePlayer());
            remain_declare_time = declare_time;
            StartCoroutine("CountTime", 1);


        }

        IEnumerator CountTime(float delayTime)
        {

            yield return new WaitForSeconds(delayTime);
            Func_Count(delayTime);
        }

        void Func_Count(float delayTime)
        {
            photonView.RPC("RPC_Count",RpcTarget.All);
            

            
            if (Is_declare_time)
            {
                // if (PhotonNetwork.IsMasterClient)
                // {
                //     remain_declare_time--;
                // }

                if (remain_declare_time == 0)
                {
                    Debug.Log("Fund_Dieplayer 0");
                    if (PhotonNetwork.IsMasterClient)
                    {
                        Is_declare_time = false;
                        //Script_UIManager.instance.Update_RemainDeclareTime(0);
                        //Script_UIManager.instance.DeclareUI.SetActive(false);
                        
                    }
                    
                    

                }
                
            }
            // Script_UIManager.instance.Update_CountTime(count_time);
            // Script_UIManager.instance.Update_RemainDeclareTime(remain_declare_time);
            // Script_UIManager.instance.DeclareUI.SetActive(Is_declare_time);

            // Debug.Log("Fund_Dieplayer 1 " + remain_declare_time);
            // if(remain_declare_time==0)
            // {
            //     Debug.Log("Fund_Dieplayer 2");
            //     Func_DiePlayer();
            //     if(PhotonNetwork.IsMasterClient)
            //     {
            //         remain_declare_time=declare_time;
            //     }
                
            // }
            StartCoroutine("CountTime", 1);
        }

        void Func_DiePlayer()
        {
            //stage_number가 declare_number인 애들 죽이기
            Debug.Log("Fund_Dieplayer 3");
            GameObject[] All_Player = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in All_Player)
            {
                Debug.Log("player stagenumber : "+player.GetComponent<Script_PlayerMovement>().enter_stage_number +"  declare stage : "+declare_stage);
                if(player.GetComponent<Script_PlayerMovement>().enter_stage_number==declare_stage)
                {
                    Debug.Log("Fund_Dieplayer 4");
                    //죽은 이펙트 보여주기
                    player.GetComponent<Script_Player>().ShowDieEffect();
                }
            }
        }

        [PunRPC]
        void RPC_Count()
        {
            count_time++;
            if(Is_declare_time)
            {
                if(--remain_declare_time<0)
                {
                    Func_DiePlayer();
                    remain_declare_time=declare_time;
                    Is_declare_time=false;
                }
            }
            Script_UIManager.instance.Update_CountTime(count_time);
            Script_UIManager.instance.Update_RemainDeclareTime(remain_declare_time);
            Script_UIManager.instance.DeclareUI.SetActive(Is_declare_time);
        }
        


        IEnumerator CreatePlayer()
        {
            Debug.Log("CreatePlayer");
            PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0, 1, 0), Quaternion.identity, 0);


            yield return null;
        }

        void StartGame()
        {

            GameObject generate_gridmap = GameObject.Find("Generate_GridMap");
            generate_gridmap.GetComponent<Script_GenerateBoxMap>().CreateGridMap();
        }


        // public void Set_now_stage(int stage_in)
        // {
        //     if (!isGameover)
        //     {
        //         now_stage = stage_in;
        //         Script_UIManager.instance.Update_Now_Stage(now_stage);
        //     }
        // }

        public void Set_DeclareStage(int stage_in)
        {
            Debug.Log("Declare is "+Is_declare_time + " stage : "+declare_stage+" remain time : "+remain_declare_time);
            
            if (Is_declare_time)
            {
                return;
            }
            else
            {
                
                this.photonView.RPC("OnClicked_DeclareButton", RpcTarget.All,stage_in);
            }
        }

        [PunRPC]
        public void OnClicked_DeclareButton(int stage_in)
        {
            Is_declare_time = true;
            declare_stage = stage_in;
            Script_UIManager.instance.Update_Declate_Stage(declare_stage);
            Script_UIManager.instance.PlayAnimation_Declare(true);
            GameObject[] BoxList = GameObject.FindGameObjectsWithTag("ColorBox");
            foreach (GameObject box in BoxList)
            {
                box.GetComponent<Script_ColorBox_Overlap>().Spwan_DeclareBall();
            }
        }


    }

}