using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using PUBLIC_FUNC;

public class Script_ColorBox_Overlap : MonoBehaviourPun
{
    public int stage_Number = 0;

    
    public int delayTime=10;

    public GameObject DeclareBall;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        
    }

    
    public void Set_BoxInfo(int stage_in)
    {
        int receive_stage_in=stage_in;
        if(receive_stage_in>=10)
        {
            receive_stage_in=receive_stage_in%10;
        }
        //Debug.Log(receive_stage_in);
        stage_Number = receive_stage_in;
        //transform.parent.gameObject.GetComponent<Script_ColorBox>().Change_StageNumber_And_Material(stage_in);
        //transform.parent.gameObject.GetComponent<Script_ColorBox>().photonView.RPC("Change_GridMapValue", RpcTarget.All, receive_stage_in);
    }



    public void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(stage_Number);
        }
        else
        {
            stage_Number = (int)stream.ReceiveNext();
        }
    }

    public void Spwan_DeclareBall()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            if(stage_Number==Script_GameManager.instance.declare_stage && Script_GameManager.instance.Is_declare_time==true)
            {
                StartCoroutine("SpawnDeclareBall",1);
            }
            
        }
        
    }

    IEnumerator SpawnDeclareBall()
    {
        if(Script_GameManager.instance.Is_declare_time)
        {
            PhotonNetwork.Instantiate(DeclareBall.name, this.gameObject.transform.position, this.gameObject.transform.rotation,0);
            yield return new WaitForSeconds(delayTime);
            StartCoroutine("SpawnDeclareBall",3);
        }
        
        
        
    }
}
