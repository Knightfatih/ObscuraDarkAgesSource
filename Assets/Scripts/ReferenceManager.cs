using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceManager : MonoBehaviour
{
    static public ReferenceManager RM;
    public GameObject Player { get; set; }
    public PlayerInventory playerInventory { get; private set; }
    public UIManager uIManager { get; private set; }
    public EnemySpawnManager enemySpawnManager;
    public WaveManager waveManager;
    public ClipManager clipManager;
    public HighscoreTable HighscoreTable;
    void Awake()
    {
        RM = this;
        Player = GameObject.FindGameObjectWithTag("Player");
        playerInventory = Player.GetComponent<PlayerInventory>();
        uIManager = GetComponent<UIManager>();
    }
}
