using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    public ActiveObjectProperties player;
    public AudioMixer audioMixer;
    public bool lastLifeSoundEffect = true;

    private LevelMaker levelMaker;
    private float parameter;

    void Start()
    {
        if (!player) player = GameObject.FindGameObjectWithTag("Player").GetComponent<ActiveObjectProperties>();
        levelMaker = GameObject.Find("LEVEL_MAKER").GetComponent<LevelMaker>();
        audioMixer.SetFloat("lowpass_effects", 22000);
        audioMixer.SetFloat("volume_effects", 0);
    }

    void Update()
    {
        if (lastLifeSoundEffect) LastLifeSoundEffect();
    }

    public void EndLevelEffect()
    {
        audioMixer.GetFloat("lowpass_effects", out parameter);
        audioMixer.SetFloat("lowpass_effects", Mathf.Lerp(parameter, 500, 10 * Time.deltaTime));
        audioMixer.GetFloat("volume_effects", out parameter);
        audioMixer.SetFloat("volume_effects", Mathf.Lerp(parameter, -55, 0.005f));
    }

    public void LastLifeSoundEffect()
    {
        if (player.lives == 0)
        {
            audioMixer.GetFloat("lowpass_effects", out parameter);
            audioMixer.SetFloat("lowpass_effects", Mathf.Lerp(parameter, 1000, 10 * Time.deltaTime));
        }
        else if (!levelMaker.uIEventIsDone)
        {
            audioMixer.GetFloat("lowpass_effects", out parameter);
            audioMixer.SetFloat("lowpass_effects", Mathf.Lerp(parameter, 22000, 10 * Time.deltaTime));
        }
    }
}
