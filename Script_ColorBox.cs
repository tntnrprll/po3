using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using PUBLIC_FUNC;

public class Script_ColorBox : MonoBehaviourPun
{
    public List<Color> ColorList;

    public BoxInfo boxInfo;
    


    // Start is called before the first frame update
    void Start()
    {
        ColorList.Add(Color.red);
        ColorList.Add(Color.black);
        ColorList.Add(Color.green);
        ColorList.Add(Color.blue);
        ColorList.Add(Color.yellow);
        ColorList.Add(Color.white);
        ColorList.Add(Color.black);
        ColorList.Add(Color.red);
        ColorList.Add(Color.green);
        ColorList.Add(Color.red);

        

    }

    void FixedUpdate()
    {
        //Change_StageNumber_And_Material();
    }
    [PunRPC]
    public void Set_Data(int key_in,int stage_in,Vector3 spawnpoint_in)
    {
        boxInfo=new BoxInfo{key=key_in,stage=stage_in,spawn_point=spawnpoint_in};
        Set_Show(boxInfo.stage);
    }

    public void Set_Show(int stage_in)
    {
        boxInfo.stage=stage_in;
        GetComponent<Renderer>().material.color = ColorList[boxInfo.stage];
        transform.GetChild(0).gameObject.GetComponent<Script_ColorBox_Overlap>().Set_BoxInfo(stage_in);
        transform.GetChild(1).gameObject.GetComponent<Script_Wall>().Change_Rotation(boxInfo.stage);

        if(Script_GameManager.instance.Is_declare_time==true && Script_GameManager.instance.declare_stage==boxInfo.stage)
        {
            //불들어오게
        }
    }

    [PunRPC]
    public void Change_GridMapValue(int stage_in)
    {
        Set_Show(stage_in);
        GameObject Generate_GridMap = GameObject.FindWithTag("GridMap");
        Generate_GridMap.GetComponent<Script_GenerateBoxMap>().GridMap[boxInfo.key]=boxInfo;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(boxInfo);
        }
        else
        {
            boxInfo = (BoxInfo)stream.ReceiveNext();
        }
    }

    

}
