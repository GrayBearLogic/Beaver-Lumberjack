using System;
using System.Collections;
using UnityEngine;

public class Tutor : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private static readonly int SlideId = Animator.StringToHash("Slide");
    private static readonly int TapId = Animator.StringToHash("Tap");
    private static readonly int DisableId = Animator.StringToHash("Disable");

    [SerializeField] private float delay = 3f;
    private float timer;

    private event Action animAction;

    private void Awake()
    {
        timer = delay;
    }

    public void Slide()
    {
        gameObject.SetActive(true);

        animAction = null;
        animAction += () => animator.SetTrigger(SlideId);
    }
    public void Tap()
    {
        gameObject.SetActive(true);

        animAction = null;
        animAction += () => animator.SetTrigger(TapId);
    }
    public void Hide()
    {
        animator.SetTrigger(DisableId);
        gameObject.SetActive(false);
    }

    public void Clicked()
    {
        timer = delay;
        animator.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (animator.gameObject.activeSelf == false)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                animator.gameObject.SetActive(true);
                animAction.Invoke();
            }
        }
    }
}
