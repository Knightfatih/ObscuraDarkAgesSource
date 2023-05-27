using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] int pointsOnDeath;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] float warningDistance;
    [SerializeField] float attackingDistance;
    [SerializeField] int attackDamage;
    [SerializeField] Animator anim;
    GameObject player;
    PlayerInventory playerInventory;
    Renderer rend;
    float distanceToPlayer;
    bool hasWarned = false;
    bool isAlive =true;
    RagdollManager ragdollManager;
    AudioSource audioSource;
    void Start()
    {
        rend = GetComponent<Renderer>();
        audioSource = GetComponent<AudioSource>();
        ragdollManager = GetComponent<RagdollManager>();
        player = ReferenceManager.RM.Player;
        playerInventory = ReferenceManager.RM.playerInventory;
        agent.destination = player.transform.position;
        ReferenceManager.RM.clipManager.PlayClip(audioSource, ReferenceManager.RM.clipManager.walkingSkeleton);
        audioSource.loop = true;
        audioSource.Play();
    }
    private void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        WarningLogic();
        AttackLogic();
    }

    private void AttackLogic()
    {
        if (distanceToPlayer < attackingDistance)
        {
            audioSource.loop = false;
            agent.speed = 0;
            anim.SetBool("Attacking", true);
        }
    }

    private void WarningLogic()
    {
        if (distanceToPlayer < warningDistance && isAlive)
        {
            Vector3 relativePosition = player.transform.InverseTransformPoint(transform.position);

            if (!rend.isVisible && relativePosition.x > 0 && !hasWarned)
            {
                hasWarned = ReferenceManager.RM.uIManager.SendWarning("left");
            }
            else if (!rend.isVisible && relativePosition.x < 0 && !hasWarned)
            {
                hasWarned = ReferenceManager.RM.uIManager.SendWarning("right");
            }
        }
    }
    public void Attack()
    {
        ReferenceManager.RM.clipManager.PlayClip(audioSource, ReferenceManager.RM.clipManager.hitCastle);
        playerInventory.DamageHealth(attackDamage);
    }
    public void OnDeath()
    {
        GetComponent<BoxCollider>().enabled = false;
        ragdollManager.ActivateRagdoll();
        Vector3 deathDirection = (transform.position - player.transform.position);
        ragdollManager.ApplyForce(deathDirection * 5);
        isAlive = false;
         agent.speed = 0;
        GameManager.gm.AddScore(pointsOnDeath);
        Destroy(gameObject, 5);
        ReferenceManager.RM.enemySpawnManager.numberOfEnemiesOnScreen--;
        ReferenceManager.RM.enemySpawnManager.remainingEnemiesinWave--;
        ReferenceManager.RM.uIManager.SetWaveBarPercentage(ReferenceManager.RM.enemySpawnManager.remainingEnemiesinWave);
    }
}
