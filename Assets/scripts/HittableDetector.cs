using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HittableDetector : MonoBehaviour
{
    public Animator animator;
    private GameObject parent;
    private Slider Health;
    // Start is called before the first frame update
    void Start()
    {
        PlayerManager manager = GameObject.Find("Manager").GetComponent<PlayerManager>();
        if(transform.IsChildOf(manager.Player1.transform))
        {
            parent = manager.Player1.transform.GetChild(0).gameObject;
            Health = manager.Player1HealthBar;
            Debug.Log("child of " + manager.Player1.name);
        }
        else
        {
            if(transform.IsChildOf(manager.Player2.transform))
            {
                parent = manager.Player2.transform.GetChild(0).gameObject;
                Health = manager.Player2HealthBar;
                Debug.Log("child of " + manager.Player2.name);
            }
        }
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "weapon")
        {
            if(!parent.GetComponent<PlayerCommon>().GotHit)
            {
                animator.SetTrigger("Hit");
                Health.value -= 0.2f;
                Debug.Log(gameObject.name + " Got Hit");
                parent.GetComponent<PlayerCommon>().GotHit = true;
            }
        }
    }
}
