using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempController : MonoBehaviour
{
    private SpriteRenderer playerSprite, auraSprite;
    private bool readyToSwap;
    private float swapTime;
    private float coolDown = 0.5f;

    private Color playerRed = new Color(0.8f, 0.24f, 0.24f);
    private Color playerBlue = new Color(0.24f, 0.24f, 0.8f);
    private Color auraRed = new Color(0.8f, 0.14f, 0.14f, 0.3f);
    private Color auraBlue = new Color(0.14f, 0.14f, 0.8f, 0.3f);

    private bool isHot;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        isHot = true;
        readyToSwap = true;
        swapTime = 0.0f;
        playerSprite = gameObject.GetComponent<SpriteRenderer>();
        auraSprite = GameObject.Find("Temp Aura").gameObject.GetComponent<SpriteRenderer>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float currTime = Time.realtimeSinceStartup;
        
        if (!readyToSwap && currTime - swapTime >= coolDown)
        {
            swapTime = currTime;
            readyToSwap = true;
        }
        if (readyToSwap && Input.GetKeyDown(KeyCode.F))
        {
            TemperatureSwap();
        }
    }

    private void TemperatureSwap()
    {
        readyToSwap = false;
        if(isHot)
        {
            isHot = false;
            playerSprite.color = playerBlue;
            auraSprite.color = auraBlue;
        }
        else
        {
            isHot = true;
            playerSprite.color = playerRed;
            auraSprite.color = auraRed;
        }
    }

    public void HeatContact(bool otherIsHot)
    {
        if (otherIsHot == isHot)
        {
            gameManager.Death();
        }
    }

    public bool AuraContact(bool otherIsHot)
    {
        return otherIsHot != isHot;
    }
}
