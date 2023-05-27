using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
    [SerializeField] Rigidbody RB;
    public bool isFlying = false;
    [SerializeField] float rotationSpeed;
    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (isFlying)
        {
            transform.Rotate(Time.deltaTime * -rotationSpeed, 0, 0);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (isFlying)
        {
            string otherTag = collision.gameObject.tag;
            switch (otherTag)
            {
                case "Ground":
                    MakeArrowStatic();
                    //AOE attack
                    //Smoke
                    break;
                case "Enemy":
                    MakeArrowStatic();
                    
                    collision.gameObject.GetComponentInParent<EnemyBehaviour>().OnDeath();
                    Bounce();
                    //AOE attack
                    //Smoke
                    break;
            }
        }
    }
    private void MakeArrowStatic()
    {
        ReferenceManager.RM.clipManager.PlayClip(audioSource, ReferenceManager.RM.clipManager.hitArrow);
        RB.velocity = Vector3.zero;
        RB.useGravity = false;
        isFlying = false;
        rotationSpeed = 0;
        Destroy(gameObject, 10);
    }
    // this makes the arrows stick to the enemies, it doesnt work with ragdolls but you guys are welcome to try it
    /// <summary>
    /// it works by once it hits an enemy, it finds all the children and loops through them, comparing their distance to the closing distance, 
    /// if it finds one with a closer distance it will assign that as the closest child.
    /// once the closest child is found it then parents the arrow to that child
    /// </summary>
    /*private void Stick(Transform shotObject) 
    {
        float closestDistance = 1;
        Transform closestPart = shotObject;
        Transform[] children = shotObject.GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            float distance = Vector3.Distance(transform.position, child.position);
            if (distance < closestDistance)
            {

                    closestDistance = distance;
                    closestPart = child;
                
            }
            transform.parent = closestPart;
            Debug.Log(closestPart);
            
        }
    }*/
    private void Bounce()
    {
        RB.useGravity = true;
        RB.mass = 1;
        isFlying = false;
        rotationSpeed = 0;
        Destroy(gameObject, 10);
    }
}
