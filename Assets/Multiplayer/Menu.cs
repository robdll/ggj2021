using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public string menuName;
    public bool open;

    // Start is called before the first frame update
    public void Open()
    {
        open = true;
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    public void Close()
    {
        open = false;
        gameObject.SetActive(false);
    }
}
