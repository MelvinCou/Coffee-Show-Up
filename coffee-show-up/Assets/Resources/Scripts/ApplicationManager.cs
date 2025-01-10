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

    public Button AudioButton;

    public Button ChangeViewButton;

    public AudioSource AudioSource;

    private Text _presentationText;

    private bool _innerView;

    void Start()
    {
        AppState = ApplicationStates.APP_STARTED;
        InnerParts.SetActive(false);
        InteractiveButtons.SetActive(false);
        Text[] children = StartElementUI.GetComponentsInChildren<Text>();
        _presentationText = children.First(text => text.name == "PresentationText");
        if (_presentationText)
        {
            _presentationText.DOText("Scannez votre machine à café...", 5);
        }

        AudioButton.onClick.AddListener(PlayAudio);
        ChangeViewButton.onClick.AddListener(ChangeView);
    }

    void Update()
    {
        if (!AppState.Equals(ApplicationStates.APP_STARTED))
        {
            StartElementUI.SetActive(false);
        }

        if(!AppState.Equals(ApplicationStates.MODEL_SPAWNED))
        {
            InnerParts.SetActive(false);
            InteractiveButtons.SetActive(false);
        } 
        else
        {
            InteractiveButtons.SetActive(true);
        }
    }

    void PlayAudio()
    {
        AudioSource.Play();
    }

    void ChangeView()
    {
        _innerView = !_innerView;

        GameObject lateralPlane = GameObject.FindGameObjectWithTag("Right-Lateral-Plane");
        GameObject upperPart = GameObject.FindGameObjectWithTag("Upper-Part");
        if (lateralPlane != null && upperPart != null)
        {
            if (_innerView && AppState.Equals(ApplicationStates.MODEL_SPAWNED))
            {
                InnerParts.SetActive(true);
                lateralPlane.transform.DOMoveY(0.05f, 5);
                upperPart.transform.DOMoveY(0.05f, 5);
            }
            else
            {
                //lateralPlane.transform.DOMoveY(-0.05f, 5);
                //upperPart.transform.DOMoveY(-0.05f, 5);
                //DOTween.RewindAll();
                InnerParts.SetActive(false);
            }     
        }
    }
}
