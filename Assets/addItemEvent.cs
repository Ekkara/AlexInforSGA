using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addItemEvent : MonoBehaviour {

    public Item item;
    [Tooltip("Removes the sprite from the map when picked up if 'true'")]
    public bool removeOnPickup = false;
    public AudioClip pickupClip;
    
    public void pickup()
    {
        if (pickupClip != null) AudioManager.instance.playSFXClip(pickupClip);
        Debug.Log("Grabbed " + item.name);
        Inventory.instance.AddItem(item);
        if (removeOnPickup)
        {
            Destroy(this.gameObject);
        }
    }    
}
