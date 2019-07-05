using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class FireUIButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private MachineGun playerMG;

    void Start()
    {
        playerMG = GameObject.FindGameObjectWithTag("Player").GetComponent<MachineGun>();
    }

    void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        playerMG.gunActivate = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        playerMG.gunActivate = false;
    }   
}
