using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalMagnet : MonoBehaviour
{
    public GameObject prfb_magnet;
    public bool activate = false;
    public bool explode = false;
    public float cooldown = 7.5f;
    public float power = -10;
    public float radius = 3;
    public float speed = 5;
    public int maxCharges = 1;

    private GameObject magnetObject;
    private float timer = 0;
    private int charges = 0;

    void Start()
    {
        charges = maxCharges;
    }

    void Update()
    {
        if (charges < maxCharges)
        {
            timer += Time.deltaTime;
            if (timer >= cooldown)
            {
                charges++;
                timer = 0;
            }
        }
        if (!magnetObject) explode = false;

        if (explode && (Input.GetButtonDown("Magnet") || activate))
        {
            magnetObject.GetComponent<Move>().moveSpeed = 0;
            magnetObject.transform.GetChild(0).GetComponentInChildren<PointEffector2D>().forceMagnitude = power;
            magnetObject.transform.GetChild(0).GetComponentInChildren<CircleCollider2D>().radius = radius;
            var pSystem = magnetObject.transform.GetChild(0).GetComponent<ParticleSystem>().shape;
            pSystem.radius = radius;
            magnetObject.transform.GetChild(0).GetComponent<ParticleSystem>().startLifetime = radius;
            magnetObject.GetComponent<AudioSource>().enabled = false;
            magnetObject.GetComponent<Renderer>().enabled = false;
            magnetObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        if ((Input.GetButtonDown("Magnet") || activate))
        {
            if (charges > 0)
            {
                var magnet = Instantiate(prfb_magnet, transform.position, Quaternion.identity);
                magnet.GetComponent<Move>().moveSpeed = speed;
                magnet.GetComponent<Move>().moveVector = Vector2.right;
                magnetObject = magnet;
                explode = true;
                charges--;
            }
            activate = false;
        }  
    }
}
