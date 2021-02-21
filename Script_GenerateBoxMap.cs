using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using PUBLIC_FUNC;

// public struct BoxInfo
// {
//     public int stage;
//     public Vector3 spawn_point;
// }
public class Script_GenerateBoxMap : MonoBehaviourPun
{
    public static Script_GenerateBoxMap instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = FindObjectOfType<Script_GenerateBoxMap>();
                }

                return m_instance;
            }
        }

        private static Script_GenerateBoxMap m_instance;

    public int X_Count = 10;
    public int Y_Count = 10;
    public float Distance = 300;
    public GameObject SpawnObject;

    public Dictionary<int, BoxInfo> GridMap;
    public Dictionary<int, GameObject> ColorBoxMap;


    // Start is called before the first frame update
    void Start()
    {
        GridMap = new Dictionary<int, BoxInfo>();
        ColorBoxMap = new Dictionary<int, GameObject>();

    }

    public void CreateGridMap()
    {
        Debug.Log("Start repeat?");
        if (PhotonNetwork.IsMasterClient)
        {
            GenerateGrid(X_Count, Y_Count, Distance);
            SpawnGridMap();
        }
    }

    void GenerateGrid(int in_grid_witch_in, int in_grid_height_in, float in_tile_size_in)
    {
        int i = 0;
        for (int x = 0; x < in_grid_witch_in; x++)
        {
            for (int y = 0; y < in_grid_witch_in; y++)
            {
                BoxInfo info = new BoxInfo {key= i, stage = Random.Range(0, 10), spawn_point = new Vector3(x * Distance, 0, y * Distance) };
                
                GridMap.Add(i, info);
                i++;
            }
        }

    }

    void SpawnGridMap()
    {
        foreach (BoxInfo info in GridMap.Values)
        {
            GameObject box = PhotonNetwork.Instantiate(SpawnObject.name, info.spawn_point, Quaternion.identity,0);
            Debug.Log(" is where : " + " key : " + info.key);
            box.GetComponent<Script_ColorBox>().photonView.RPC("Set_Data",RpcTarget.All,info.key,info.stage,info.spawn_point);
            
        }

    }



}
