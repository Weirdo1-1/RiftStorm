using UnityEngine;
using UnityEngine.EventSystems;

public class MenuFocus : MonoBehaviour
{
    public GameObject buttonToSelect;

    public void RestoreFocus()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(buttonToSelect);
    }
}