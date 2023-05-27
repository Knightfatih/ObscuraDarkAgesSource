using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [field: SerializeField] public int maxPlayerHealth { get; private set; }
    int currentHealth;
    AudioSource audioSource;
    bool isAlive;
    void Start()
    {
        currentHealth = maxPlayerHealth;
        audioSource = GetComponent<AudioSource>();
        isAlive = true;
    }
    public void DamageHealth(int damage)
    {
        currentHealth -= damage;
        ReferenceManager.RM.uIManager.SetHealthBarPercentage(currentHealth);
        if (currentHealth < 0 && isAlive ==true)
        {
            GameManager.gm.SendScore();
            ReferenceManager.RM.uIManager.DisplayDeath();
            ReferenceManager.RM.clipManager.PlayClip(audioSource, ReferenceManager.RM.clipManager.death);
            isAlive = false;
        }
    }
}
