using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatObject : MonoBehaviour
{
    public bool isHot;
    public bool isObstacle;
    TempController playerTemp;

    // Start is called before the first frame update
    void Start()
    {
        playerTemp = GameObject.Find("Player").GetComponent<TempController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")
            || collision.gameObject.CompareTag("Feet"))
        {
            playerTemp.HeatContact(isHot);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isObstacle && collision.gameObject.CompareTag("Aura"))
        {
            if (playerTemp.AuraContact(isHot))
                Destroy(gameObject);
        }
    }
}
