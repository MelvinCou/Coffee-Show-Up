using Assets.Resources.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class ModeAnimation : MonoBehaviour
{
    public GameObject ApplicationManagementGameObject;

    public GameObject InnerPartsOpaque;

    public GameObject InnerPartsTransparent;

    public GameObject Water;

    public Button PlayAnimationButton;

    private ApplicationManager _appManagement;

    private Animator _waterAnimator;

    void Start()
    {
        _appManagement = ApplicationManagementGameObject.GetComponent<ApplicationManager>();
        _waterAnimator = Water.GetComponent<Animator>();
        _waterAnimator.enabled = false;

        PlayAnimationButton.onClick.AddListener(PlayAnimation);
    }

    void Update()
    {
        if (_appManagement.Mode.Equals(ModeStates.STANDARD_MODE))
        {
            InnerPartsOpaque.SetActive(false);
            InnerPartsTransparent.SetActive(true);
            PlayAnimationButton.gameObject.SetActive(true);
        }
        else
        {
            InnerPartsOpaque.SetActive(true);
            InnerPartsTransparent.SetActive(false);
            PlayAnimationButton.gameObject.SetActive(false);
        }
    }

    void PlayAnimation()
    {
        _waterAnimator.enabled = true;
        _waterAnimator.Play("WaterAnimation");
    }
}
