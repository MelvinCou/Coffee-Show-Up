using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.InputSystem.EnhancedTouch;
using System.Linq;
using Assets.Resources.Scripts;

public class BigElementInteractions : MonoBehaviour
{
    public GameObject ApplicationManagementGameObject;

    public GameObject ElementCanvas;

    public Button CloseButton;

    public Text ElementName;

    public Text ElementDescription;

    public TextAsset JsonFile;

    private string _elementName;

    private Sequence _descriptionSequence;

    private MachineElement[] _machineElements;

    private ApplicationManager _appManagement;


    void Start()
    {
        ElementCanvas.SetActive(false);
        CloseButton.onClick.AddListener(CloseUI);
        EnhancedTouchSupport.Enable();
        _appManagement = ApplicationManagementGameObject.GetComponent<ApplicationManager>();

        if (JsonFile != null)
        {
            Debug.Log("Json file present");
            _machineElements = JsonHelper.FromJson<MachineElement>(JsonFile.text);
        }
    }

    void Update()
    {
        ManageTouchScreen();
    }


    void ManageTouchScreen()
    {
        if (UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches.Count > 0 && _appManagement.CoffeeMachineState.Equals(CoffeeMachineModelStates.EXPLODED_VIEW))
        {
            foreach (var touch in UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches)
            {
                if (touch.began)
                {
                    Debug.Log($"Touch {touch} started this frame");
                    Ray ray = Camera.main.ScreenPointToRay(touch.screenPosition);
                    if (Physics.Raycast(ray, out RaycastHit hit))
                    {
                        ElementCanvas.SetActive(true);
                        _elementName = hit.transform.name;
                        Debug.Log("Hit object " + _elementName);
                        _descriptionSequence = DOTween.Sequence();

                        MachineElement currentElement = _machineElements.First(elem => elem.Name.ToLower().Equals(_elementName));
                        if (_machineElements != null && _elementName != null && currentElement != null)
                        {
                            _descriptionSequence
                                    .Append(ElementName.DOText(currentElement.FullName, 2))
                                    .Append(ElementDescription.DOText(currentElement.Description, 1));

                            _appManagement.AudioSource.clip = _appManagement.AudioClips.First(clip => clip.name == currentElement.Name);
                            _appManagement.AudioSource.Play();
                        }
                    }
                }
            }
        }
    }

    void CloseUI()
    {
        ElementCanvas.SetActive(false);
        ElementName.text = "";
        ElementDescription.text = "";
    }
}
