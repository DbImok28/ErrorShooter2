using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour, ISaveable
{
    [Header("Parameters")]
    public float CurrentHealth = 10.0f;
    public float MaxHealth = 10.0f;
    public bool IsDead { get; private set; } = false;

    [Header("Events")]
    public UnityEvent<HealthComponent, float> OnTakeDamage;
    public UnityEvent OnDie;
    public UnityEvent OnRespawn;
    public UnityEvent<HealthComponent> OnGameStart;

    public void TakeDamage(float damage)
    {
        if (IsDead) return;

        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0.0f, MaxHealth);
        OnTakeDamage.Invoke(this, damage);
        if (CurrentHealth <= 0.0f)
        {
            Die();
        }
    }

    public void Heal(float recovery)
    {
        if (IsDead) return;
        CurrentHealth = Mathf.Clamp(CurrentHealth + recovery, 0.0f, MaxHealth);
    }

    public void Die()
    {
        IsDead = true;
        // TODO: Remove
        var comp = gameObject.GetComponent<Rigidbody>();
        if (comp != null) comp.isKinematic = true;

        OnDie.Invoke();

        if (GetComponent<PlayerController>())
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void Respawn()
    {
        IsDead = false;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;

        Debug.Log("iskinematic"+gameObject.GetComponent<Rigidbody>().isKinematic);

        SetDefault();

        OnRespawn.Invoke();

        if (GetComponent<PlayerController>())
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void SetDefault()
    {
        CurrentHealth = 10.0f;
    }

    public void Start()
    {
        OnGameStart?.Invoke(this);
    }

    public void SaveData(ref GameData gameData)
    {
        if (GetComponent<PlayerController>())
        {
            gameData.playerHealth = CurrentHealth;
        }
        
    }

    public void LoadData(GameData gameData)
    {
        if (GetComponent<PlayerController>())
        {
            CurrentHealth = gameData.playerHealth;
        }
        
    }

}
