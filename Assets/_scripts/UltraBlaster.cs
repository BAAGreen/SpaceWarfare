using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltraBlaster : MonoBehaviour
{
    public GameObject prfb_ultraBlaster;
    public int perDamage = 50; //damage per 0.1 sec//
    public float activeTime = 1; //min = 1//
    public Vector3 offset;

    public bool activate = false;

    void Start ()
    {
        
	}
	
	void Update ()
    {
        if (Input.GetButtonDown("UltraBlaster") || activate)
        {
            var blaster = Instantiate(prfb_ultraBlaster, transform.position + offset, Quaternion.identity);
            blaster.transform.GetChild(1).GetComponent<AOEDamage>().damage = perDamage;
            blaster.transform.GetChild(1).GetComponentInChildren<Events>().activeTime = activeTime;
            blaster.transform.parent = transform;
            activate = false;
        }
	}
}
