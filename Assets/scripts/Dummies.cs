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
        if(distance < 1 )
        {
            int index = Random.Range(0, targets.Count);
            currentTarget = targets[index];
        }
        
        agent.SetDestination(currentTarget);

        Debug.Log(agent.velocity);
    }
}
