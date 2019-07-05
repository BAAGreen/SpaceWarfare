using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public AudioSource levelFailed_snd;

    public void LevelFailed()
    {
        Instantiate(levelFailed_snd);
    }
}
