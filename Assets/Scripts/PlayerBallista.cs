using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBallista : MonoBehaviour
{
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] float minTimeBetweenShots;
    float shotTimer;
    [field: SerializeField] public float shotPower { get; set; }
    [SerializeField] Transform shotSource;
    [SerializeField] Animator anim;
    public bool isEmpty;
    GameObject preppedArrow;
    public AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        isEmpty = true;
        Reload();
        
        //shotPower = 0;
    }
    void Update()
    {
        shotTimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootingLogic();
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Reload();
        }
    }
    public void ShootingLogic()
    {
        if (!isEmpty && shotTimer > minTimeBetweenShots)
        {
            Rigidbody arrowRB = preppedArrow.GetComponent<Rigidbody>();
            arrowRB.AddForce(-transform.forward * shotPower, ForceMode.Impulse);
            ReferenceManager.RM.clipManager.PlayClip(audioSource, ReferenceManager.RM.clipManager.shootingArrow);
            arrowRB.useGravity = true;
            preppedArrow.GetComponent<ArrowBehaviour>().isFlying = true;
            preppedArrow.transform.parent = null;
            anim.SetBool("Fire", false);
            isEmpty = true;
            shotTimer = 0;
        }
    }

    public void Reload()
    {
        if (isEmpty)
        {
            anim.SetBool("Fire", true);
            preppedArrow = Instantiate(arrowPrefab, shotSource.position, shotSource.rotation);
            ReferenceManager.RM.clipManager.PlayClip(audioSource, ReferenceManager.RM.clipManager.reloadArrow);
            preppedArrow.transform.parent = transform;
            isEmpty = false;
        }
    }

}
