using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHome : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.instance.uiHome.SetActive(false);
        GameManager.instance.uiGamePlay.SetActive(true);
        GameManager.instance.grid.SetupGameplay();
        
    }
}
