using Lean.Touch;
using UnityEngine;

public class SliderInput : MonoBehaviour
{
    private LeanFinger fingerInput;
  
    public bool IsActive
    {
        get;
        private set;
    }

    public float ValueDelta
    {
        get => (fingerInput.ScreenPosition - fingerInput.LastScreenPosition).x;
    }

    public void EnableInput()
    {
        IsActive = true;
    }


    public void DisableInput()
    {
        IsActive = false;
    }
    
    public void SetFingerInput(LeanFinger leanFinger)
    {
        fingerInput = leanFinger;
    }
}
