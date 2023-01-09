using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateStat : MonoBehaviour
{
    public TextMeshProUGUI Name, Mobility, AttackStrength, CombatXp, NFTType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Set(string name)
    {
        if (name == "Ape-Button")
            Name.text = "Ape Rival";
        else
            if (name == "Shiba-Button")
                Name.text = "Shiba Rival";
            else
                if (name == "Kishu-Button")
                    Name.text = "Kishu Rival";
                else
                    if (name == "volt-Button")
                        Name.text = "Voly Inu Rival";
                    else
                        Name.text = "White Witch\nRival";  //its white witch
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
