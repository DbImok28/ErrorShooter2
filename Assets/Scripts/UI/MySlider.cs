using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MySlider : MonoBehaviour
{
    private Slider slider;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        //slider.slider.va.AddListener(SetValue);
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(SetValue);
    }

    public void SetValue(float value)
    {
        //Debug.Log(value.ToString());
        text.text = value.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
