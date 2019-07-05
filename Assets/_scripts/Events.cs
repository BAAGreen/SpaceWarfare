using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Events : MonoBehaviour
{
    public Vector3 animPrefabOffset;
    public bool setParrentNewAnim = true;
    public List<GameObject> gObjectsToEnable;
    public float activeTime = 1;

    private Animation m_animation;

    public void DestroyAnimObject()
    {
        Destroy(gameObject);
    }

    public void ShakeCam()
    {
        CameraShaker.Instance.ShakeOnce(1, 20f, 0.01f, 1.5f);
    }

    public void InstantiateNewPrefab(GameObject newPrefab)
    {
        var prefab = Instantiate(newPrefab, transform.parent.transform.position + animPrefabOffset, Quaternion.identity);
        if (setParrentNewAnim) prefab.transform.parent = transform.parent;
    }

    public void EnableGameObject()
    {
        for (int i = 0; i < gObjectsToEnable.Count; i++)
        {
            gObjectsToEnable[i].SetActive(true);
        }
    }

    public void PlaySound(GameObject sound)
    {
        Instantiate(sound, transform.position, Quaternion.identity);
    }

    private IEnumerator PauseAnimationCoroutine(float time)
    {
        var animator = GetComponent<Animator>();
        float prevSpeed = animator.speed;
        animator.speed = 0;

        yield return new WaitForSeconds(time);

        animator.speed = prevSpeed;
    }

    public void PauseAnimation()
    {
        StartCoroutine(PauseAnimationCoroutine(Mathf.Max(activeTime - 1, 0)));
    }
}
