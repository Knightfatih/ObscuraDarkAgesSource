using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollManager : MonoBehaviour
{
    Rigidbody[] rbs;
    Animator anim;
    void Start()
    {
        rbs = GetComponentsInChildren<Rigidbody>();
        anim = GetComponent<Animator>();

        DeactivateRagdoll();
    }

    public void DeactivateRagdoll()
    {
        foreach (var rb in rbs)
        {
            rb.isKinematic = true;
        }
        anim.enabled = true;
    }

    public void ActivateRagdoll()
    {
        foreach (var rb in rbs)
        {
            rb.isKinematic = false;
        }
        anim.enabled = false;
    }

    public void ApplyForce(Vector3 force)
    {
        var rb = anim.GetBoneTransform(HumanBodyBones.Hips).GetComponent<Rigidbody>();
        rb.AddForce(force, ForceMode.VelocityChange);
    }
}
