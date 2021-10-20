using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private new Transform transform;

    public void ChangeOrigin(Transform newParent, float time)
    {
        transform.parent = newParent;
        StartCoroutine(MoveToParent(time));
    }

    private IEnumerator MoveToParent(float time)
    {
        var startRotation = transform.localRotation;
        var startPosition = transform.localPosition;
        var value = 0f;
        do
        {
            value += Time.deltaTime;
            var fraction = value / time;

            transform.localPosition = Vector3.Lerp(startPosition, Vector3.zero, fraction);
            transform.localRotation = Quaternion.Lerp(startRotation, Quaternion.identity, fraction);

            yield return null;
        } while (value < time);
        
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        yield return null;
    }
    
}