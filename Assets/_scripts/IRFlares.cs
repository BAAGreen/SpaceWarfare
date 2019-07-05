using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IRFlares : MonoBehaviour
{
    public GameObject prfb_CH;
    public GameObject prfb_irFlare;
    public List<GameObject> lockedRockets;
    public bool isAI = false;
    public bool isLocked = false;
    public bool activate = false;
    public float cooldown = 10;
    public float triggerTimer = 0.5f;
    public float flareSpeed;
    public float lifeTime = 2;
    public int maxCharges = 1;
    public int minFlaresCount = 4;
    public int flaresCount;

    private GameObject ch;


    private int charges;
    private float cooldownTimer, trTimer;

    void Start ()
    {
        charges = maxCharges;
        trTimer = triggerTimer;
        cooldownTimer = cooldown;
        ch = Instantiate(prfb_CH, transform.position, Quaternion.identity);
        ch.transform.parent = transform;
        ch.SetActive(false);
    }
	
	void Update ()
    {
        if (lockedRockets.Count > 0) ch.SetActive(true);
        else ch.SetActive(false);

        if (!isAI && (Input.GetButtonDown("IRFlares") || activate))
        {
            LaunchFlares();
        }

        if (isAI && lockedRockets.Count > 0)
        {
            trTimer -= Time.deltaTime;
            if (trTimer <= 0 && charges > 0)
            {
                LaunchFlares();
                trTimer = triggerTimer;
            }
        }

        if (cooldownTimer > 0)
        {
            if (charges < maxCharges)
            {
                cooldownTimer -= Time.deltaTime;
            }
        }
        if (cooldownTimer <= 0)
        {
            if (charges < maxCharges)
            {
                charges++;
                cooldownTimer = cooldown;
            }
        }
	}

    private void LaunchFlares()
    {
        if (charges > 0)
        {
            List<GameObject> flares = new List<GameObject>();
            float angle = 0;
            flaresCount = Mathf.Max(minFlaresCount, lockedRockets.Count);
            for (int i = 0; i < flaresCount; i++)
            {
                if ((int)Random.Range(0, 2) == 0) angle = (Random.Range(45, 135));
                else angle = (Random.Range(225, 315));
                flares.Add(Instantiate(prfb_irFlare, transform.position, Quaternion.identity));
                flares[i].GetComponent<Move>().moveVector = flares[i].transform.right;
                flares[i].GetComponent<Move>().moveSpeed = flareSpeed;
                flares[i].transform.rotation = Quaternion.Euler(0, 0, angle);
                flares[i].GetComponent<Move>().destroyOnLifeTime = true;
                flares[i].GetComponent<Move>().lifeTime = lifeTime;
            }
            if (lockedRockets.Count > 0)
            {
                int del = 0;
                for (int j = 0; j < lockedRockets.Count; j++)
                {
                    lockedRockets[j].GetComponent<Rotate>().target = flares[j + del].transform;
                    var flareCH = Instantiate(prfb_CH, flares[j + del].transform.position, Quaternion.identity);
                    flareCH.transform.parent = flares[j + del].transform;
                    int index = lockedRockets.FindIndex(x => x.gameObject == lockedRockets[j].gameObject);
                    if (index != -1)
                    {
                        lockedRockets.RemoveAt(index);
                        j--;
                        del++;
                    }
                }
            }
            charges--;
            cooldownTimer = cooldown;
        }
        activate = false;
    }
}
