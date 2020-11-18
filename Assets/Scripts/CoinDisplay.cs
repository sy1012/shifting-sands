using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinDisplay : MonoBehaviour
{
    public Text coinText;
        
    // Sets the Coin Amount
    public void SetCoins(int amount)
    {
        // Displays the Coin Total
        coinText.text = amount.ToString();
        
    }

}
