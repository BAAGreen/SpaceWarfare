using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public List<GameObject> destroyPrefabs;
    public Vector2 moveVector;
    public bool destroyOnLifeTime = false;
    public bool isDrag = false;
    public bool moveWithVelocity = false;
    public float lifeTime;
    public float moveSpeed;
    public float minMoveSpeed;
    public float drag;
    public int maxX, maxY;

    private float velocity;    
    private float timer;

	void Start ()
    {
        timer = lifeTime;
	}
	
	void Update ()
    {
        if (moveWithVelocity) velocity = Mathf.Lerp(velocity, moveSpeed, 1f * Time.deltaTime);
        else velocity = moveSpeed;

        if (isDrag) velocity = Mathf.Lerp(velocity, minMoveSpeed, drag * Time.deltaTime);

        transform.Translate(new Vector3(moveVector.x, moveVector.y, 0) * velocity * Time.deltaTime);
        if (destroyOnLifeTime)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                for (int i = 0; i < destroyPrefabs.Count; i++)
                {
                    Instantiate(destroyPrefabs[i], transform.position, Quaternion.identity);
                }
                Destroy(gameObject);
            }
        }

        if (maxY != 0 || maxX != 0)
        {
            if (transform.position.x < -maxX || transform.position.x > maxX || transform.position.y < -maxY || transform.position.y > maxY) Destroy(gameObject);
        }

    }

    
}
