using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEDamage : MonoBehaviour
{
    public List<ActiveObjectProperties> damageTargets;
    public GameObject animPrefab;
    public int damage = 50;
    public bool periodicDamage = true;
    public float delay = 0.1f;

    private float timer = 0;

    void Start ()
    {
		
	}
	
	void Update ()
    {
        if (periodicDamage)
        {
            timer += Time.deltaTime;
            if (timer >= delay)
            {
                for (int i = 0; i < damageTargets.Count; i++)
                {
                    damageTargets[i].healthPoints -= damage;
                    if(animPrefab) Instantiate(animPrefab, damageTargets[i].transform.position, Quaternion.identity);
                }
                timer = 0;
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<ActiveObjectProperties>() != null)
        {
            int index = damageTargets.FindIndex(x => x.gameObject.GetComponent<ActiveObjectProperties>() == col.gameObject.GetComponent<ActiveObjectProperties>());
            if (index == -1) damageTargets.Add(col.gameObject.GetComponent<ActiveObjectProperties>());
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.GetComponent<ActiveObjectProperties>() != null)
        {
            int index = damageTargets.FindIndex(x => x.gameObject.GetComponent<ActiveObjectProperties>() == col.gameObject.GetComponent<ActiveObjectProperties>());
            if (index != -1) damageTargets.RemoveAt(index);
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<ActiveObjectProperties>() != null)
        {
            int index = damageTargets.FindIndex(x => x.gameObject.GetComponent<ActiveObjectProperties>() == col.gameObject.GetComponent<ActiveObjectProperties>());
            if (index == -1) damageTargets.Add(col.gameObject.GetComponent<ActiveObjectProperties>());
        }
    }


}
