using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    /*
    ok so ive got my board.
    the board consists of spaces. spaces have properties. 
    the board has agents on it. agents get to make a move when its their turn. 
        moves can consist of short animations, and function calls. 
        moves can alter the spaces on the board, and the position of the player. 
    
    maybe ill do a hex grid later instead of a grid. 

    board : gets "initiative" of all the agents, calls their turns in order
        maintains a sorted list of the agents and adds and removes when necessary. 
            each agent has a "period" value and a "time" value , used to sort the array. After a turn is made, the 
                agent is popped, its "time" is incremented by its "period" and its reinserted. 
            for now let me consider supporting a "period" value and a move-based increment. 
    */

    public int width;
    public int height;

    public Space[][] spaces;
    public List<Agent> agents;
    public static GameBoard instance ;
    public GameObject spacePrefab;

    bool awaitingTurnCompletion = false;
    int turnLength;
    // Start is called before the first frame update

    void Awake()
    {
        instance= this;
        spaces = new Space[width][];
        for(int x = 0 ; x < width; x ++)
        {
            spaces[x] = new Space[height];
            for(int y = 0 ; y < height; y++ )
            {
                Vector3 at= new Vector3( transform.position.x - width/2 * 2 + x*2 ,transform.position.y - height/2 * 2 + y*2, transform.position.z );
                spaces[x][y] = Instantiate(spacePrefab,at,Quaternion.identity).GetComponent<Space>();
                spaces[x][y].x = x;
                spaces[x][y].y = y;
                spaces[x][y].occupied= false;
            }
        }
        
    }
    void Start()
    {
        agents[0].Init(spaces[0][0]);
        agents[1].Init(spaces[1][0]);
        spaces[0][0].occupied = true;
        spaces[1][0].occupied = true;
    }

    void NextTurn()
    {
        agents[0].TurnStart();
        print("Starting turn of: " + agents[0].name);
        Agent t = agents[0];
        agents.RemoveAt(0);
        agents.Add(t);
    }
    
    public void NotifyTurnCompleted(int turnLength)
    {
        awaitingTurnCompletion = false;
        this.turnLength = turnLength; 
    }
    // Update is called once per frame
    void Update()
    {
        if(!awaitingTurnCompletion)
        {
            NextTurn();
            awaitingTurnCompletion = true;
        }
    }

}
