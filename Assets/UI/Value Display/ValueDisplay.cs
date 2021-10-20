using TMPro;
using UnityEngine;

public class ValueDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI label;

    public void ShowValue(int value)
    {
        label.text = value.ToString();
    }
}
