using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public ROTATE_TYPE rotareType;
    public List<GameObject> destroyPrefabs;

    public Transform target;
    public float rotateSpeed = 10;

    private Vector2 newDir;

	void Start ()
    {
        if (rotareType == ROTATE_TYPE.LOOK_AT)
        {

        }
	}

    void Update ()
    {
        LookAt();		
	}

    private void LookAt()
    {
        if (target == null)
        {
            for (int i = 0; i < destroyPrefabs.Count; i++)
            {
                Instantiate(destroyPrefabs[i], transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
        else
        {
            var dir = target.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), rotateSpeed * Time.deltaTime);
        }  
    }
}

public enum ROTATE_TYPE
{
    LOOK_AT,
    MANUAL,
    CHANGE_ANGLE
}
