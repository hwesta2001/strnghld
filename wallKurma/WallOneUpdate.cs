using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallOneUpdate : WallUpdate
{

    void Update()
    {
        if (onlyRotate)
        {
            RotateDuvara(Globals.WALL_KURMA.ikinciDuvar);
            return;
        }
        else
        {
            if (wallState!=1)
            {
                RotateDuvara(Globals.WALL_KURMA.ikinciDuvar);
            }

            GridMove();
        }
    }


}
