using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUIController : MonoBehaviour
{
    public ActiveObjectProperties player;
    public GameObject playerHpMpBar;
    public GameObject warningEffect;
    public Image hpBar;
    public Image deltaHpBar;
    public Image mpBar;
    public Image lvlBar;
    public Image livesBar;
    public TextMeshProUGUI txtLives;

    public static GameObject levelFailed;
    public static GameObject levelCompleted;
    public static GameObject hidebleUiElements;

    void Start()
    {
        levelFailed = GameObject.Find("levelFailedUI");
        warningEffect = GameObject.Find("warningEffect");
        levelCompleted = GameObject.Find("levelCompletedUI");
        hidebleUiElements = GameObject.Find("HidebleUIElements");
        levelFailed.SetActive(false);
        warningEffect.SetActive(false);
        levelCompleted.SetActive(false);
        hidebleUiElements.SetActive(true);
    }

    void Update()
    {
        //hp bar
        hpBar.fillAmount = Mathf.InverseLerp(0, player.MAX_HEALTH_POINTS, player.healthPoints);
        deltaHpBar.fillAmount = Mathf.Lerp(deltaHpBar.fillAmount, Mathf.InverseLerp(0, player.MAX_HEALTH_POINTS, player.healthPoints), 4 * Time.deltaTime);

        //mp bar
        mpBar.fillAmount = Mathf.InverseLerp(0, player.MAX_MANA_POINTS, player.manaPoints);

        //lvl bar


        //lives bar
        if (player.lives < 1)
        {
            warningEffect.SetActive(true);
            livesBar.color = new Color(1, 1, 1, 0.3f);
        }
        else
        {
            warningEffect.SetActive(false);
            livesBar.color = new Color(1, 1, 1, 1f);
        }
        txtLives.text = "x" + player.lives;
    }

    public static void LevelFailed()
    {
        levelFailed.SetActive(true);
        hidebleUiElements.SetActive(false);
    }

    public static void LevelCompleted()
    {
        levelCompleted.SetActive(true);
        hidebleUiElements.SetActive(false);
    }
}
