using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public List<bool> skillIsActive;//0 - autoAimRocket, 1 - IRFlares, 2 - UltraBlaster, 3 - magnet//
    public List<GameObject> buttonObjects;
    public List<Sprite> buttonSprites;

    private int buttonsCount = 0;

    void Start()
    {
        ButtonsInit();
    }

    //---------------------------------------------------------------------------//

    private void ButtonsInit()
    {
        if (skillIsActive[0]) //0 - autoAimRocket
        {
            GetComponent<AutoAimingRocket>().enabled = true;
            buttonsCount++;
        }
        else GetComponent<AutoAimingRocket>().enabled = false;

        if (skillIsActive[1]) //1 - IRFlares
        {
            GetComponent<IRFlares>().enabled = true;
            buttonsCount++;
        }
        else GetComponent<IRFlares>().enabled = false;

        if (skillIsActive[2]) //2 - UltraBlaster
        {
            GetComponent<UltraBlaster>().enabled = true;
            buttonsCount++;
        }
        else GetComponent<UltraBlaster>().enabled = false;

        if (skillIsActive[3]) //3 - magnet
        {
            GetComponent<PhysicalMagnet>().enabled = true;
            buttonsCount++;
        }
        else GetComponent<PhysicalMagnet>().enabled = false;

        for (int i = 0; i < buttonsCount; i++)
        {
            buttonObjects[i].SetActive(true);
            for (int j = 0; j < skillIsActive.Count; j++)
            {
                if (skillIsActive[j])
                {
                    switch(j)
                    {
                        case 0:
                            buttonObjects[i].GetComponent<Button>().onClick.AddListener(OclBtnAutoAimRocket);
                            break;
                        case 1:
                            buttonObjects[i].GetComponent<Button>().onClick.AddListener(OclBtnIRFlares);
                            break;
                        case 2:
                            buttonObjects[i].GetComponent<Button>().onClick.AddListener(OclBtnUltraBlaster);
                            break;
                        case 3:
                            buttonObjects[i].GetComponent<Button>().onClick.AddListener(OclBtnMagnet);
                            break;
                    }
                    buttonObjects[i].GetComponent<Image>().sprite = buttonSprites[j];
                    skillIsActive[j] = false;
                    break;
                }
            }
        }
    }

    private void OclBtnAutoAimRocket()//0 - autoAimRocket
    {
        GetComponent<AutoAimingRocket>().activate = true;
    }

    private void OclBtnIRFlares()//1 - IRFlares
    {
        GetComponent<IRFlares>().activate = true;
    }

    private void OclBtnUltraBlaster()//2 - UltraBlaster
    {
        GetComponent<UltraBlaster>().activate = true;
    }

    private void OclBtnMagnet()//2 - UltraBlaster
    {
        GetComponent<PhysicalMagnet>().activate = true;
    }

}
