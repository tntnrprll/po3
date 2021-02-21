using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Time : MonoBehaviour
{
    [SerializeField] private float secondPerRealTimeSecond;
    private bool night=false;
    [SerializeField] private float fogdensitycalc=0.5f;
    [SerializeField] private float nightfogdensity=0.2f;
    private float dayfogdensity;
    private float currentFogDencity;

    // Start is called before the first frame update
    void Start()
    {
        dayfogdensity=RenderSettings.fogDensity;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right,0.1f*secondPerRealTimeSecond*Time.deltaTime);

        if(transform.eulerAngles.x>=170)
        {
            night=true;
        }
        else if(transform.eulerAngles.x>=340)
        {
            night=false;
        }
        
        if(night)
        {
            if(currentFogDencity>=nightfogdensity)
            {
                currentFogDencity+=0.1f*fogdensitycalc*Time.deltaTime;
                RenderSettings.fogDensity=currentFogDencity; 
            }
            
            
        }
        else
        {
            currentFogDencity -= 0.1f * fogdensitycalc * Time.deltaTime;
            RenderSettings.fogDensity = currentFogDencity;
        }
        
    }
}
