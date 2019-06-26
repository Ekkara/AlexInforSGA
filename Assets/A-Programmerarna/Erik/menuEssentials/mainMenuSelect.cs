using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class mainMenuSelect : MonoBehaviour {

    public UnityEvent onSelect;

    public void exit()
    {
        Debug.Log("exit");
        Application.Quit();
    }
    public void play()
    {
        try
        {
            bagCanvas.Instance.image.color = Color.white;
        }
        catch
        {
            Debug.Log("first time");
        }
        menuManager.Instance.inisiate = false;
        menuManager.Instance.menuState = menuManager.MenuState.noMenu;
        SceneController.instance.loadScene("Alex");
    }
    public void credtis()
    {
        menuManager.Instance.inisiate = false;
        menuManager.Instance.menuState = menuManager.MenuState.disabled;
        SceneController.instance.loadScene("Credits");
    }
}
