using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using PUBLIC_FUNC;

public class Script_Shooter : MonoBehaviour
{
    public enum State
    {
        Ready,
        Empty,
        Reloading
    }

    public State state { get; private set; }
    public Transform shotTransform;

    public int shotDistance = 10;

    public int ammoRemain = 10;
    public int magCatacity = 3;
    public int magAmmo;

    public float timeBetShot = 5.0f;
    public float reloadTime = 2.0f;
    private float lastShotTime;

    public GameObject spawnObject;



    private void OnEnable()
    {
        magAmmo = magCatacity;
        state = State.Ready;
        lastShotTime = 0;
        Script_UIManager.instance.Update_Remain_HaveChangeBall_Text(ammoRemain);
        Script_UIManager.instance.Update_Chaged_ChangeBall_Text(magAmmo);
    }
    public void Fire()
    {
        if (state == State.Ready && Time.time >= lastShotTime + timeBetShot)
        {
            lastShotTime = Time.time;
            Shot();
        }
        
    }

    private void Shot()
    {
        //change spawn
        PhotonNetwork.Instantiate(spawnObject.name, shotTransform.position, shotTransform.transform.rotation,0);
        //�����Ȱ� �ٛ��ٸ�
        magAmmo--;
        if (magAmmo <= 0)
        {
            state = State.Empty;
        }
        Debug.Log("ChargeBall : "+magAmmo);
        Script_UIManager.instance.Update_Chaged_ChangeBall_Text(magAmmo);
    }

    public bool Reload()
    {
        if (state == State.Reloading || ammoRemain <= 0)
        {
            return false;
        }
        Debug.Log("Enter Reload");
        StartCoroutine(ReloadRoutine());
        
        return true;
    }

    private IEnumerator ReloadRoutine()
    {
        
        state = State.Reloading;


        int ammoToFill = magCatacity - magAmmo;
        Debug.Log("Reload : "+ammoToFill);
        yield return new WaitForSeconds(ammoToFill * reloadTime);
        magAmmo = magCatacity; 
        ammoRemain -= ammoToFill;

        Debug.Log("magAmmo : "+magAmmo +" ammoremain : "+ammoRemain);
        state = State.Ready;
        Script_UIManager.instance.Update_Remain_HaveChangeBall_Text(ammoRemain);
        Script_UIManager.instance.Update_Chaged_ChangeBall_Text(magAmmo);

    }


}
