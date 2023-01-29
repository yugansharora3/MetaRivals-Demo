using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Numerics;

public class Balance : MonoBehaviour
{
    public TextMeshProUGUI Textfield;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    async public void check()
    {
        string chain = "ethereum";
        string network = "mainnet";
        string contract = "0x495f947276749Ce646f68AC8c248420045cb7b5e"; // the contract goes here
        string account = "0x74Fdbc80554267009C88C29Ef7e09338a11A8eF4"; // the wallet goes here
        string tokenId = "52916603731213063443444035644694998674632414650110839552858290462179048030258"; // the tokenID goes here

        BigInteger balanceOf = await ERC1155.BalanceOf(chain, network, contract, account, tokenId);
        print(balanceOf);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
