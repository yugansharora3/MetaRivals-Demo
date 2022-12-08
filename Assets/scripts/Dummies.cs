using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dummies : MonoBehaviour
{
    public Vector3 currentTarget;
    private List<Vector3> targets;
    public float distance;
    public GameObject TargetsObj;
    private NavMeshAgent agent;

    public Animator anim;
    private float prevMag = 0.0f;
    void Start()
    {
        targets = new List<Vector3>();
        for(int i = 0;i < TargetsObj.transform.childCount;i++)
        {
            targets.Add(TargetsObj.transform.GetChild(i).transform.position);
        }
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 10;
        int index = Random.Range(0, targets.Count);
        currentTarget = targets[index];
    }

    void Update()
    {
        distance = Vector3.Distance(transform.position, currentTarget);
        if(distance < 1 || agent.velocity.magnitude == 0)
        {
            int index = Random.Range(0, targets.Count);
            currentTarget = targets[index];
        }
        
        agent.SetDestination(currentTarget);
        float mag = agent.velocity.magnitude/agent.speed;
        if(mag > 0.5f && prevMag < 0.5f)
        {
            mag = Mathf.Lerp(mag, prevMag, 0.1f);
        }

        if (mag < 0.5f && prevMag > 0.5f)
        {
            mag = Mathf.Lerp(mag, prevMag, 0.9f);
        }
        

        anim.SetFloat("moveSpeed", mag);
        if (mag == 0)
        {
            anim.SetBool("move", false);
        }
        prevMag = mag;
        //Debug.Log(agent.velocity.magnitude);
    }
}
