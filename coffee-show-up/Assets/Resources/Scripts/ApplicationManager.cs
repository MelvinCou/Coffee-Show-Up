using UnityEngine;
using Assets.Resources.Scripts;

public class ApplicationManager : MonoBehaviour
{
    public GameObject StartElementUI;
    public ApplicationStates AppState { get; set; }


    void Start()
    {
        AppState = ApplicationStates.APP_STARTED;
    }

    void Update()
    {
        if (!AppState.Equals(ApplicationStates.APP_STARTED))
        {
            StartElementUI.SetActive(false);
        }
    }
}
