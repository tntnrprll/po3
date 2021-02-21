using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using PUBLIC_FUNC;

public class Script_Wall : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    public void Change_Rotation(int stage_in)
    {
        Debug.Log("stage_in " + stage_in%4);
        Set_Rotation(stage_in);
    }

    public void Set_Rotation(int stage_in)
    {
        switch(stage_in%4)
        {
            
            case 0:
            {
                this.gameObject.transform.position=transform.parent.transform.position+(new Vector3(-1.25f,2.2f,0f));
                this.gameObject.transform.rotation=transform.parent.gameObject.transform.rotation*Quaternion.Euler(0f, 0.0f, 0f);
                break;
            }
            case 1:
            {
                this.gameObject.transform.position=transform.parent.transform.position+(new Vector3(1.25f,2.2f,0f));
                this.gameObject.transform.rotation=transform.parent.gameObject.transform.rotation*Quaternion.Euler(0f, 0.0f, 0f);
                break;
            }
            case 2:
            {
                this.gameObject.transform.position=transform.parent.transform.position+(new Vector3(0f,2.2f,-1.25f));
                //this.gameObject.transform.Translate(new Vector3(0f,0.1946f,-0.045f));
                this.gameObject.transform.rotation=transform.parent.gameObject.transform.rotation*Quaternion.Euler(0f, 90.0f, 0f);
                break;
            }
            case 3:
            {
                this.gameObject.transform.position=transform.parent.transform.position+(new Vector3(0f,2.2f,1.25f));
                //this.gameObject.transform.Translate(new Vector3(0f,0.1946f,0.045f));
                this.gameObject.transform.rotation=transform.parent.gameObject.transform.rotation*Quaternion.Euler(0f, 90.0f, 0f);
                break;
            }
        }
    }


}
