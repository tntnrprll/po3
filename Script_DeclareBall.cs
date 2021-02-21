using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using PUBLIC_FUNC;

public class Script_DeclareBall : MonoBehaviourPun, IPunObservable
{
    private byte random_rotate;
    private float move_speed=0.1f;
    public ParticleSystem destroyEffect;
    private bool is_startpoint=true;
    // Start is called before the first frame update

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(random_rotate);
        }
        else
        {
            random_rotate = (byte)stream.ReceiveNext();
        }
    }

    void Start()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            random_rotate=(byte)Random.Range(1,5);
        }
        

        switch(random_rotate)
        {
            case 1: 
            {
                transform.rotation=Quaternion.Euler(0f,0f,0f);
                //transform.Rotate(new Vector3(0f,0f,0f));
                break;
            }
            case 2:
            {
                transform.rotation=Quaternion.Euler(0f,180f,0f);
                //transform.Rotate(new Vector3(0f,180f,0f));
                break;
            }
            case 3:
            {
                transform.rotation=Quaternion.Euler(0f,90f,0f);
                //transform.Rotate(new Vector3(0f,90f,0f));
                break;
            }
            case 4:
            {
                transform.rotation=Quaternion.Euler(0f,270f,0f);
                //transform.Rotate(new Vector3(0f,270f,0f));
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(random_rotate)
        {
            case 1: 
            {
                transform.Translate(Vector3.forward *move_speed* Time.deltaTime);
                break;
            }
            case 2:
            {
                transform.Translate(-Vector3.forward *move_speed* Time.deltaTime);
                break;
            }
            case 3:
            {
                transform.Translate(Vector3.right *move_speed* Time.deltaTime);
                break;
            }
            case 4:
            {
                transform.Translate(-Vector3.right *move_speed* Time.deltaTime);
                break;
            }
        }
        
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag=="ColorBox")
        {
            if(!is_startpoint)
            {
                GameObject enter_box=col.gameObject;
                if(enter_box.GetComponent<Script_ColorBox_Overlap>().stage_Number!=Script_GameManager.instance.declare_stage)
                {
                    enter_box.transform.parent.gameObject.GetComponent<Script_ColorBox>().photonView.RPC("Change_GridMapValue",RpcTarget.All,enter_box.GetComponent<Script_ColorBox_Overlap>().stage_Number+1);
                    ParticleSystem effectobject = Instantiate(destroyEffect,this.gameObject.transform.position,Quaternion.identity);
                    effectobject.Play();
                    
                }
                Destroy(this.gameObject);
            }
            else
            {
                is_startpoint=false;
            }
            
            
        }
    }
}
