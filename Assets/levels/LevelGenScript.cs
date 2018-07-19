using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelGenScript : MonoBehaviour {

    private List<GameObject> unusedDoors;
    private List<RoomObject.RoomTypes> roomTypesOfUnusedDoors;
    private int totalDoors;
    public GameObject StartRoom;
    public GameObject[] MainRooms;
    public GameObject[] Corridors;
    public GameObject[] SideRooms;
    public GameObject startTile;
    public GameObject exitTile;
    public int MinTilesBeforeExit;
    public int MaxTilesBeforeExit;
    public int MaxTilesTotal;
    public GameObject NavMeshBaker;
    public GameObject Spawner;

    private bool[,,] levelMatrix;


    void Start()
    {
        levelMatrix = new bool[100, 100, 100];
        levelMatrix[50, 50, 50] = true;

        GenerateLevel(MainRooms, Corridors, SideRooms, StartRoom, MinTilesBeforeExit, MaxTilesBeforeExit, MaxTilesTotal);


    }

    void Update()
    {
    }

    public void GenerateLevel(GameObject[] tilesM, GameObject[] tilesC, GameObject[] tilesS, GameObject startRoom, int minTilesBeforeExit, int maxTilesBeforeExit, int maxTilesTotal)
    {
        bool exitExists = false;
        unusedDoors = new List<GameObject>();
        roomTypesOfUnusedDoors = new List<RoomObject.RoomTypes>();
        foreach (GameObject door in startRoom.GetComponent<RoomObject>().outerDoorsObjects)
        {
            unusedDoors.Add(door);
            roomTypesOfUnusedDoors.Add(RoomObject.RoomTypes.MainRoom);
        }
        int timeOut = 0;
        totalDoors = unusedDoors.Count;
        while (unusedDoors.Count > 0 && timeOut<40)
        {
            bool creatingExit = false;
            int indexOfDoorToUse = Mathf.FloorToInt(Random.Range(0, unusedDoors.Count));

            int lowerBound = 0;
            if (totalDoors < minTilesBeforeExit)
            {
                lowerBound = 1;
            }
            GameObject[] tiles = new GameObject[0];
            if (roomTypesOfUnusedDoors[indexOfDoorToUse] == RoomObject.RoomTypes.MainRoom)
            {
                Debug.Log("MR");
                tiles = tilesC;
                if (totalDoors > minTilesBeforeExit)
                {
                    var chanceForSideRoom = Random.Range(0, 100);
                    if (chanceForSideRoom < 50)
                    {
                        tiles = tilesS;
                    }
                }
            }
            else
            {
                Debug.Log("CR");
                tiles = tilesM;
                if (totalDoors > minTilesBeforeExit)
                {
                    var chanceForSideRoom = Random.Range(0, 100);
                    if (chanceForSideRoom < 25)
                    {
                        tiles = tilesS;
                    }
                }
            }
            int indexOfTileToUse = Mathf.FloorToInt(Random.Range(0, tiles.Length));
            GameObject tileToUse = tiles[indexOfTileToUse];
            if (timeOut > 10)
            {
                tileToUse = tilesS[0];
            }
            if (!exitExists && totalDoors > minTilesBeforeExit)
            {
                float randomChanceToBeExit = Random.Range(0, 100);
                if (randomChanceToBeExit < 10 || totalDoors > maxTilesBeforeExit - 1)
                {
                    tileToUse = exitTile;
                    creatingExit = true;
                }
            }
            else if(!exitExists && totalDoors >= maxTilesBeforeExit)
            {
                tileToUse = exitTile;
                creatingExit = true;
            }
            RoomObject roomOfTile = tileToUse.GetComponent<RoomObject>();







            Vector3Int intPositionOfDoorXandZ = new Vector3Int(
                Mathf.RoundToInt((unusedDoors[indexOfDoorToUse].transform.position * (1f / 10f)).x),
                0, 
                Mathf.RoundToInt((unusedDoors[indexOfDoorToUse].transform.position * (1f / 10f)).z));

            Vector3Int intPositionOfDoor = Vector3Int.RoundToInt(intPositionOfDoorXandZ +  new Vector3Int(0, Mathf.RoundToInt((unusedDoors[indexOfDoorToUse].transform.position * (1f / 6f)).y),0));
            intPositionOfDoor = intPositionOfDoor += new Vector3Int(50, 50, 50);
            Door inMaxtrixDoorToUse = new Door(intPositionOfDoor, (int)unusedDoors[indexOfDoorToUse].transform.eulerAngles.y, roomTypesOfUnusedDoors[indexOfDoorToUse]);
            int indexOfDoorToConnect = Mathf.FloorToInt(Random.Range(0, roomOfTile.innerDoorsObjects.Length));
            Vector3Int[] matrixCoordsOfNewRoom = roomOfTile.ToMatrixTiles(inMaxtrixDoorToUse, indexOfDoorToConnect);

            bool roomWillFit = true;
            foreach(var coordinate in matrixCoordsOfNewRoom)
            {
                if (levelMatrix[coordinate.x, coordinate.y, coordinate.z] == true)
                {
                    roomWillFit = false;
                }
            }
            
            //Check if door is on wall
            if (levelMatrix[intPositionOfDoor.x, intPositionOfDoor.y, intPositionOfDoor.z] == true)
            {
                unusedDoors.Remove(unusedDoors[indexOfDoorToUse]);
                
            }
            //Check tiles of new room
            //roomOfTile.InWorldDimensions
            else if (!roomWillFit)
            {
                timeOut += 1;
            }
            else if (timeOut>15)
            {
                
                unusedDoors.Remove(unusedDoors[indexOfDoorToUse]);
                timeOut = 0;
            }
            else if (roomOfTile.outerDoorsObjects.Length - 1 + totalDoors <= maxTilesTotal)
            {
                timeOut = 0;

                GameObject DoorToConnect = roomOfTile.innerDoorsObjects[indexOfDoorToConnect];
                GameObject createdRoom = Instantiate(tileToUse);
                RoomObject roomObjOfCreatedRoom = createdRoom.GetComponent<RoomObject>();

                float rotationDifference = roomObjOfCreatedRoom.innerDoorsObjects[indexOfDoorToConnect].transform.eulerAngles.y - unusedDoors[indexOfDoorToUse].transform.eulerAngles.y;
                createdRoom.transform.eulerAngles -= new Vector3(0, rotationDifference, 0);

                Vector3 PositionDifference = roomObjOfCreatedRoom.innerDoorsObjects[indexOfDoorToConnect].transform.position - unusedDoors[indexOfDoorToUse].transform.position;
                createdRoom.transform.position -= PositionDifference;
                
                foreach (var coordinate in matrixCoordsOfNewRoom)
                {
                    levelMatrix[coordinate.x, coordinate.y, coordinate.z] = true;
                }

                for (int i = 0; i < roomObjOfCreatedRoom.outerDoorsObjects.Length; i++)
                {
                    if (i != indexOfDoorToConnect)
                    {
                        totalDoors += 1;
                        roomTypesOfUnusedDoors.Add(roomObjOfCreatedRoom.RoomType);
                        unusedDoors.Add(roomObjOfCreatedRoom.outerDoorsObjects[i]);
                    }
                }

                float chanceToBeSpawner = Random.Range(0, 100);
                if (chanceToBeSpawner < 20)
                {
                    Instantiate(Spawner, createdRoom.transform.position + new Vector3(0,1,0), Quaternion.Euler(new Vector3(0, 0, 0)));
                }

                unusedDoors.RemoveAt(indexOfDoorToUse);
                roomTypesOfUnusedDoors.RemoveAt(indexOfDoorToUse);
                if (creatingExit) {
                    exitExists = true;
                }
            }
        }
        NavMeshBaker.GetComponent<NavMeshSurface>().BuildNavMesh();
        //string logString = "";
        //var x = 0;
        //for(int i = 99; x < 100; i--)
        //{
        //    if (levelMatrix[i, 0, x] == true)
        //    {
        //        logString += "█";
        //    }
        //    else 
        //    {
        //        logString += "_";
        //    }
        //    if (i == 0)
        //    {
        //        logString += "\n";
        //        i = 99;
        //        x++;
        //    }
        //}
        //Debug.Log(logString);
    }
}
