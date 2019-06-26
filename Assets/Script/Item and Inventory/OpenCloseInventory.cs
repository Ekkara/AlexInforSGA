using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenCloseInventory : MonoBehaviour
{
    public GameObject inventory;

    // Update is called once per frame
    void Update()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        if (Input.GetKeyDown(KeyCode.I) && !menuManager.IsInMenu && newMovement.canMove)
        {
            if(SceneManager.GetActiveScene().name != "Credits")
            {
                if (SceneManager.GetActiveScene().name != "main")
                {
                    inventory.SetActive(true);
                }
            }
        }
        else if (Input.GetButtonDown("Cancel") || Input.GetKeyDown(KeyCode.I))
        {
            inventory.SetActive(false);
        }
    }
}
