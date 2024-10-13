using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private int Turn;

    private List<CreateShape> myShapes = new List<CreateShape>();

    private List<CreateShape> enemyShapes = new List<CreateShape>();

    [HideInInspector] public int isLose;

    [SerializeField] private TextMeshProUGUI txt_Point_Player;
    [SerializeField] private TextMeshProUGUI txt_Point_Bot;

    private int win_Point = 5;

    private int point_Player;
    private int point_Bot;

    private float timer;

    public int GetTurn()
    {
        return Turn;
    }
    public void StartTurnManager()
    {
        
        for (int i = 0; i < GameManager.instance.grid.shapeStore.createShapes.Count; i++)
        {
            if (i >= 3)
            {
                myShapes.Add(GameManager.instance.grid.shapeStore.createShapes[i]);
            }
            else
            {
                enemyShapes.Add(GameManager.instance.grid.shapeStore.createShapes[i]);
            }
        }
    }
    public void ReGame()
    {
        txt_Point_Bot.text = "0";
        txt_Point_Player.text = "0";
        point_Bot = 0;
        point_Player = 0;
        Turn = 0;
        timer = 20;
        isLose = 0;
    }

    private void Update()
    {
        if(GameManager.instance.uiGamePlay.activeSelf == true)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                ChangeTurn();
                GameEvent.MoveShapeToStartPosition();
                if (Turn % 2 == 0)
                {
                    if (CheckResults() != 1 && CheckResults() != -1)
                    {
                        GameManager.instance.grid.PutShapeEnemy();
                    }

                }
            }
        }
    }

    public int CheckResults()
    {
        if (point_Bot == win_Point) return -1;
        else if (point_Player == win_Point) return 1;

        var shapes = Turn % 2 == 1 ? myShapes : enemyShapes;
        int numberShapeNotNull = 0;
        foreach (var item in shapes)
        {
            if (item.transform.childCount == 0) continue;

            numberShapeNotNull++;
            if (!item.IsDrag && item.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite == item.DisableDrag)
                isLose++;
        }
        if ((isLose == numberShapeNotNull && numberShapeNotNull!=0) && Turn % 2 == 1) return -1;
        else if ((isLose == numberShapeNotNull && numberShapeNotNull != 0) && Turn % 2 == 0) return 1;
        
        isLose = 0;
        return 0;
    }

    public void ChangeTurn()
    {
        Turn++;
        for (int i = 0; i < GameManager.instance.grid.shapeStore.createShapes.Count; i++)
        {
            if (i < 3)
            {
                GameManager.instance.grid.shapeStore.createShapes[i].gameObject.GetComponent<BoxCollider2D>().enabled = Turn % 2 == 0;

                if(!GameManager.instance.grid.shapeStore.createShapes[i].IsDrag) GameManager.instance.grid.shapeStore.createShapes[i].gameObject.GetComponent<BoxCollider2D>().enabled = GameManager.instance.grid.shapeStore.createShapes[i].IsDrag;

            }
            else
            {
                GameManager.instance.grid.shapeStore.createShapes[i].gameObject.GetComponent<BoxCollider2D>().enabled = Turn % 2 == 1;
                if (!GameManager.instance.grid.shapeStore.createShapes[i].IsDrag) GameManager.instance.grid.shapeStore.createShapes[i].gameObject.GetComponent<BoxCollider2D>().enabled = GameManager.instance.grid.shapeStore.createShapes[i].IsDrag;
            }
        }
        timer = 20;
    }

    public void GetPonit()
    {
        if (Turn % 2 == 1) point_Player++;
        else point_Bot++;

        txt_Point_Player.text = point_Player.ToString();
        txt_Point_Bot.text = point_Bot.ToString();
    }

    
}
