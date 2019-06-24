﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct movementType
{
    public string nameOfAnimation;
    public Texture2D animationSpriteSheet;
    public int amountOfFrames;
    public float animationSpeed;
    public bool flipAnimation;
    public Vector2 direction;
    public float speed;
    public int[] frameOfStep;
    [HideInInspector] public int prioOrder;
}

public class newMovement : MonoBehaviour
{
    //[HideInInspector] public static bool canMove;

    [SerializeField] Item itemNeedToRun;
    bool isRunning = false;

    bool animationBreak = true;
    [SerializeField] movementType[] allAnimations;
    [SerializeField] movementType[] runAnimations;
    movementType prevousilyType;
    string ActivatedKeyCodeState = "Idle";

    SpriteRenderer sr;
    Rigidbody2D rb;
    AudioHandler footStep;

    bool rightIsPressed = false;
    bool leftIsPressed = false;
    bool upIsPressed = false;
    bool downIsPressed = false;
    movementType prioMovmentType;

    // Use this for initialization
    void Start()
    {
        //canMove = true;


        footStep = gameObject.GetComponent<AudioHandler>();

        sr = gameObject.AddComponent<SpriteRenderer>();
        sr.sortingOrder = 10;
        sr.sortingLayerName = "Moving";
        rb = gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine(playAnimation(findAnimation("Idle")));
    }

    // Update is called once per frame
    void Update()
    {
        switch (ActivatedKeyCodeState)
        {
            case "Idle": //Idle state
                if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    updateAnimation("Right");
                    ActivatedKeyCodeState = "Right";
                }
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    updateAnimation("Left");
                    ActivatedKeyCodeState = "Left";
                }
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    updateAnimation("Up");
                    ActivatedKeyCodeState = "Up";
                }
                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    updateAnimation("Down");
                    ActivatedKeyCodeState = "Down";
                }
                break;
            case "Right": //Right
                if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
                {
                    pushDownOrder("Right");
                    lookForQuedMovment("Right");
                    updateAnimation(prioMovmentType.nameOfAnimation);
                    ActivatedKeyCodeState = prioMovmentType.nameOfAnimation;
                }
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    updateAnimation("Left");
                    ActivatedKeyCodeState = "Left";
                }
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    updateAnimation("Up");
                    ActivatedKeyCodeState = "Up";
                }
                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    updateAnimation("Down");
                    ActivatedKeyCodeState = "Down";
                }
                break;
            case "Left": //Left
                if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
                {
                    pushDownOrder("Left");
                    lookForQuedMovment("Left");
                    updateAnimation(prioMovmentType.nameOfAnimation);
                    ActivatedKeyCodeState = prioMovmentType.nameOfAnimation;
                }
                if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    updateAnimation("Right");
                    ActivatedKeyCodeState = "Right";
                }
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    updateAnimation("Up");
                    ActivatedKeyCodeState = "Up";
                }
                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    updateAnimation("Down");
                    ActivatedKeyCodeState = "Down";
                }
                break;
            case "Up": //Up
                if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
                {
                    pushDownOrder("Up");
                    lookForQuedMovment("Up");
                    updateAnimation(prioMovmentType.nameOfAnimation);
                    ActivatedKeyCodeState = prioMovmentType.nameOfAnimation;
                }
                if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    updateAnimation("Right");
                    ActivatedKeyCodeState = "Right";
                }
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    updateAnimation("Left");
                    ActivatedKeyCodeState = "Left";
                }
                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    updateAnimation("Down");
                    ActivatedKeyCodeState = "Down";
                }
                break;
            case "Down": //Down
                if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
                {
                    pushDownOrder("Down");
                    lookForQuedMovment("Down");
                    updateAnimation(prioMovmentType.nameOfAnimation);
                    ActivatedKeyCodeState = prioMovmentType.nameOfAnimation;
                }
                if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    updateAnimation("Right");
                    ActivatedKeyCodeState = "Right";
                }
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    updateAnimation("Left");
                    ActivatedKeyCodeState = "Left";
                }
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    updateAnimation("Up");
                    ActivatedKeyCodeState = "Up";
                }
                break;

            default:
                Debug.LogError("Error 404: state in newMovement not found!");
                break;
        } // input controll
        #region track if movment keys are pressed
        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            downIsPressed = false;
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            rightIsPressed = false;
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            upIsPressed = false;
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            leftIsPressed = false;
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            downIsPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            rightIsPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            upIsPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            leftIsPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            toggleSprint();
        }
        #endregion
    }
    void toggleSprint()
    {
        if (Inventory.instance.hasItem(itemNeedToRun))
        {
            isRunning = !isRunning;
            transform.GetChild(0).gameObject.active = isRunning;
        }
    }
    void pushDownOrder(string nameOfPush)
    {
        movementType pushed = findAnimation(nameOfPush);
        foreach (movementType mov in allAnimations)
        {
            movementType temp = findAnimation(mov.nameOfAnimation);
            temp.prioOrder--;
        }
        pushed.prioOrder = 1;
    }
    void lookForQuedMovment(string oldMovment)
    {
        List<movementType> states = new List<movementType>();
        if (leftIsPressed && findAnimation(oldMovment).nameOfAnimation != "Left") { states.Add(findAnimation("Left")); }
        if (rightIsPressed && findAnimation(oldMovment).nameOfAnimation != "Right") { states.Add(findAnimation("Right")); }
        if (upIsPressed && findAnimation(oldMovment).nameOfAnimation != "Up") { states.Add(findAnimation("Up")); }
        if (downIsPressed && findAnimation(oldMovment).nameOfAnimation != "Down") { states.Add(findAnimation("Down")); }

        if (states.Count <= 0)
        {
            prioMovmentType = findAnimation("Idle");
            return;
        }
        movementType havePrio = states[0];
        for (int i = 0; i < states.Count; i++)
        {
            if (havePrio.prioOrder <= states[i].prioOrder)
            {
                havePrio = states[i];
            }
        }
        prioMovmentType = havePrio;
        return;
    }
    void updateAnimation(string animationName)
    {
        StopAllCoroutines();
        StartCoroutine(playAnimation(findAnimation(animationName)));
    }
    movementType findAnimation(string animationName)
    {
        for (int i = 0; i < allAnimations.Length; i++)
        {
            if (allAnimations[i].nameOfAnimation == animationName)
            {
                if (isRunning)
                {
                    return runAnimations[i];
                }

                else
                {
                    return allAnimations[i];
                }
            }
        }
        Debug.LogError("No Animation Was Found with the name " + animationName + " , make Sure You Had The Right Name Or That The Animation Exist");
        return allAnimations[0];
    }

    IEnumerator playAnimation(movementType currentAnimationType)
    {
        StartCoroutine(movePlayer(currentAnimationType));
        //set the animation varibles to match the animation type
        int frameAt = 0;
        Texture2D currentAnimation = currentAnimationType.animationSpriteSheet;
        int frameCount = currentAnimationType.amountOfFrames;
        float animationPlaySpeed = currentAnimationType.animationSpeed;
        prevousilyType = currentAnimationType;
        int[] playFootStepAt = currentAnimationType.frameOfStep;

        float waitTime = animationPlaySpeed / frameCount;

        while (true)
        {
            if (canPlayerMove.Instance.canMove)
            {
                if (frameCount <= frameAt) { frameAt = 0; }

                Rect drawedImage = new Rect(64 * frameAt, 0, 64, 64);
                Sprite currentRect = Sprite.Create(currentAnimation, drawedImage, new Vector2(0.5f, 0.5f), 100.0f);

                if (currentAnimationType.flipAnimation)
                {
                    sr.flipX = true;
                }
                else
                {
                    sr.flipX = false;
                }

                sr.sprite = currentRect;

                if (playFootStepAt.Length == 2)
                {
                    int playFirstFootStepAt = playFootStepAt[0];
                    int playSecondFootStepAt = playFootStepAt[1];
                    if (playFirstFootStepAt == frameAt)
                    {
                        footStep.PlayFootsteps();
                    }
                    if (playSecondFootStepAt == frameAt)
                    {
                        footStep.PlayFootsteps();
                    }
                }
                else if (playFootStepAt.Length == 1)
                {
                    if (playFootStepAt[0] == frameAt)
                    {
                        footStep.PlayFootsteps();
                    }
                }
                yield return new WaitForSeconds(waitTime);
                frameAt++;
            }
        }
    }
    IEnumerator movePlayer(movementType movementType)
    {
        Vector3 dir = movementType.direction;
        float speed = movementType.speed;

        while (true)
        {
            if (canPlayerMove.Instance.canMove)
            {
                yield return null;
                rb.MovePosition(transform.position + dir * speed * Time.fixedDeltaTime);
            }
        }
    }
}
