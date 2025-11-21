using UnityEngine;
using System.Collections; 

public class FastSpriteSwitching : MonoBehaviour
{
    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    private int currentIndex = 0;
    public float switchInterval = 0.2f; // half a second between switches


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.Log("SpriteRenderer component not found.");
            enabled = false;
        }
        if (sprites.Length > 0) 
        { 
            spriteRenderer.sprite = sprites[currentIndex];
            StartCoroutine(SwitchSpriteLoop());

        }
    }

    public void SwitchToNextSprite()
    {
        if (sprites.Length == 0) return;

        currentIndex = (currentIndex + 1) % sprites.Length; // loop through sprites
        spriteRenderer.sprite = sprites[currentIndex];
    }

    private IEnumerator SwitchSpriteLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(switchInterval);
            SwitchToNextSprite();
        }
    }
}
