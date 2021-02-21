using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using PUBLIC_FUNC;

public class Script_PlayerMovement : MonoBehaviourPunCallbacks
{
    public float moveSpeed=5f;
    public float rotateSpeed=180f;

    private Script_PlayerInput playerInput;
    private Rigidbody playerRigidbody;
    private Animator playerAnimator;
    public int enter_stage_number;





    // Start is called before the first frame update
    private void Start()
    {
        if(photonView.IsMine)
        {
            playerInput = GetComponent<Script_PlayerInput>();
            playerRigidbody = GetComponent<Rigidbody>();
            playerAnimator = GetComponent<Animator>();
        }

        
    }


    void FixedUpdate()
    {
        Move();
        Rotate(true);

        

    }

    private void Move()
    {
        if(photonView.IsMine)
        {
            

            if(playerInput.move_vertical!=0)
            {
                Vector3 moveDistance= playerInput.move_vertical*transform.forward*moveSpeed*Time.deltaTime;
                playerRigidbody.MovePosition(playerRigidbody.position+moveDistance);
                playerAnimator.SetBool("IsMoving",true);
            }
            else
            {
                playerAnimator.SetBool("IsMoving",false);
            }
        }
        
    }

    private void Rotate(bool direction)
    {
        if(photonView.IsMine)
        {
            if(playerInput.move_horizontal!=0)
            {
                float turn=playerInput.move_horizontal*rotateSpeed*Time.deltaTime;
                playerRigidbody.rotation=playerRigidbody.rotation*Quaternion.Euler(0,turn,0f);
                
            }
        }
        

        
    }


    IEnumerator MoveTo(GameObject target_in, Vector3 pos_in)
    {
        //IsMoving=true;
        float count=0;
        Vector3 wasPos=target_in.transform.position;
        while(true)
        {

            count+=Time.deltaTime;
            target_in.transform.position=Vector3.Lerp(wasPos,pos_in,count);

            if(count>=1)
            {
                target_in.transform.position=pos_in;
                break;
            }
            yield return null;
        }
        //IsMoving=false;
    }

    void OnTriggerEnter(Collider col)
    {
        if(photonView.IsMine)
        {
            if (col.gameObject.tag == "ColorBox")
            {
                //Debug.Log("is colorbox ? ");
                enter_stage_number = col.gameObject.GetComponent<Script_ColorBox_Overlap>().stage_Number;
                Script_UIManager.instance.Update_Now_Stage(enter_stage_number);
                //Now_Position=col.gameObject.transform.position;
                //StartCoroutine(MoveTo(this.gameObject,col.gameObject.transform.position));
                //this.gameObject.transform.position=col.gameObject.transform.position;
            }
        }
        

    }

    

    

}
