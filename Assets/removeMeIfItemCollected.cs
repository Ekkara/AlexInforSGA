using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class removeMeIfItemCollected : MonoBehaviour {
    [SerializeField] Item item;


	// Use this for initialization
	void Start () {
        if (Inventory.instance.hasItem(item))
        {
            Destroy(gameObject);
        }	
	}
}
