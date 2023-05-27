using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipManager : MonoBehaviour
{
    [field: SerializeField] public AudioClip shootingArrow { get; private set; }
    [field: SerializeField] public AudioClip hitArrow { get; private set; }
    [field: SerializeField] public AudioClip reloadArrow { get; private set; }
    [field: SerializeField] public AudioClip hitCastle { get; private set; }
    [field: SerializeField] public AudioClip hitEnemy { get; private set; }
    [field: SerializeField] public AudioClip walkingSkeleton { get; private set; }
    [field: SerializeField] public AudioClip fanfareOne { get; private set; }
    [field: SerializeField] public AudioClip fanfareTwo { get; private set; }
    [field: SerializeField] public AudioClip fanfareThree { get; private set; }
    [field: SerializeField] public AudioClip fanfareFour { get; private set; }
    [field: SerializeField] public AudioClip death { get; private set; }


    public void PlayClip(AudioSource source ,AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }
}
