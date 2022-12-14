using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GenerateCoin : MonoBehaviour
{
    public GameObject Coin;
    public GameObject CoinsParent;
    public GameObject InsidePlatforms;
    public GameObject OutsidePlatforms;
    public GameObject ScoreBoard;
    int InsideCoins = 15, OutsideCoins = 20;
    public int score = 0,TotalCoins = 0;

    public void Awake()
    {
        for (int i = 0; i < InsideCoins; i++)
            Generate(InsidePlatforms);
        
        for (int i = 0; i < OutsideCoins; i++)
            Generate(OutsidePlatforms);
        TotalCoins = InsideCoins + OutsideCoins;
        UpdateScore();
    }

    public void UpdateScore()
    {
        TextMeshProUGUI Text = ScoreBoard.GetComponent<TextMeshProUGUI>();
        Text.text = "MRVL Points : " + score + "/" + TotalCoins;
    }
    public void IncreaseScore()
    {
        score++;
    }

    public void Generate(GameObject Platforms)
    {
        int SelectedRoad = Random.Range(0, Platforms.transform.childCount);
        //Debug.Log("selected road = " + SelectedRoad);
        GameObject road = Platforms.transform.GetChild(SelectedRoad).gameObject;
        Vector3 v = new(0.0f, 4.0f, 0.0f);
        int length = (int)Vector3.Scale(road.GetComponent<MeshFilter>().mesh.bounds.extents, road.transform.localScale).x;
        int offset = Random.Range(0, length);
        //Debug.Log("extents " + Vector3.Scale(road.GetComponent<MeshFilter>().mesh.bounds.extents,road.transform.localScale));
        if (road.transform.rotation.y > 0.25f || road.transform.rotation.y < -0.25f)
        {
            v.z += offset;
        }
        else
        {
            v.x += offset;
        }
        CheckIfNear();
        GameObject coin = Instantiate(Coin, road.transform.position + v, Quaternion.identity,CoinsParent.transform);
        coin.name = coin.name + Platforms.name;
        CoinInfo info = coin.AddComponent<CoinInfo>();
        info.Init(road, SelectedRoad, v);
        //Debug.Log(v);
    }

    public bool CheckIfNear()
    {
        return true;
    }

}
