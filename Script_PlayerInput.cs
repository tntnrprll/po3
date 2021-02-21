using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PUBLIC_FUNC;

public class Script_PlayerInput : MonoBehaviour
{
    // public string moveButtonName_U="Vertical_U";
    // public string moveButtonName_D="Vertical_D";
     public string moveButtonName_R="Horizontal_R";
     public string moveButtonName_L="Horizontal_L";
    public string horizontal="Horizontal";
    public string vertical="Vertical";
    public string shotButtonName="Fire1";
    public string reloadButtonName="Reload";

    // public bool move_U {get; private set;}
    // public bool move_D {get; private set;}
    public bool move_R{get; private set;}
    public bool move_L{get; private set;}

    public float move_horizontal{get; private set;}
    public float move_vertical{get; private set;}
    
    
    public bool shot{get; private set;}
    public bool reload{get; private set;}


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //게임오버상태일때인데 gamemanager(state)에 대한 세팅이 필요한듯
        // move=0;
        // rotate=0;
        // shot_changeball=false;

        //move_U=Input.GetButtonDown(moveButtonName_U);
        //move_D=Input.GetButtonDown(moveButtonName_D);
        move_R=Input.GetButtonDown(moveButtonName_R);
        move_L=Input.GetButtonDown(moveButtonName_L);

        move_horizontal=Input.GetAxis(horizontal);
        move_vertical=Input.GetAxis(vertical);
        shot=Input.GetButtonDown(shotButtonName);
        reload=Input.GetButtonDown(reloadButtonName);
    }

}
