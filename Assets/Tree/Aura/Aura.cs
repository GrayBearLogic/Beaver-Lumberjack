using System.Collections;
using UnityEngine;

public class Aura : MonoBehaviour
{
    [SerializeField] private GameObject visiblePart;
    [SerializeField] private float growTime;
    
    public void Show()
    {
        visiblePart.SetActive(true);

        StartCoroutine("Scale");
    }

    public void Hide()
    {
        visiblePart.SetActive(false);
    }



    IEnumerator Scale()
    {
        for (float i = 0f; i <= growTime; i += Time.deltaTime)
        {
            visiblePart.transform.localScale = Vector3.one * Mathf.Lerp(0.1f,2f,i/growTime);
            yield return null;
        }
    }
}