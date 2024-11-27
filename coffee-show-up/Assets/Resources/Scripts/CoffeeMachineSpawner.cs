using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Assets.Resources.Scripts;
using DG.Tweening;
using UnityEngine.UI;

public class CoffeeMachineSpawner : MonoBehaviour
{
    public GameObject ApplicationManagementGameObject;
    public GameObject CoffeeMachinePrefab;
    public GameObject Referentiel;
    public Button TestSpawn;

    private ApplicationManager _appManagement;

    void Start()
    {
        _appManagement = ApplicationManagementGameObject.GetComponent<ApplicationManager>();
        TestSpawn.onClick.AddListener(MockSpawnState);
    }

    void Update()
    {
        if(_appManagement.AppState.Equals(ApplicationStates.SPAWNING))
        {
            // Spawn object
            GameObject obj = Instantiate(CoffeeMachinePrefab, Referentiel.transform.position + Referentiel.transform.forward * 10f, Referentiel.transform.rotation);
            obj.transform.DOMoveZ(1, 5);
            obj.transform.DOScale(new Vector3(2f, 2f, 2f), 5);

            // change state
            _appManagement.AppState = ApplicationStates.MODEL_SPAWNED;
            Debug.Log("Current State : "+_appManagement.AppState);
        }
    }
    private void MockSpawnState()
    {
        // TODO ML to delete when scan marker functionnal
        _appManagement.AppState = ApplicationStates.SPAWNING;
    }
}
