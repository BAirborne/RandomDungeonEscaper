using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomObject : MonoBehaviour {

    public enum RoomTypes{ MainRoom, Corridor, SideRoom };
    public Vector3Int size;
    public GameObject[] innerDoorsObjects;
    public GameObject[] outerDoorsObjects;
    Door[] idoors;
    Door[] odoors;
    public RoomTypes RoomType;


    // Use this for initialization  
    void Start () {
        idoors = new Door[innerDoorsObjects.Length];
        odoors = new Door[outerDoorsObjects.Length];

        for (var i = 0; i < innerDoorsObjects.Length;i++)
        {
            idoors[i] = new Door(Vector3Int.RoundToInt(innerDoorsObjects[i].transform.position * (1f/10f)), (int)innerDoorsObjects[i].transform.eulerAngles.y, RoomType);
            idoors[i].location += new Vector3Int(0, (int)(innerDoorsObjects[i].transform.position.y * (1f / 6f)- idoors[i].location.y),0);
        }
        for (var i = 0; i < outerDoorsObjects.Length; i++)
        {
            odoors[i] = new Door(Vector3Int.RoundToInt(outerDoorsObjects[i].transform.position * (1f / 10f)), (int)outerDoorsObjects[i].transform.eulerAngles.y, RoomType);
            odoors[i].location += new Vector3Int(0, (int)(innerDoorsObjects[i].transform.position.y * (1f / 6f) - idoors[i].location.y), 0);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public Vector3Int[] ToMatrixTiles(Door doorToBeConnected, int door)
    {
        Start();
        Vector3Int[] tiles = new Vector3Int[size.x*size.y*size.z];
        var doorToUse = idoors[door];
        //Rotate Room
        var rotatedSize = size;
        var rotatedDoorLocation = new Vector3Int(doorToUse.location.x, doorToUse.location.y, doorToUse.location.z);
        var rotatedDoor = new Door(rotatedDoorLocation, doorToUse.direction, RoomType);
        var totalDoorRotation = (doorToBeConnected.direction - doorToUse.direction) % 360;
        if (totalDoorRotation < 0)
        {
            totalDoorRotation += 360;
        }
        if (totalDoorRotation == 90)
        {
            rotatedDoor.location = new Vector3Int(doorToUse.location.z,
                doorToUse.location.y,
                size.x - 1 - doorToUse.location.x);
            rotatedSize.x = size.z;
            rotatedSize.z = size.x;
        }
        else if (totalDoorRotation == 180)
        {
            rotatedDoor.location = new Vector3Int(size.z - 1 - doorToUse.location.z,
                doorToUse.location.y,
                size.x - 1 - doorToUse.location.x);
        }
        else if (totalDoorRotation == 270)
        {
            rotatedDoor.location = new Vector3Int(size.z - 1 - doorToUse.location.z,
                doorToUse.location.y,
                doorToUse.location.x);
            rotatedSize.x = size.z;
            rotatedSize.z = size.x;
        }

        //Find Tiles
        int i = 0;
        for(int z = doorToBeConnected.location.z - rotatedDoor.location.z;
            z < doorToBeConnected.location.z - rotatedDoor.location.z + rotatedSize.z;
            z++)
        {
            for(int y = doorToBeConnected.location.y - rotatedDoor.location.y;
                y < doorToBeConnected.location.y - rotatedDoor.location.y + rotatedSize.y;
                y++)
            {
                for (int x = doorToBeConnected.location.x - rotatedDoor.location.x;
                    x < doorToBeConnected.location.x - rotatedDoor.location.x + rotatedSize.x;
                    x++)
                {
                    //Debug.Log(x + "," + y + "," + z);
                    tiles[i] = new Vector3Int(x, y, z);
                    i++;
                }
            }
        }
        return tiles;

    }
    
    public Door[] getODoors()
    {
        return odoors;
    }

    public Door[] RefreshDoors()
    {
        idoors = new Door[innerDoorsObjects.Length];
        odoors = new Door[outerDoorsObjects.Length];

        for (var i = 0; i < innerDoorsObjects.Length; i++)
        {
            idoors[i] = new Door(Vector3Int.RoundToInt(innerDoorsObjects[i].transform.position * (1f / 10f)), (int)innerDoorsObjects[i].transform.eulerAngles.y, RoomType);
            idoors[i].location += new Vector3Int(0, (int)(innerDoorsObjects[i].transform.position.y * (1f / 6f) - idoors[i].location.y), 0);
        }
        for (var i = 0; i < outerDoorsObjects.Length; i++)
        {
            odoors[i] = new Door(Vector3Int.RoundToInt(outerDoorsObjects[i].transform.position * (1f / 10f)), (int)outerDoorsObjects[i].transform.eulerAngles.y, RoomType);
            odoors[i].location += new Vector3Int(0, (int)(innerDoorsObjects[i].transform.position.y * (1f / 6f) - idoors[i].location.y), 0);
        }
        return odoors;
    }
}
