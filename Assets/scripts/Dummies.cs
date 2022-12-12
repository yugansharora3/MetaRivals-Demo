using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Dummies : MonoBehaviour
{
    public Vector3 currentTarget;
    private List<Vector3> targets;
    public float distance;
    public GameObject TargetsObj;
    public GameObject CoinParent;
    private NavMeshAgent agent;

    public Animator anim;
    private float prevMag = 0.0f;

    private List<string> m_namesList;
    private TextAsset m_textAsset;
    TextMeshPro m_text;

    public int index;
    public GameObject TextObj;
    public bool GoingForCoin = false;
    private Transform target;

    private List<int> CoinIndexes;
    private void Awake()
    {
        targets = new List<Vector3>();
        m_textAsset = Resources.Load("TextFiles/names") as TextAsset;
        m_namesList = m_textAsset.text.Split('\n').ToList();
        agent = GetComponent<NavMeshAgent>();
        CoinIndexes = Enumerable.Range(0, CoinParent.transform.childCount).ToList();
    }
    void Start()
    {
        for(int i = 0;i < TargetsObj.transform.childCount;i++)
        {
            targets.Add(TargetsObj.transform.GetChild(i).transform.position);
        }

        SetDestination();

        m_text = TextObj.GetComponent<TextMeshPro>();
        m_text.text = m_namesList[Random.Range(0, m_namesList.Count)];
    }

    void Update()
    {
        TextObj.transform.rotation = Camera.main.transform.rotation;
        distance = Vector3.Distance(transform.position, currentTarget);
        if(distance < 1)// || agent.velocity.magnitude == 0)
        {
            SetDestination();
        }
        
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    if (GoingForCoin)
                    {
                        CoinParent.transform.GetChild(index).gameObject.SetActive(false);
                    }
                    SetDestination();
                }
            }
        }

        SetAnimation();
    }

    bool ReturnIfCoinIsActive()
    {
        while (!CoinParent.transform.GetChild(index).gameObject.activeSelf || CoinIndexes.Count != 0)
        {
            int i = Random.Range(0, CoinIndexes.Count());
            index = CoinIndexes[i];
        }
        if(CoinIndexes.Count == 0)
            return false;
        return true;
    }

    void SetDestination()
    {
        
        int x = Random.Range(0, 5);
        if(x > 3)
        {

            while (!CoinParent.transform.GetChild(index).gameObject.activeSelf)
            {
                index = Random.Range(0, CoinParent.transform.childCount);
            }
            target = CoinParent.transform.GetChild(index);
            GoingForCoin = true;
        }
        else
        {
            index = Random.Range(0, TargetsObj.transform.childCount);
            target = TargetsObj.transform.GetChild(index);
            GoingForCoin = false;
        }
        currentTarget = target.position;
        agent.SetDestination(currentTarget);

        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(currentTarget, path);
        if (path.status == NavMeshPathStatus.PathPartial)
        {
            Debug.Log("Invalid target");
            Debug.Log(index);
        }
    }

    //private void OnCollisionEnter(Collision hit)
    //{
    //    if (hit.gameObject.tag == "Coin")
    //    {
    //        hit.gameObject.SetActive(false);
    //        Debug.Log("Dummy collided with coin");
    //    }
    //}
    //private void OnTriggerEnter(Collider hit)
    //{
    //    if (hit.gameObject.tag == "Coin")
    //    {
    //        hit.gameObject.SetActive(false);
    //        Debug.Log("Dummy collided with coin");
    //    }
    //}

    void SetAnimation()
    {
        float mag = agent.velocity.magnitude / agent.speed;
        if (mag > 0.5f && prevMag < 0.5f)
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
