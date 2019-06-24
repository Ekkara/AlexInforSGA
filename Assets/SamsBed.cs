using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamsBed : MonoBehaviour {
    [SerializeField] QuestSO quest;
    [SerializeField] Sprite newBed;
    [SerializeField] SpriteRenderer sr;

	// Use this for initialization
	void Start ()
    {
        if(QuestManager.Instance.questExistsInCompletedQuests(quest))
        {
            ChangeBed();
        }		
	}
   public void ChangeBed()
    {
        sr.sprite = newBed;
    }
}
