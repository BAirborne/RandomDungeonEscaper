using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door {

    public Vector3Int location { get; set; }
    public int direction { get; set; }
    private RoomObject.RoomTypes DoorOriginRoomType;

    public Door(Vector3Int location, int direction, RoomObject.RoomTypes DoorOriginRoomType)
    {
        this.location = location;
        this.direction = direction;
        this.DoorOriginRoomType = DoorOriginRoomType;
    }

    public RoomObject.RoomTypes GetDoorOriginRoomType()
    {
        return DoorOriginRoomType;
    }
}
