using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DestructiveChoice : Selectable, ISubmitHandler
{
    public GameObject confirmPanel;
    public void OnSubmit(BaseEventData eventData)
    {
        if(confirmPanel != null)
        {
            Instantiate(confirmPanel);
        }
        else
        {
            throw new System.NotImplementedException();
        }
    }
}