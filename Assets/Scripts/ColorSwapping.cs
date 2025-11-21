using UnityEngine;
using System.Collections;


public class ColorSwapping : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public float switchInterval = 0.2f; // half a second between switches
    public bool isOn = true;
    private int counter = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.Log("SpriteRenderer component not found.");
            enabled = false;
        }
        if (isOn == true)
        {
            spriteRenderer.color = Color.red;
            StartCoroutine(SwitchSpriteLoop());

        }
    }

    public void SwitchToNextSpriteColor()
    {
        if (counter % 2 == 0)
        {
            spriteRenderer.color = Color.yellow;
        }
        else
        {
            spriteRenderer.color = Color.red;
        }
        counter++;
    }

    private IEnumerator SwitchSpriteLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(switchInterval);
            SwitchToNextSpriteColor();
        }
    }
}
