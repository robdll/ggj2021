using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public PlayerAbilityInfo playerabilityInfo;
    public GameObject itemGameObject;
    public abstract void Use();
}
