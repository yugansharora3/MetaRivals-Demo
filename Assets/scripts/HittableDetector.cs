using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittableDetector : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "weapon")
        {
            animator.SetTrigger("Hit");
            Debug.Log(other.gameObject.name + "Got Hit");
        }
    }
}
