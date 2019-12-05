using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    GameBoard gb;
    Space on;
    bool myTurn = false;
    public int myTime;
    public int mySpeed;
    public string name;

    public void Init(Space space)
    {
        on = space;
        transform.position = on.transform.position;
    }

    public void TurnStart()
    {
        myTurn = true;
    }

    void DoTurn()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            if(TryMove(on.x-1,on.y))
                return;
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            if(TryMove(on.x+1,on.y))
            return;
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            if(TryMove(on.x,on.y-1))
            return;
        }
        if(Input.GetKeyDown(KeyCode.W))
        {
            if(TryMove(on.x,on.y+1))
            return;
        }
    }

    bool TryMove(int x, int y)
    {
        if(x >= 0 && y >=0 && x < GameBoard.instance.spaces.Length && y < GameBoard.instance.spaces[0].Length)//assuming square board
        {
            if(!GameBoard.instance.spaces[x][y].occupied)
            {
                on.occupied = false;
                on = GameBoard.instance.spaces[x][y];
                on.occupied = true;
                transform.position = on.transform.position;
                GameBoard.instance.NotifyTurnCompleted(0);
                myTurn = false;
                print(name + " moving to " + x + " "+ y);
                return true;
            }
        }
            print("Invalid Move, space occupied");
            return false;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        gb = GameBoard.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(myTurn)
            DoTurn();
    }
}
