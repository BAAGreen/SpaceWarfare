using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAimingRocket : MonoBehaviour
{
    public GameObject soundPrefab;
    public GameObject rocketPrefab;
    public List<string> rocketTargetTags;
    public float speed;
    public float rotateSpeed;
    public int damage;
    public int maxRockets;

    private GameObject[] targets;
    private int rocketsCount;
    private int changedDamage;

    public bool activate = false;

	void Start ()
    {
        rocketsCount = maxRockets;
        changedDamage = damage;
    }
	
	void Update ()
    {
        if (Input.GetButtonDown("AutoAimingRocket") || activate)
        {
            for (int i = 0; i < rocketTargetTags.Count; i++)
            {
                targets = GameObject.FindGameObjectsWithTag(rocketTargetTags[i]);
            }

            if (targets.Length > 0) changedDamage /= Mathf.Min((targets.Length + 1), maxRockets) / 2;

            if (rocketsCount > 0 && targets.Length > 0)
            {
                var sound = Instantiate(soundPrefab, transform.position, Quaternion.identity);
                sound.GetComponent<AudioSource>().pitch = 1 + Random.Range(-0.2f, 0.2f);
            }

                for (int i = 0; i < targets.Length; i++)
            {
                if (rocketsCount > 0)
                {
                    var rocket = Instantiate(rocketPrefab, transform.position, Quaternion.identity);
                    rocket.GetComponent<Move>().moveVector = rocket.transform.right;
                    rocket.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-130, 130));
                    rocket.GetComponent<Move>().moveSpeed = speed;
                    rocket.GetComponent<Move>().destroyOnLifeTime = false;
                    rocket.GetComponent<Rotate>().rotateSpeed = rotateSpeed;
                    rocket.GetComponent<Rotate>().target = targets[i].transform;
                    if (targets[i].GetComponent<IRFlares>()) targets[i].GetComponent<IRFlares>().lockedRockets.Add(rocket);
                    rocket.GetComponent<CollisionManager>().dealDamageOnCollision = true;
                    rocket.GetComponent<CollisionManager>().damage = changedDamage;
                    rocketsCount--;
                }
            }
            changedDamage = damage;
            rocketsCount = maxRockets;
            activate = false;
        }
	}
}
