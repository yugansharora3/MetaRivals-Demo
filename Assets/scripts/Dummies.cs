using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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

    private static List<string> m_namesList;
    private TextAsset m_textAsset;
    TextMeshPro m_text;

    public GameObject TextObj;
    private void Awake()
    {
        m_textAsset = Resources.Load("TextFiles/names") as TextAsset;
        m_namesList = m_textAsset.text.Split('\n').ToList();
    }
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

        m_text = TextObj.GetComponent<TextMeshPro>();
        m_text.text = m_namesList[Random.Range(0, m_namesList.Count)];
    }

    void Update()
    {
        TextObj.transform.rotation = Camera.main.transform.rotation;

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
