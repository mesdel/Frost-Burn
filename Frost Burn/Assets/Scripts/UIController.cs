using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject winPopup, losePopup;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WinScreen()
    {
        winPopup.SetActive(true);
    }

    public void LoseScreen()
    {
        losePopup.SetActive(true);
    }
}
