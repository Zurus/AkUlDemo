using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinTextManager : MonoBehaviour
{

    public Inverntory inventory;
    public TextMeshProUGUI coinDisplay;

    public void UpdateCoinCount()
    {
        coinDisplay.text = "" + inventory.coins;
    }
}
