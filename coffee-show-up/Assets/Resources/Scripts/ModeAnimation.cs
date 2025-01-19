using Assets.Resources.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class ModeAnimation : MonoBehaviour
{
    public GameObject ApplicationManagementGameObject;

    public GameObject InnerPartsOpaque;

    public GameObject InnerPartsTransparent;

    public GameObject Coffee;

    public Button PlayAnimationButton;

    public Button StopAnimationButton;

    private ApplicationManager _appManagement;

    private Animator _coffeeAnimator;

    private AnimationStates _animationState;

    void Start()
    {
        _appManagement = ApplicationManagementGameObject.GetComponent<ApplicationManager>();
        _coffeeAnimator = Coffee.GetComponent<Animator>();
        _coffeeAnimator.enabled = false;
        _animationState = AnimationStates.NOT_STARTED;

        PlayAnimationButton.onClick.AddListener(PlayAnimation);
        StopAnimationButton.onClick.AddListener(StopAnimation);
    }

    void Update()
    {
        if (_appManagement.Mode.Equals(ModeStates.STANDARD_MODE))
        {
            InnerPartsOpaque.SetActive(false);
            InnerPartsTransparent.SetActive(true);
            PlayAnimationButton.gameObject.SetActive(true);
            StopAnimationButton.gameObject.SetActive(true);
            Coffee.SetActive(true);
        }
        else
        {
            InnerPartsOpaque.SetActive(true);
            InnerPartsTransparent.SetActive(false);
            PlayAnimationButton.gameObject.SetActive(false);
            StopAnimationButton.gameObject.SetActive(false);
            Coffee.SetActive(false);
        }
    }

    void PlayAnimation()
    {
        switch (_animationState)
        {
            case AnimationStates.NOT_STARTED:
                {
                    Coffee.SetActive(true);
                    _coffeeAnimator.enabled = true;
                    _animationState = AnimationStates.PLAYING;
                    _coffeeAnimator.Play("StartMachine");
                    break;
                }
            case AnimationStates.PLAYING:
                {
                    _coffeeAnimator.speed = 0;
                    _animationState = AnimationStates.PAUSED;
                    break;
                }
                
            case AnimationStates.PAUSED:
                {
                    _coffeeAnimator.speed = 1;
                    _animationState = AnimationStates.PLAYING;
                    break;
                }
            default: break;
        }
    }

    void StopAnimation()
    {
        _coffeeAnimator.Rebind();
        _coffeeAnimator.Update(0);
        _animationState = AnimationStates.NOT_STARTED;
        _coffeeAnimator.enabled = false;
    }
}
