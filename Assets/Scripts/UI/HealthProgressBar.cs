using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthProgressBar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Image imageFiller;
    public Text text;

    public void Init(HealthComponent healthComponent)
    {
        healthComponent.OnTakeDamage.AddListener(SetValue);
        healthComponent.OnHeal.AddListener(SetValueHealed);

        SetValue(healthComponent, 0);
    }

    public void SetValue(HealthComponent healthComponent, float damage)
    {
        //text.text = healthComponent.CurrentHealth.ToString();
        float currentHealth = healthComponent.CurrentHealth;
        float defaultHealth = healthComponent.MaxHealth;

        float valueNormalized = (float)currentHealth / defaultHealth;

        imageFiller.fillAmount = valueNormalized;

        text.text = $"{currentHealth} / {defaultHealth} ";
    }

    public void SetValueHealed(HealthComponent healthComponent)
    {
        //text.text = healthComponent.CurrentHealth.ToString();
        float currentHealth = healthComponent.CurrentHealth;
        float defaultHealth = healthComponent.MaxHealth;

        float valueNormalized = (float)currentHealth / defaultHealth;

        imageFiller.fillAmount = valueNormalized;

        text.text = $"{currentHealth} / {defaultHealth} ";
    }

    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
