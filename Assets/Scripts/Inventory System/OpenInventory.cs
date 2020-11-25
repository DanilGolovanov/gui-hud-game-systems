using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInventory : MonoBehaviour
{
    [SerializeField]
    private GameObject inventoryScreen;
    [SerializeField]
    private GameObject consumables;

    // Update is called once per frame
    void Update()
    {
        if (!inventoryScreen.activeSelf && Input.GetKeyDown(KeyCode.T))
        {
            inventoryScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            consumables.SetActive(false);

        }
        else if (inventoryScreen.activeSelf && Input.GetKeyDown(KeyCode.T))
        {
            inventoryScreen.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            consumables.SetActive(true);
        }
    }
}
