using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public GameObject hitEffect_1;
    public GameObject hitEffect_2;
    public GameObject snd_bulletHitHero;
    public GameObject snd_bulletHitEnemy;
    public GameObject snd_bulletHitRock;
    public GameObject snd_autoAimingRocketOut;

    public bool dealDamageOnCollision = false;
    public int damage = 45;
    public List<string> ignoreTags;

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < ignoreTags.Count; i++)
        {
            var ignoredGameObjects = GameObject.FindGameObjectsWithTag(ignoreTags[i]);
            for (int j = 0; j < ignoredGameObjects.Length; j++)
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), ignoredGameObjects[j].GetComponent<Collider2D>());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (gameObject.tag != "Bullet" && col.transform.tag != "Bullet")
        {
            Instantiate(hitEffect_2, col.contacts[0].point, Quaternion.identity);
        }

        bool ignoreThisCol = false;

        for (int i = 0; i < ignoreTags.Count; i++)
        {
            if (col.transform.tag == ignoreTags[i])
            {
                ignoreThisCol = true;
            }
        }

        if (dealDamageOnCollision && !ignoreThisCol)
        {
            if (col.gameObject.GetComponent<ActiveObjectProperties>() != null)
            {
                if (col.gameObject.GetComponent<ActiveObjectProperties>().isDestructible)
                {
                    col.gameObject.GetComponent<ActiveObjectProperties>().healthPoints -= damage;
                    print(damage);
                }
            }
        }

        if (gameObject.tag == "Player" && !ignoreThisCol)
        {
            if (col.transform.tag == "Enemy")
            {
                var snd_hit = Instantiate(snd_bulletHitEnemy, transform.position, Quaternion.identity);
                snd_hit.GetComponent<AudioSource>().pitch = 1 + Random.Range(-0.15f, 0.15f);
            }
            else if (col.transform.tag == "Rock")
            {
                var snd_hit = Instantiate(snd_bulletHitRock, transform.position, Quaternion.identity);
                snd_hit.GetComponent<AudioSource>().pitch = 1 + Random.Range(-0.15f, 0.15f);
            }
        }

        if (gameObject.tag == "Bullet" && !ignoreThisCol)
        {           
            Instantiate(hitEffect_1, transform.position, Quaternion.identity);
            if (col.transform.tag == "Player")
            {
                var snd_hit = Instantiate(snd_bulletHitHero, transform.position, Quaternion.identity);
                snd_hit.GetComponent<AudioSource>().pitch = 1 + Random.Range(-0.15f, 0.15f);
            }
            else if (col.transform.tag == "Enemy")
            {
                var snd_hit = Instantiate(snd_bulletHitEnemy, transform.position, Quaternion.identity);
                snd_hit.GetComponent<AudioSource>().pitch = 1 + Random.Range(-0.15f, 0.15f);
            }
            else if (col.transform.tag == "Rock")
            {
                var snd_hit = Instantiate(snd_bulletHitRock, transform.position, Quaternion.identity);
                snd_hit.GetComponent<AudioSource>().pitch = 1 + Random.Range(-0.15f, 0.15f);
            }
            Destroy(gameObject);
        }

        if (gameObject.tag == "AutoAimingRocket")
        {
            Instantiate(hitEffect_1, transform.position, Quaternion.identity);
            var snd_hit = Instantiate(snd_autoAimingRocketOut, transform.position, Quaternion.identity);
            snd_hit.GetComponent<AudioSource>().pitch = 1 + Random.Range(-0.25f, 0.25f);
            if (gameObject.GetComponent<Rotate>().target.GetComponent<IRFlares>())
            {
                int index = gameObject.GetComponent<Rotate>().target.GetComponent<IRFlares>().lockedRockets.FindIndex(x => x.gameObject == gameObject);
                if (index != -1) gameObject.GetComponent<Rotate>().target.GetComponent<IRFlares>().lockedRockets.RemoveAt(index);
            }

            foreach (Transform child in GetComponent<Rotate>().target.transform)
            {
                if (!GetComponent<Rotate>().target.GetComponent<IRFlares>() && child.tag == "CH") Destroy(child.gameObject);
            }

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "AutoAimingRocket")
        {
            Instantiate(hitEffect_1, transform.position, Quaternion.identity);
            var snd_hit = Instantiate(snd_autoAimingRocketOut, transform.position, Quaternion.identity);
            snd_hit.GetComponent<AudioSource>().pitch = 1 + Random.Range(-0.25f, 0.25f);
            if (gameObject.GetComponent<Rotate>().target.GetComponent<IRFlares>())
            {
                int index = gameObject.GetComponent<Rotate>().target.GetComponent<IRFlares>().lockedRockets.FindIndex(x => x.gameObject == gameObject);
                if (index != -1) gameObject.GetComponent<Rotate>().target.GetComponent<IRFlares>().lockedRockets.RemoveAt(index);
            }

            foreach (Transform child in GetComponent<Rotate>().target.transform)
            {
                if (!GetComponent<Rotate>().target.GetComponent<IRFlares>() && child.tag == "CH") Destroy(child.gameObject);
            }

            Destroy(gameObject);
        }
    }


}
