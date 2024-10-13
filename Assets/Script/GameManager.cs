using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Grid grid;
    public TurnManager turnManager;
    public GameObject uiResult;
    public GameObject uiHome;
    public GameObject uiGamePlay;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        uiGamePlay.SetActive(false);
        uiResult.SetActive(false);
    }

}
