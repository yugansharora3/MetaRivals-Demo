using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCoin : MonoBehaviour
{
    public GameObject Coin;
    public GameObject Roads;
    // Start is called before the first frame update
    void Start()
    {
        //Generate();
    }

    public void Generate()
    {
        Coin.SetActive(true);
        int SelectedRoad = Random.Range(0, Roads.transform.childCount);
        Debug.Log("selected road = " + SelectedRoad);
        GameObject road = Roads.transform.GetChild(SelectedRoad).gameObject;
        Vector3 v = new(0.0f, 1.37f, 0.0f);
        int length = (int)Vector3.Scale(road.GetComponent<MeshFilter>().mesh.bounds.extents, road.transform.localScale).x;
        int offset = Random.Range(0, length);
        Debug.Log("extents " + Vector3.Scale(road.GetComponent<MeshFilter>().mesh.bounds.extents,road.transform.localScale));
        if (road.transform.rotation.y > 0.25f || road.transform.rotation.y < -0.25f)
        {
            v.z += offset;

        }
        else
        {
            v.x += offset;

        }
        Coin.transform.position = road.transform.localPosition + v ;
        Debug.Log(v);
    }

}
