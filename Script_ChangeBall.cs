using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using PUBLIC_FUNC;

public class Script_ChangeBall : MonoBehaviourPun
{
    public bool is_setstart_stagenumber = false;
    public int Start_stage_Number=0;
    public GameObject last_Enter_ColorBox;

    public ParticleSystem destroyEffect;


    // Start is called before the first frame update
    void Start()
    {
        last_Enter_ColorBox = new GameObject();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime);

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag=="ColorBox")
        {
            if (is_setstart_stagenumber)
            {

                if (Start_stage_Number != col.gameObject.GetComponent<Script_ColorBox_Overlap>().stage_Number)
                {
                    int reach_stage_number=col.gameObject.GetComponent<Script_ColorBox_Overlap>().stage_Number;
                    if(last_Enter_ColorBox.transform.parent.gameObject.GetComponent<Script_ColorBox>())
                    {
                        last_Enter_ColorBox.transform.parent.gameObject.GetComponent<Script_ColorBox>().photonView.RPC("Change_GridMapValue", RpcTarget.All, reach_stage_number);
                    }
                    
                    
                    ParticleSystem effectobject = Instantiate(destroyEffect,this.gameObject.transform.position,Quaternion.identity);
                    effectobject.Play();
                    photonView.RPC("RPC_Destroy",RpcTarget.All);
                }
                else
                {
                    last_Enter_ColorBox = col.gameObject;
                }
            }
            else
            {
                last_Enter_ColorBox = col.gameObject;
                //Debug.Log("Trigger 6 "+last_Enter_ColorBox.name);
                Start_stage_Number = last_Enter_ColorBox.GetComponent<Script_ColorBox_Overlap>().stage_Number;
                is_setstart_stagenumber = true;
                //Debug.Log("Trigger 5   "+Start_stage_Number);
            }

        }
        
    }
    [PunRPC]
    void RPC_Destroy()
    {
        Destroy(this.gameObject);
    }

}


