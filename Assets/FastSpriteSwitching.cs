using UnityEngine;

public class FastSpriteSwitching : MonoBehaviour
{
    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    private int currentIndex = 0;


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
        } 
    }

    public void SwitchToNextSprite()
    {
        if (sprites.Length == 0) return;

        currentIndex = (currentIndex + 1) % sprites.Length; // loop through sprites
        spriteRenderer.sprite = sprites[currentIndex];
    }

    // Update is called once per frame
    void Update()
    {
        SwitchToNextSprite();
    }
}
