using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTwoUpdate : WallUpdate
{
    void Update()
    {
        if (onlyRotate)
        {
            RotateDuvara(Globals.WALL_KURMA.ilkDuvar);
            return;
        }
        else
        {
            RotateDuvara(Globals.WALL_KURMA.ilkDuvar);
            GridMove();
            
        }


    }
}
