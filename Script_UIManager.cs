using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using PUBLIC_FUNC;

public class Script_UIManager : MonoBehaviour
{
    public static Script_UIManager instance 
    {
        get 
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<Script_UIManager>();
            }
            return m_instance;
        }
    }

    private static Script_UIManager m_instance;

    public Text Text_HaveChangeBall;
    public Text Text_ChagedChangeBall;
    public Text Text_NowStage;
    public Text Text_DeclareStage;
    public Text Text_CountTime;
    public Text Text_RemainDeclareTime;

    public Button Button_Declare;
    public GameObject DeclareUI;
    public GameObject DieUI;
    public int now_stage;

    void Awake()
    {
        DieUI.SetActive(false);
        DeclareUI.SetActive(false);
    }

    public void Update_Chaged_ChangeBall_Text(int chaged_in)
    {
        Text_ChagedChangeBall.text="충전된\n changeball\n"+chaged_in;
    }
    public void Update_Remain_HaveChangeBall_Text(int remainAmmo_in)
    {
        //Text_HaveChangeBall.text = "가지고있는\n changeball\n" + remainAmmo_in;
    }

    public void Update_Now_Stage(int stage_in)
    {
        now_stage=stage_in;
        Text_NowStage.text = "현재 위치한\n stage\n" + stage_in;
    }

    public void Update_Declate_Stage(int stage_in)
    {
        Text_DeclareStage.text = "현재 선언된\n stage\n" + stage_in;
    }

    public void Update_CountTime(int time_in)
    {
        Text_CountTime.text="진행 시간\n"+time_in;
    }

    public void Update_RemainDeclareTime(int time_in)
    {
        Text_RemainDeclareTime.text="남은 선언 진행 시간\n"+time_in;
        Debug.Log("aoilsdhgjdfkghklsdfg   "+ Script_GameManager.instance.Is_declare_time);
        Button_Declare.interactable=!Script_GameManager.instance.Is_declare_time;
        
    }

    public void PlayAnimation_Declare(bool active_in)
    {
        Debug.Log("playanimation ");
        DeclareUI.SetActive(active_in);
        DeclareUI.transform.GetChild(0).GetComponent<Text>().text=Script_GameManager.instance.declare_stage + "가 선언되었습니다";
        
        DeclareUI.GetComponent<Animation>().Play("ANI_ShowDeclare");

    }

    public void Show_DieUI()
    {
        DieUI.SetActive(true);
    }


}
