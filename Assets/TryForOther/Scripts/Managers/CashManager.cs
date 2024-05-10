using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashManager : MonoBehaviour
{
    public static CashManager instance;
    private int coins;
    private string keyCoins = "keyCoins";
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    public void ExchangeProduct(ProductData productData)
    {
        AddCoin(productData.productPrice);
    }

    public void AddCoin(int price)
    {
        coins += price;
        //Debug.Log("Cash " + coins);
        DisplayCoins(); 
    }
    private void SpendCoin(int price)
    {
        coins -= price;
        DisplayCoins();
    }

    public bool TryBuyThisUnit(int price) 
    {
        if (GetCoins() > price)
        {
            SpendCoin(price);
            return true;
            //paraný harca
        }
        return false;
    }

    private void Start()
    {
        LoadCash();
        DisplayCoins();
    }

    public int GetCoins()
    {
        return coins;
    }

    private void DisplayCoins()
    {
        UIManager.instance.ShowCoinCountOnScreen(coins);
        SaveCash();
    }

    private void LoadCash()
    {
        coins = PlayerPrefs.GetInt(keyCoins, 0);
    }

    private void SaveCash()
    {
        PlayerPrefs.SetInt(keyCoins, coins);
    }
}
