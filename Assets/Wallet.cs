using UnityEngine;

public class Wallet : MonoBehaviour
{
    private const string BeaverGlobal = "BeaverGlobal";
    
    [SerializeField] private ValueDisplay valueDisplay;
    public int income;

    private static int Worth
    {
        get => PlayerPrefs.GetInt(BeaverGlobal);
        set => PlayerPrefs.SetInt(BeaverGlobal, value);
    }
    public void Add(int value)
    {
        income += value;
        DisplayIncome();
    }

    public void Multiply(int factor)
    {
        income *= factor;
        DisplayIncome();
    }
    public void Save()
    {
        Worth += income;
    }

    public void DisplayIncome()
    {
        valueDisplay.ShowValue(income);
    }
    public void DisplayWorth()
    {
        valueDisplay.ShowValue(Worth);
    }
}