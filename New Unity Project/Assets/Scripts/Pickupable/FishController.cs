using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : Pickupable {
    
    protected override void Pickup()
    {
        _scriptablePlayer._lifes++;
    }
}
