using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class LevelMaker : MonoBehaviour
{
    public ActiveObjectProperties playerProp;
    private SoundController soundController;
    
    public enum GAME_MOD
    {
        TIMER,
        STAY_ALIVE,
        DESTROY_EVERYONE
    }

    public GAME_MOD gameMod;

    public float timer;
    public int targetPoints;
    public int currentPoints;
    public int enemyCount;

    public bool uIEventIsDone = false;

    private BlurOptimized blur;
    private int destroyedEnemyes;

    

    void Start()
    {
        soundController = GameObject.Find("Sound_Controller").GetComponent<SoundController>();
        blur = Camera.main.GetComponent<BlurOptimized>();
        blur.blurSize = 0;
        blur.blurIterations = 1;
        blur.enabled = false;
    }

    void Update()
    {
        if (gameMod == GAME_MOD.TIMER) //режим на время
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                EndLevelEvent();
                if (currentPoints >= targetPoints) LevelCompleted();
                else LevelFailed();
            }
        }

        if (gameMod == GAME_MOD.STAY_ALIVE) //выживание
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                EndLevelEvent();
                LevelCompleted();
            }
        }

        if (gameMod == GAME_MOD.DESTROY_EVERYONE)
        {
            if (destroyedEnemyes >= enemyCount)
            {
                EndLevelEvent();
                LevelCompleted();
            }
        }

        if (playerProp.lives < 0)
        {
            EndLevelEvent();
            LevelFailed();
        }
    }

    private void EndLevelEvent()
    {
        //постепенное замедление
        Time.timeScale = Mathf.Lerp(Time.timeScale, 0, 3 * Time.deltaTime);

        //эффект размытия
        blur.enabled = true;
        blur.blurIterations = 2;
        blur.blurSize = Mathf.Lerp(blur.blurSize, 3, 5 * Time.deltaTime);

        //эффект замедления звука 
        soundController.EndLevelEffect();
    }

    private void LevelFailed() 
    {
        //UI
        if (!uIEventIsDone)
        {
            GUIController.LevelFailed();
            uIEventIsDone = true;
        }
    }

    private void LevelCompleted()
    {
        //UI
        if (!uIEventIsDone)
        {
            GUIController.LevelCompleted();
            uIEventIsDone = true;
        }
    }
}
