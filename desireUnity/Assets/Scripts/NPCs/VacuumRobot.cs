using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumRobot : NPCMovement
{

    public GameObject trashBin;
    private float speed;

    public void CleanTrashBin ()
    {
        speed = 2;
        toggleMovement = true;
        GoTo (trashBin.transform.position, speed);
    }
}