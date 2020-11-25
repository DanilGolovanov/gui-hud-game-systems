using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressAnyKey : MonoBehaviour
{
    public GameObject pressAnyKeyPanel, menuPanel;

    void Start()
    {
        menuPanel.SetActive(false);
    }

    void Update()
    {
        if (pressAnyKeyPanel.activeSelf == true)
        {
            if (Input.anyKey)
            {
                menuPanel.SetActive(true);
                pressAnyKeyPanel.SetActive(false);
            }
        }
    }
}
