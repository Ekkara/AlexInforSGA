using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PottyRemain : MonoBehaviour
{
    [SerializeField] QuestSO nameOfQuest;
    [SerializeField] GameObject potty1, potty2;


	// Use this for initialization
	void Start ()
    {
        if(QuestManager.Instance.questExistsInCompletedQuests(nameOfQuest))
        {
            potty1.active = false;
            potty2.active = true;
        }
        else
        {
            potty1.active = true;
            potty2.active = false;
        }

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
