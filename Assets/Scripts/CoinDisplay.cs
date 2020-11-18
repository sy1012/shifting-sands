using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinDisplay : MonoBehaviour
{
    public Text coinText;
    private Inventory inventory;

    public void Start()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();

        EventManager.onCoinPickedUp += CoinPickedUp;
        CoinPickedUp(null);
    }

    private void CoinPickedUp(System.EventArgs e)
    {
        coinText.text = inventory.GetCoinAmount().ToString();
    }

    // Sets the Coin Amount
    //public void SetCoins(int amount)
    //{
    //    // Displays the Coin Total
    //    coinText.text = amount.ToString();
    //}

}
