using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotEye : Eye
{
    public override void Use()
    {
        Debug.Log("Using eyes" + playerabilityInfo.abilityName );
    }
}

