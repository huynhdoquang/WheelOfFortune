using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleSpinControllerExample : MonoBehaviour
{
    [SerializeField] private Dropdown dropdownSelectEndAngle;
    [SerializeField] private Toggle toggleClockwise;
    [SerializeField] private Text txtStatus;

    [SerializeField] private SpinWheel spinWheel;

    [SerializeField] private GameObject[] stickGos;

    const string GUIDE_STRING = "pls press from 0 to 9 to select prize";

    private void Start()
    {
        dropdownSelectEndAngle.onValueChanged.RemoveListener(OnDropDownValueChange);
        dropdownSelectEndAngle.onValueChanged.AddListener(OnDropDownValueChange);

        toggleClockwise.onValueChanged.RemoveListener(OnToggleValueChange);
        toggleClockwise.onValueChanged.AddListener(OnToggleValueChange);

        //Reset Value
        dropdownSelectEndAngle.value = 3; //angle end = 0
        toggleClockwise.isOn = true;
        txtStatus.text = GUIDE_STRING;

        spinWheel.OnEndSpinAction = OnEndSpinWheel;
        spinWheel.OnStartSpinAction = OnStartSpinWheel;
    }

    void OnDropDownValueChange(int selectNo)
    {
        spinWheel.SetAngleEnd(selectNo);

        foreach (var go in stickGos)
            go.SetActive(go.transform.GetSiblingIndex() == selectNo ? true : false);

    }

    void OnToggleValueChange(bool isSelect)
    {
        spinWheel.SetClockwise(isSelect);
    }

    void OnStartSpinWheel(int order)
    {
        SetInteractable(false);
        txtStatus.text = "You start spin with order: " + order;
    }
    void OnEndSpinWheel(int order)
    {
        SetInteractable(true);
        txtStatus.text = " Congratulations!! You win with prize order: " + order;
    }

    void SetInteractable(bool isInteractable)
    {
        dropdownSelectEndAngle.interactable = isInteractable;
        toggleClockwise.interactable = isInteractable;
    }

}
