using GoogleMobileAds.Api;
using Lean.Touch;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Master : MonoBehaviour
{
    [Header("User Interface")] 
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject endMenu;
    [Space]
    public VariableJoystick variableJoystick;
    public ProgressBar progressBar;
    public Tutor tutor;
    [Header("Components")]
    public Beaver beaver;
    public new CameraController camera;
    [SerializeField] private LeanTouch leanTouch;
    [SerializeField] private Transform endMenuPoint;
    [SerializeField] public  Wallet wallet;
    [SerializeField] private GameObject confetti;

    private GoogleRewardAd doubleIncomeAd;

    private void Start()
    {
        wallet.DisplayWorth();
        doubleIncomeAd = new GoogleRewardAd();
        doubleIncomeAd.RewardedAd.OnUserEarnedReward += Advertisement_Finish;
    }

    #region Game State Transitions

    public void MainMenu_Walking()
    {
        camera.ChangeOrigin(beaver.cameraPoint, 1f);
        variableJoystick.gameObject.SetActive(true);
        startMenu.SetActive(false);
        wallet.DisplayIncome();
    }

    public void Walking_Eating(Tree tree)
    {
        var position = beaver.transform.position;
        tree.rig.LookAt(new Vector3(position.x, 0, position.z));

        tutor.Slide();
        progressBar.Show();
        variableJoystick.gameObject.SetActive(false);
        leanTouch.gameObject.SetActive(true);
        camera.ChangeOrigin(tree.cameraPoint, 1f);
        beaver.WalkToEat(tree);
    }
    public void Eating_Walking(Tree tree)
    {
        Destroy(tree.gameObject);

        var treeTransform = tree.transform;
        var deadTree = Instantiate(tree.deadTree, treeTransform.position, treeTransform.rotation);
        deadTree.transform.LookAt(beaver.transform.position);

        tutor.Hide();
        progressBar.Hide();
        variableJoystick.gameObject.SetActive(true);
        leanTouch.gameObject.SetActive(false);
        camera.ChangeOrigin(beaver.cameraPoint, 1f);
        beaver.EatToWalk(deadTree.log);
    }

    public void Walking_Building(Dam dam)
    {
        beaver.GetComponent<CharacterController>().enabled = false;

        tutor.Tap();
        progressBar.Show();
        variableJoystick.OnPointerUp(new PointerEventData(EventSystem.current));
        variableJoystick.gameObject.SetActive(false);
        leanTouch.gameObject.SetActive(true);
        camera.ChangeOrigin(dam.cameraPoint, 1f);
        beaver.WalkToBuild();
    }
    public void Building_Walking()
    {
        beaver.GetComponent<CharacterController>().enabled = true;

        tutor.Hide();
        progressBar.Hide();
        variableJoystick.gameObject.SetActive(true);
        leanTouch.gameObject.SetActive(false);
        camera.ChangeOrigin(beaver.cameraPoint, 1f);
        beaver.BuildToWalk();
    }
    public void Building_EndMenu()
    {
        endMenu.SetActive(true);
        variableJoystick.gameObject.SetActive(false);

        camera.ChangeOrigin(endMenuPoint, 1f);
        confetti.SetActive(true);
    }

    public void EndMenu_Advertisement()
    {
        doubleIncomeAd.UserChoseToWatchAd();
    }

    private void Advertisement_Finish(object sender, Reward reward)
    {
        print("SuccessRewardAd");
        wallet.Multiply(2);
        wallet.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void EndMenu_Finish()
    {
        wallet.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion
}