using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;
using PUBLIC_FUNC;

public class Script_CameraSetup : MonoBehaviourPun
{
    private CinemachineVirtualCamera followCam;

    // Start is called before the first frame update
    void Start()
    {
        if(photonView.IsMine)
        {
            followCam=FindObjectOfType<CinemachineVirtualCamera>();

            followCam.Follow=transform;
            followCam.LookAt=transform;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

}
