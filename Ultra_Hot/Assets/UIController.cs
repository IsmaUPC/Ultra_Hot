using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    public GameObject menu;
    bool isActive = false;

    public void OnPressMenu() 
    {
        isActive= !isActive;
        menu.SetActive(isActive);
    }

}
