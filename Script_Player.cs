using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using PUBLIC_FUNC;

public class Script_Player : MonoBehaviourPun
{
    public ParticleSystem DieEffect;
    // Start is called before the first frame update
    void Start()
    {
        if(!photonView.IsMine)
        {
            this.gameObject.transform.GetChild(3).GetComponent<Light>().intensity=0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowDieEffect()
    {
        Debug.Log("die");
        ParticleSystem effectobject = Instantiate(DieEffect,this.gameObject.transform.position,Quaternion.identity);
        effectobject.Play();
        
        Script_UIManager.instance.Show_DieUI();
        photonView.RPC("RPC_Destroy",RpcTarget.All);
    }
    [PunRPC]
    void RPC_Destroy()
    {
        Destroy(this.gameObject);
    }

    public void OnEvent_Set_Declare()
    {
        Debug.Log("OnEvent");
        Script_GameManager.instance.Set_DeclareStage(Script_UIManager.instance.now_stage);

    }
    
    
}
