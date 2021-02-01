using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public EyesInfo eyeInfo;
    public GameObject itemGameObject;

    public abstract void Use();
}
