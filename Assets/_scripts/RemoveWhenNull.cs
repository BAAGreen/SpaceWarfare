using UnityEngine;

public class RemoveWhenNull : MonoBehaviour
{
    public GameObject targetObject;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        if (!targetObject) Destroy(gameObject);
	}
}
