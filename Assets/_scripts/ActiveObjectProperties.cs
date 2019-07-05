using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;

public class ActiveObjectProperties : MonoBehaviour
{
    public GameObject cinematicDestroyEffect;
    public Image healthBar;

    public bool isDestructible = true;
    public bool showHPBar = false;

    public int MAX_HEALTH_POINTS = 250;
    public int healthPoints = 250;
    public int MAX_MANA_POINTS = 250;
    public int manaPoints = 250;
    public int lives = 1;

    public List<GameObject> destroyPrefabs;
    public List<GameObject> lostLivePrefabs;

    void Start ()
    {
        healthPoints = MAX_HEALTH_POINTS;
        manaPoints = MAX_MANA_POINTS;
	}

    void Update ()
    {
        if (isDestructible)
        {
            if (healthPoints <= 0)
            {
                if (lives > 0)
                {
                    healthPoints = MAX_HEALTH_POINTS;
                    LostLife();
                    lives--;
                }
                else
                {
                    DestroyGameObject();
                    CinematicDestroyEffect();
                    lives--;
                }
            }
        }
        if(showHPBar)
        {
            HPBar();
        }
    }

    private void DestroyGameObject()
    {
        for (int i = 0; i < destroyPrefabs.Count; i++)
        {
            Instantiate(destroyPrefabs[i], transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    private void LostLife()
    {
        for (int i = 0; i < lostLivePrefabs.Count; i++)
        {
            Instantiate(lostLivePrefabs[i], transform.position, Quaternion.identity);
        }
        if (gameObject.tag == "Player") CameraShaker.Instance.ShakeOnce(3, 12, 0, 0.75f);
        if (GetComponent<SpriteDamageSystem>() != null) GetComponent<SpriteDamageSystem>().spriteIndex++;
    }

    private void CinematicDestroyEffect()
    {
        if (cinematicDestroyEffect)
        {
            var CDE = Instantiate(cinematicDestroyEffect, transform.position, Quaternion.identity);
            CDE.GetComponent<Move>().moveVector = new Vector2(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f)).normalized;
            if (gameObject.tag == "Player") CameraShaker.Instance.ShakeOnce(3, 12, 0, 0.75f);
        }
    }

    private void HPBar()
    {
        healthBar.fillAmount = Mathf.InverseLerp(0, MAX_HEALTH_POINTS, healthPoints);
    }
}
