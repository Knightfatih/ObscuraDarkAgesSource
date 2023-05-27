using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class GameManager : MonoBehaviour
{
    static public GameManager gm;

    public int score { get; set; }
    
    void Start()
    {
        //this has to be moved to scene load!!!!
        gm = this;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        ReferenceManager.RM.uIManager.UpdateScoreText(score);
        Time.timeScale = 1;
    }
    public void SendScore()
    {
        ReferenceManager.RM.HighscoreTable.AddHighscoreEntry(score, DateTime.Now.ToString());
    }
    public void AddScore(int points)
    {
        score += points;
        ReferenceManager.RM.uIManager.UpdateScoreText(score);
    }
}
