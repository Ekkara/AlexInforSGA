using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TailManager : MonoBehaviour
{
    public Text text;
    enum MoveState { move, wall, apple, bugApple };

    //LineRenderer lineRenderer;
    public Color frontColour = Color.black;

    public GameObject blockPrefab;
    public float tickTimer = 1.0f;
    public static List<GameObject> positionOccupation = new List<GameObject>();
    public int tailParsRemoved = 1;

    public int applesToeat;
    int applesEaten = 0;
    public List<GameObject> tailPart = new List<GameObject>();

    public UnityEvent victryEvent;
    // Update is called once per frame
    float f = 0;
    void Update()
    {
        f += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            move(Vector2.up);
            //dir = Vector2.up;//For automaticMovement
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            move(Vector2.left);
            //dir = Vector2.left;//For automaticMovement
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            move(Vector2.down);
            //dir = Vector2.down;//For automaticMovement
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            move(Vector2.right);
            //dir = Vector2.right;//For automaticMovement
        }

        if (f >= 5.0f)
        {
            float alpha = Mathf.Sin(f - 5.0f);
            text.color = new Color(1, 1, 1, alpha);
        }
        else
        {
            text.color = new Color(1, 1, 1, 0);
        }
    }



    void move(Vector3 vec)
    {
        Vector3 pos = tailPart[0].transform.position + vec;

        MoveState moveState = MoveState.move;

        for (int i = positionOccupation.Count - 1; i >= 0; i--)
        {
            if (positionOccupation[i].transform.position == pos)
            {
                if (positionOccupation[i].tag == "Apple")
                {
                    moveState = MoveState.apple;
                    Destroy(positionOccupation[i]);
                }
                else if (positionOccupation[i].tag == "BuggApple")
                {
                    moveState = MoveState.bugApple;
                    Destroy(positionOccupation[i]);
                }
                else
                {
                    moveState = MoveState.wall;

                    if (positionOccupation[i] == tailPart[tailPart.Count - 1] && tailPart.Count > 2)
                    {
                        moveState = MoveState.move;
                    }
                }
            }
        }

        switch (moveState)
        {
            case MoveState.move:
                if (tailPart.Count > 0)
                {
                    moveForwardLastBlock(pos);
                    f = 0;
                }
                break;


            case MoveState.apple:
                tailPart[0].GetComponent<SpriteRenderer>().color = Color.white;
                GameObject Tail = Instantiate(blockPrefab, pos, Quaternion.identity);
                tailPart.Insert(0, Tail);
                applesEaten++;
                tailPart[0].GetComponent<SpriteRenderer>().color = frontColour;
                if (applesEaten >= applesToeat)
                {
                    victryEvent.Invoke();
                }
                break;


            case MoveState.bugApple:
                int temp = tailPart.Count - 1 - tailParsRemoved;
                for (int y = tailPart.Count - 1; y > temp; y--)
                {
                    if (y > 0) tailPart.RemoveAt(y);
                }
                moveForwardLastBlock(pos);
                break;
        }
    }

    void moveForwardLastBlock(Vector3 pos)
    {
        tailPart[0].GetComponent<SpriteRenderer>().color = Color.white;
        tailPart[tailPart.Count - 1].transform.position = pos;
        tailPart.Insert(0, tailPart[tailPart.Count - 1]);
        tailPart.RemoveAt(tailPart.Count - 1);
        tailPart[0].GetComponent<SpriteRenderer>().color = frontColour;
    }


    private void Start()
    {
        bagCanvas.Instance.image.color = new Color(0, 0, 0, 0);
    }
    private void OnDisable()
    {
        bagCanvas.Instance.image.color = Color.white;
    }

}