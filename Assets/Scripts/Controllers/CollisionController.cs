using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    private PlayerController pc;

    private void Awake()
    {
        pc = GetComponentInParent<PlayerController>();
        if (pc == null)
        {
            Debug.LogError("PlayerController component not found in parent!");
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 7)
        {
            pc.IsGrounded = true;
            Debug.Log("groundda");
        }
    }
}
