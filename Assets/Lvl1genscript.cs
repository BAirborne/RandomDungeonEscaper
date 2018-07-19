using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl1genscript : MonoBehaviour {

    public GameObject deadend;
    public GameObject corner;
    public GameObject hall;
    public GameObject fway;
    public GameObject tway;
    public GameObject full;
    public Vector2 startPos;

    // Use this for initialization
    void Start () {
        bool[,] map = new bool[10,10];

        Vector2 startPos = new Vector2(Mathf.Floor(Random.Range(0f, 10f)), Mathf.Floor(Random.Range(0f, 10f)));
        map[(int)startPos.x,(int)startPos.y] = true;
        var currentPoses = new List<Vector2>();
        currentPoses.Add(startPos);
        for (int i = 0; i < 20; i++)
        {
            var newPoses = new List<Vector2>();
            foreach(var creationPos in currentPoses)
            {
                var creationRoll = Random.Range(0f, 100f);
                if(i>5 && creationRoll < 10)
                {
                }
                else if(creationRoll < 60)
                {
                    if( creationRoll < 40)
                    {
                        var newX = (int)(creationPos.x + Mathf.Floor(Random.Range(-1f, 1.99f)));
                        map[newX,(int)creationPos.y] = true;
                        newPoses.Add(new Vector2(newX, (int)creationPos.y));
                    }
                    else
                    {
                        var newY = (int)(creationPos.y + Mathf.Floor(Random.Range(-1f, 1.99f)));
                        map[(int)creationPos.x, newY] = true;
                        newPoses.Add(new Vector2((int)creationPos.x, newY));
                    }
                }
                else if (creationRoll < 80)
                {
                        var newX = (int)(creationPos.x + Mathf.Floor(Random.Range(-1f, 1.99f)));
                        map[newX, (int)creationPos.y] = true;
                        newPoses.Add(new Vector2(newX, (int)creationPos.y));
                }
                else
                {
                    if (creationRoll < 40)
                    {
                        map[(int)creationPos.x + 1, (int)creationPos.y] = true;
                        map[(int)creationPos.x - 1, (int)creationPos.y] = true;
                        map[(int)creationPos.x, (int)creationPos.y + 1] = true;
                        map[(int)creationPos.x, (int)creationPos.y - 1] = true;
                        newPoses.Add(new Vector2((int)creationPos.x + 1, (int)creationPos.y));
                        newPoses.Add(new Vector2((int)creationPos.x - 1, (int)creationPos.y));
                        newPoses.Add(new Vector2((int)creationPos.x, (int)creationPos.y + 1));
                        newPoses.Add(new Vector2((int)creationPos.x, (int)creationPos.y - 1));
                    }
                }
            }
            currentPoses = newPoses;
        }


        var logString = "";
        var rowCount = 0;
        foreach(bool val in map)
        {
            if (val)
            {
                logString += "X";
            }
            else
            {
                logString += "O";
            }
            if (rowCount >= 10)
            {
                rowCount = 0;
                logString += "\n";
            }
            rowCount += 1;
        }
        Debug.Log(logString);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
