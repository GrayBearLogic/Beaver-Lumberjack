using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image mask;
    [SerializeField] private TextMeshProUGUI label;

    private float fullness;
    public float Fullness
    {
        get => fullness;
        set
        {
            fullness = Mathf.Min(value, 1);

            mask.fillAmount = fullness;
            label.text = (int)(fullness * 100) + "%";
        }
    }
    
    public void Show()
    {
        gameObject.SetActive(true);
        mask.fillAmount = 0;
        label.text = "0%";
        Fullness = 0;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
