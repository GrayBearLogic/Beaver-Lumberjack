using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(SliderInput))]
[RequireComponent(typeof(Animator))]
public class Beaver : MonoBehaviour
{
    private enum BeaverState { Walking, Eating, Building, Cinematic }

    [SerializeField] private new Transform transform;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private VariableJoystick variableJoystick;
    [SerializeField] private Animator animator;
    [SerializeField] private SliderInput sliderInput;
    [SerializeField] private ParticleSystem sawdust;
    [SerializeField] private GameObject hammer;
    [SerializeField] private GameObject guide;
    [Space]
    [SerializeField] public Transform cameraPoint;
    [SerializeField] public Transform logPoint;
    [SerializeField] public Dam dam;
    [Space]
    [SerializeField] private float sideMoveSpeed = 0.1f;
    [SerializeField] private float eatingFactor = 2e-5f;
    
    private BeaverState state = BeaverState.Walking;
    private Vector3 displacement;
    private Tree targetTree;
    private Transform carriedLog;
    
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Eat = Animator.StringToHash("Eat");
    private static readonly int Carry = Animator.StringToHash("Carry");
    private static readonly int Build = Animator.StringToHash("Build");

    public bool IsCarryLog
    {
        get => carriedLog != null;
    }

    public void FixedUpdate()
    {
        switch (state)
        {
            case BeaverState.Walking:
                var direction = new Vector3(variableJoystick.Horizontal, 0, variableJoystick.Vertical);
                Walk(direction);
                break;
            case BeaverState.Eating:
                if (sliderInput.IsActive)
                    EatAndMove(sliderInput.ValueDelta);
                break;
            case BeaverState.Building:
                break;
        }
    }
    

    #region State Transitions

    public void WalkToEat(Tree tree)
    {
        StartCoroutine(WalkToEatCoroutine(tree));
    }
    private IEnumerator WalkToEatCoroutine(Tree tree)
    {
        targetTree = tree;
        state = BeaverState.Cinematic;
        transform.parent = tree.beaverPoint;

        yield return null;
        
        Vector3 direction;
        do
        {
            direction = transform.parent.position - transform.position;
            Walk(direction.normalized);
            yield return null;
        } while (direction.magnitude > 0.2f);

        displacement = Vector3.zero;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        state = BeaverState.Eating;
        sawdust.gameObject.SetActive(true);
        
        animator.SetFloat(Speed, 0f);
        animator.SetBool(Eat, true);

        yield return null;
    }

    public void EatToWalk(Transform log)
    {
        transform.parent = null;
        StartCoroutine(EatToWalkCoroutine(log));
    }
    private IEnumerator EatToWalkCoroutine(Transform log)
    {
        state = BeaverState.Cinematic;
        sawdust.gameObject.SetActive(false);

        transform.Rotate(new Vector3(0,90,0), Space.Self);
        transform.Translate(Vector3.right*0.5f);
        animator.SetBool(Carry, true);
        animator.SetBool(Eat, false);
        yield return new WaitForSeconds(1);
        guide.SetActive(true);

        state = BeaverState.Walking;
        
        carriedLog = log;
        carriedLog.SetParent(logPoint);
        carriedLog.localPosition = Vector3.zero;
        carriedLog.localRotation = Quaternion.identity;
        yield return null;
    }
    
    public void WalkToBuild()
    {
        state = BeaverState.Building;

        hammer.SetActive(true);
        guide.SetActive(false);

        animator.SetFloat(Speed, 0f);
        animator.SetBool(Build, true);
        animator.SetBool(Carry, false);
        Destroy(carriedLog.gameObject);
    }

    public void BuildToWalk()
    {
        state = BeaverState.Walking;

        hammer.SetActive(false);
        animator.SetBool(Build, false);

    }

    #endregion

    #region Utility Methods
    private void OnAnimatorMove()
    {
        displacement += animator.deltaPosition;
    }

    private void Walk(Vector3 direction)
    {
        transform.LookAt(direction + transform.position, Vector3.up);
        animator.SetFloat(Speed, direction.magnitude);

        characterController.Move(displacement);
        displacement = Vector3.zero;
    }
    private void EatAndMove(float value)
    {
        targetTree.rig.Rotate(Vector3.up, value * sideMoveSpeed);
        var progress = Mathf.Abs(value) * eatingFactor;
        targetTree.BeEaten(progress);
    }

    public void MoveToPoint(Transform point)
    {
        transform.position = point.position;
        transform.rotation = point.rotation;
    }


    #endregion
    
    public void BuildTap()
    {
        if (state == BeaverState.Building)
            dam.BeBuilt();
    }
}