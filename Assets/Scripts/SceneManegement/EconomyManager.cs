using TMPro;
using UnityEngine;

public class EconomyManager : Singlton<EconomyManager>
{
    private int currentMoney = 0;
    private TMP_Text text;

    private const string COINS_STRING = "CoinText";

    public int CurrentMoney { get { return currentMoney; } private set { currentMoney = value; } }

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

    public void GoldPayment(int amount)
    {
        currentMoney -= amount;

        text.text = currentMoney.ToString("D3");
    }
}
