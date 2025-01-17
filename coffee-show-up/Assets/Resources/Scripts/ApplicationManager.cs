using Assets.Resources.Scripts;
using DG.Tweening;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ApplicationManager : MonoBehaviour
{
    public GameObject StartElementUI;

    public GameObject InteractiveButtons;

    public GameObject InnerParts;

    public ApplicationStates AppState { get; set; }

    public CoffeeMachineModelStates CoffeeMachineState { get; set; }

    public ModeStates Mode { get; set; }

    public Button AudioButton;

    public Button ChangeViewButton;

    public Button ActiveExplodedViewButton;

    public Button ResetButton;

    public AudioSource AudioSource;

    public AudioClip[] AudioClips;

    private Text _presentationText;

    private Sequence _innerPartsRevealSequence;

    private Sequence _explodedViewSequence;

    void Start()
    {
        Reset();

        AudioButton.onClick.AddListener(PlayAudio);
        ChangeViewButton.onClick.AddListener(ChangeViewState);
        ActiveExplodedViewButton.onClick.AddListener(ChangeInnerPartsState);
        ResetButton.onClick.AddListener(Reset);
    }

    void Update()
    {
        if (!AppState.Equals(ApplicationStates.APP_STARTED))
        {
            StartElementUI.SetActive(false);
        }

        if (!AppState.Equals(ApplicationStates.MODEL_SPAWNED))
        {
            InnerParts.SetActive(false);
            InteractiveButtons.SetActive(false);
            Mode = ModeStates.NO_ACTIVE_MODE;
            CoffeeMachineState = CoffeeMachineModelStates.NO_VIEW;
        }
        else
        {
            InteractiveButtons.SetActive(true);
            InnerParts.SetActive(true);
        }
    }

    void PlayAudio()
    {
        AudioSource.Play();
    }

    void Reset() {
        AppState = ApplicationStates.APP_STARTED;
        CoffeeMachineState = CoffeeMachineModelStates.NO_VIEW;
        Mode = ModeStates.NO_ACTIVE_MODE;

        InnerParts.SetActive(false);
        InteractiveButtons.SetActive(false);
        Text[] children = StartElementUI.GetComponentsInChildren<Text>();
        _presentationText = children.First(text => text.name == "PresentationText");
        if (_presentationText)
        {
            _presentationText.DOText("Scannez votre machine à café...", 5);
        }
    }

    void ChangeViewState()
    {
        if (CoffeeMachineState.Equals(CoffeeMachineModelStates.EXTERNAL_VIEW))
        {
            CoffeeMachineState = CoffeeMachineModelStates.INNER_VIEW;
        }
        else
        {
            CoffeeMachineState = CoffeeMachineModelStates.EXTERNAL_VIEW;
        }

        ManageExternalAnimation();
    }

    void ManageExternalAnimation()
    {
        GameObject lateralPlane = GameObject.FindGameObjectWithTag("Right-Lateral-Plane");
        GameObject upperPart = GameObject.FindGameObjectWithTag("Upper-Part");

        if (lateralPlane != null && upperPart != null)
        {
            if (CoffeeMachineState.Equals(CoffeeMachineModelStates.INNER_VIEW))
            {
                InnerParts.SetActive(true);
                _innerPartsRevealSequence = DOTween.Sequence();
                _innerPartsRevealSequence
                    .Append(lateralPlane.transform.DOMoveY(0.1f, 5))
                    .Join(upperPart.transform.DOMoveY(0.1f, 5))
                    .SetAutoKill(false);
            }
            else if (CoffeeMachineState.Equals(CoffeeMachineModelStates.EXTERNAL_VIEW))
            {
                _innerPartsRevealSequence.SmoothRewind();
                InnerParts.SetActive(false);
            }
        }
    }

    void ChangeInnerPartsState()
    {
        if (CoffeeMachineState.Equals(CoffeeMachineModelStates.INNER_VIEW))
        {
            CoffeeMachineState = CoffeeMachineModelStates.EXPLODED_VIEW;
        }
        else if (CoffeeMachineState.Equals(CoffeeMachineModelStates.EXPLODED_VIEW))
        {
            CoffeeMachineState = CoffeeMachineModelStates.INNER_VIEW;
        }
        ManageInnerPartsAnimation();
    }

    void ManageInnerPartsAnimation()
    {
        GameObject nozzle = GameObject.FindGameObjectWithTag("Nozzle");
        GameObject piston = GameObject.FindGameObjectWithTag("Piston");
        GameObject pump = GameObject.FindGameObjectWithTag("Pump");
        GameObject pipes = GameObject.FindGameObjectWithTag("Pipes");
        if (nozzle != null && piston != null && pump != null && pipes != null)
        {
            if (CoffeeMachineState.Equals(CoffeeMachineModelStates.EXPLODED_VIEW))
            {
                _explodedViewSequence = DOTween.Sequence();
                _explodedViewSequence
                    .Append(nozzle.transform.DOMove(new Vector3(-0.01f, 0.05f, 0), 5))
                    .Join(piston.transform.DOMove(new Vector3(-0.01f, 0, 0), 5))
                    .Join(pump.transform.DOMove(new Vector3(-0.01f, 0.05f, 0.02f), 5))
                    .Join(pipes.transform.DOMove(new Vector3(-0.02f, 0.05f, 0), 5))
                    .SetAutoKill(false);
            }
            else if (CoffeeMachineState.Equals(CoffeeMachineModelStates.INNER_VIEW))
            {
                _explodedViewSequence.SmoothRewind();
            }
        }
    }
}
