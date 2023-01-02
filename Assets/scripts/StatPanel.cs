using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatPanel : MonoBehaviour
{
    public TextMeshProUGUI UserNameStatValue, GamesPlayedStatValue, TotalCoinsStatValue , CoinLimitStatValue;
    public ManagerScript Manager;

    public void OnShow()
    {
        UserNameStatValue.text = Manager.data.Username;
        GamesPlayedStatValue.text = Manager.data.GamesPlayed.ToString();
        TotalCoinsStatValue.text = Manager.data.TotalCoinsCollected.ToString();
        CoinLimitStatValue.text = "20";
    }
}
