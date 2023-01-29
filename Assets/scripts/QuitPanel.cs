using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Set()
    {
        GetComponent<Animator>().SetBool("Pop", true);
    }
    public void UnSet()
    {
        GetComponent<Animator>().SetBool("Pop", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
