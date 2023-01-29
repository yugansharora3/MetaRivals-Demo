using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hittable")
        {
            //Debug.Log("Hit");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
