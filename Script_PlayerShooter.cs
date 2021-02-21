using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using PUBLIC_FUNC;

public class Script_PlayerShooter : MonoBehaviourPun
{
    public Script_Shooter shooter;
    public Transform spawnnerPivot;
    
    private Script_PlayerInput playerInput;
    private Animator playerAnimator;


    // Start is called before the first frame update
    void Start()
    {
        playerInput=GetComponent<Script_PlayerInput>();
        playerAnimator=GetComponent<Animator>();

    }

    private void OnEnable()
    {
        shooter.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        shooter.gameObject.SetActive(false);
    }

    void Update()
    {
        if(photonView.IsMine)
        {
            if (playerInput.shot)
            {
                shooter.Fire();
            }
            if(playerInput.reload)
            {
                shooter.Reload();
            }
            
        }
    }


    

}


