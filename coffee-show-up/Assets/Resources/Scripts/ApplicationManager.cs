using Assets.Resources.Scripts;
using DG.Tweening;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ApplicationManager : MonoBehaviour
{
    public GameObject StartElementUI;
    public ApplicationStates AppState { get; set; }

    private Text _presentationText;

    void Start()
    {
        AppState = ApplicationStates.APP_STARTED;
        Text[] children = StartElementUI.GetComponentsInChildren<Text>();
        _presentationText = children.First(text => text.name == "PresentationText");
        if (_presentationText)
        {
            _presentationText.DOText("Scannez le marqueur de votre machine à café...", 5);
        }
    }

    void Update()
    {
        if (!AppState.Equals(ApplicationStates.APP_STARTED))
        {
            StartElementUI.SetActive(false);
        }
    }
}
