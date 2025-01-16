using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.InputSystem.EnhancedTouch;

public class BigElementInteractions : MonoBehaviour
{
    public string[] Texts;

    public GameObject ElementCanvas;

    public Button CloseButton;

    public Text ElementName;

    public Text ElementDescription;

    private string _elementName;

    private Sequence _descriptionSequence;

    
    void Start()
    {
        ElementCanvas.SetActive(false);
        CloseButton.onClick.AddListener(CloseUI);
        EnhancedTouchSupport.Enable();
    }

    void Update()
    {
        if(UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches.Count > 0 )
        {
            foreach (var touch in UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches)
            {
                if (touch.began)
                {
                    Debug.Log($"Touch {touch} started this frame");
                    ElementCanvas.SetActive(true);
                    Ray ray = Camera.main.ScreenPointToRay(touch.screenPosition);
                    if (Physics.Raycast(ray, out RaycastHit hit))
                    {
                        _elementName = hit.transform.name;
                        Debug.Log("Hit object "+ _elementName);
                        _descriptionSequence = DOTween.Sequence();

                        switch (_elementName)
                        {
                            case "nozzle": _descriptionSequence
                                    .Append(ElementName.DOText("BEC", 2))
                                    .Append(ElementDescription.DOText("Cette partie contiendra la description du bec verseur de la machine à café", 1)); 
                                break;
                                
                            case "piston": _descriptionSequence
                                    .Append(ElementName.DOText("PISTON", 2))
                                    .Append(ElementDescription.DOText("Cette partie contiendra la description du piston de la machine à café", 1));
                                break;

                            case "pump": _descriptionSequence
                                    .Append(ElementName.DOText("POMPE", 2))
                                    .Append(ElementDescription.DOText("Cette partie contiendra la description de la pompe de la machine à café", 1));
                                break;
                            case "pipe": _descriptionSequence
                                    .Append(ElementName.DOText("Tuyau 1", 2))
                                    .Append(ElementDescription.DOText("Cette partie contiendra la description du tuyau-1 de la machine à café", 1)); 
                                break;
                            case "pipe-2": _descriptionSequence
                                    .Append(ElementName.DOText("Tuyau 2", 2))
                                    .Append(ElementDescription.DOText("Cette partie contiendra la description du tuyau-2 de la machine à café", 1)); 
                                break;
                            default: break;
                        }
                    }
                }
            }            
        }
    }

    void CloseUI()
    {
        ElementCanvas.SetActive(false);
    }
}
