using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginWallet : MonoBehaviour
{
    public Toggle rememberMe;
    public Animator anim;
    public TextMeshProUGUI MetaMaskButtonLabel;
    public TextMeshProUGUI status;
    public GameObject PopUpWindow;

    void Start()
    {
        //PlayerPrefs.SetString("Account", "");
        // if remember me is checked, set the account to the saved account
        if (PlayerPrefs.HasKey("RememberMe") && PlayerPrefs.HasKey("Account"))
        {
            if (PlayerPrefs.GetInt("RememberMe") == 1 && PlayerPrefs.GetString("Account") != "")
            {
                MetaMaskButtonLabel.text = "Connected";
            }
        }
    }

    public void popup()
    {
        PopUpWindow.SetActive(true);
        anim.SetBool("Pop", true);
    }
    public void ClosePopup()
    {
        anim.SetBool("Pop", false);
        PopUpWindow.SetActive(false);
    }

    async public void CheckOwnedNFT()
    {
        string chain = "ethereum";
        string network = "mainnet";
        string contract = "0x495f947276749Ce646f68AC8c248420045cb7b5e"; // the contract goes here
        string account = PlayerPrefs.GetString("Account"); // the wallet goes here
        string tokenId = "52916603731213063443444035644694998674632414650110839552858290462179048030258"; // the tokenID goes here

        System.Numerics.BigInteger balanceOf = await ERC1155.BalanceOf(chain, network, contract, account, tokenId);

        if(balanceOf > 1)
        {
            status.text = "You own " + balanceOf.ToString() + " NFTs \n Public Address of your account : " + PlayerPrefs.GetString("Account");
        }
    }

    async public void OnLogin()
    {
        // get current timestamp
        int timestamp = (int)(System.DateTime.UtcNow.Subtract(new System.DateTime(1970, 1, 1))).TotalSeconds;
        // set expiration time
        int expirationTime = timestamp + 60;
        // set message
        string message = expirationTime.ToString();
        // sign message
        string signature = await Web3Wallet.Sign(message);
        // verify account
        string account = await EVM.Verify(message, signature);
        int now = (int)(System.DateTime.UtcNow.Subtract(new System.DateTime(1970, 1, 1))).TotalSeconds;
        // validate
        if (account.Length == 42 && expirationTime >= now)
        {
            // save account
            PlayerPrefs.SetString("Account", account);
            if (rememberMe.isOn)
                PlayerPrefs.SetInt("RememberMe", 1);
            else
                PlayerPrefs.SetInt("RememberMe", 0);
            print("Account: " + account);
            CheckOwnedNFT();
            ClosePopup();
            MetaMaskButtonLabel.text = "Connected";
        }
    }
}
