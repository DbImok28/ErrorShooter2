using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingDialog : MonoBehaviour
{
    public SettingsManager settingsManager;

    public Slider LateralSensitivitySlider;
    public Slider VerticalSensitivitySlider;
    public Slider MaxPitchAngleSlider;
    public Slider MinPitchAngleSlider;

    private Animator animator;

    public void SetLateralSensitivity(float value)
    {
        Debug.Log($"settingsManager.lateralSensitivity = {value}");
        settingsManager.lateralSensitivity = value;

    }

    public void SetVerticalSensitivity(float value)
    {
        settingsManager.verticalSensitivity = value;
    }

    public void SetMaxPitchAngle(float value)
    {
        settingsManager.maxPitchAngle = value;
    }

    public void SetMinPitchAngle(float value)
    {
        settingsManager.minPitchAngle = value;
    }

    
    private void OnEnable()
    {
        LateralSensitivitySlider.onValueChanged.AddListener(SetLateralSensitivity);
        VerticalSensitivitySlider.onValueChanged.AddListener(SetVerticalSensitivity);
        MaxPitchAngleSlider.onValueChanged.AddListener(SetMaxPitchAngle);
        MinPitchAngleSlider.onValueChanged.AddListener(SetMinPitchAngle);

        animator = GetComponent<Animator>();

        animator.SetBool("show", true);


    }

    private void OnDisable()
    {
        animator.SetBool("show", false);
    }

}
