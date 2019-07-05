using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject snd_blasterPrefab;
    public GameObject gunFireAnimPrefab;
    public Vector3 offset;
    public float delay = 0.25f;
    public float bulletSpeed = 20;
    public int damage = 45;
    public bool isAI = false;
    public bool gunActivate = false;

    private bool ready = true;
    private float timer = 0;

    void Start ()
    {
		
	}
	
	void Update ()
    {
        if (!isAI)
        {
            if (Input.GetButtonDown("BasicAttack"))
            {
                gunActivate = true;
            }
            if (Input.GetButtonUp("BasicAttack"))
            {
                gunActivate = false;
            }
        }

        if (gunActivate)
        {
            if (ready)
            {
                var bullet = Instantiate(bulletPrefab, transform.position + offset, Quaternion.identity);
                var snd_blaster = Instantiate(snd_blasterPrefab, transform.position, Quaternion.identity);
                var gunFire = Instantiate(gunFireAnimPrefab, transform.position + offset / 2, Quaternion.identity);
                bullet.GetComponent<Move>().moveSpeed = bulletSpeed;
                bullet.GetComponent<CollisionManager>().dealDamageOnCollision = true;
                bullet.GetComponent<CollisionManager>().damage = damage;
                snd_blaster.GetComponent<AudioSource>().pitch = 1 + Random.Range(-0.1f, 0.1f);
                gunFire.transform.parent = transform;
                if (gameObject.tag == "Player") bullet.GetComponent<Move>().moveVector = Vector2.right;
                else bullet.GetComponent<Move>().moveVector = -Vector2.right;
                timer = 0;
                ready = false;
            }
        }
        timer += Time.deltaTime;
        if (timer >= delay)
        {
            timer = 0;
            ready = true;
        }
    }
}
