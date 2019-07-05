using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDamageSystem : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public List<Sprite> damageSprites;
    public int spriteIndex;

    private int MAX_SPRITE_INDEX;

    /*
    Расположить спрайты разных состояний в листе "damageSprites"
    по мере возрастания полученного урона, начиная с полного 
    отсутствия урона (под индексом 0 будет спрайт без урона)
    */

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteIndex = 0;
        MAX_SPRITE_INDEX = damageSprites.Count - 1;
    }

    void Update()
    {
        if (spriteIndex > MAX_SPRITE_INDEX) spriteIndex = MAX_SPRITE_INDEX;
        spriteRenderer.sprite = damageSprites[spriteIndex];
    }
}
