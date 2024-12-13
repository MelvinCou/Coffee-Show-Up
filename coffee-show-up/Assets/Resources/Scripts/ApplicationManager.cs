using Assets.Resources.Scripts;
using DG.Tweening;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ApplicationManager : MonoBehaviour
{
    public GameObject StartElementUI;
    public ApplicationStates AppState { get; set; }

    public Button AudioButton;

    public AudioSource AudioSource;

    private Text _presentationText;

    void Start()
    {
        AppState = ApplicationStates.APP_STARTED;
        Text[] children = StartElementUI.GetComponentsInChildren<Text>();
        _presentationText = children.First(text => text.name == "PresentationText");
        if (_presentationText)
        {
            _presentationText.DOText("Scannez votre machine à café...", 5);
        }

        AudioButton.onClick.AddListener(PlayAudio);
    }

    void Update()
    {
        if (!AppState.Equals(ApplicationStates.APP_STARTED))
        {
            StartElementUI.SetActive(false);
        }
    }

    void PlayAudio()
    {
        AudioSource.Play();
    }
}
