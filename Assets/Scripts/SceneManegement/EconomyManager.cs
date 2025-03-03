using TMPro;
using UnityEngine;

public class EconomyManager : Singlton<EconomyManager>
{
    private int currentMoney = 0;
    private TMP_Text text;

    private const string COINS_STRING = "CoinText";

    private void Start()
    {
        if (text == null)
        {
            text = GameObject.Find(COINS_STRING).GetComponent<TMP_Text>();
        }
    }

    public void UpdateCoinsNumber()
    {
        currentMoney += 1;

        text.text = currentMoney.ToString("D3");
    }
}
