using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canPlayerMove : MonoBehaviour {
    //vid behov andvänds DialogManager.Instance
    private static canPlayerMove instance;
    public static canPlayerMove Instance { get { return instance; } }
    public bool canMove;

    // Use this for initialization
    void Start ()
    {
        canMove = true;

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("There is too many canPlayerMove placed on scene");
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
