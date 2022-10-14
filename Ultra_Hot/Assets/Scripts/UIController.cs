using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    public GameObject menu;
    public Transform playerTransform;
    public TransitionManager transition;
    public float distMenu= 2;
    bool isActive = false;


    public void OnPressMenu() 
    {
        isActive= !isActive;
        menu.SetActive(isActive);

        if (isActive) 
        {
            menu.transform.position = playerTransform.position + (playerTransform.forward* distMenu);
            menu.transform.LookAt(playerTransform);
            menu.transform.forward= (-menu.transform.forward) ;
        }

    }

    public void LoadLobby()
    {
        transition.GoToScene(0);
    }

    public void ReloadLevel()
    {
        //transition.GoToScene(0);
    }

}
