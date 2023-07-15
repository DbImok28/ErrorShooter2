using UnityEngine;

public class BotDeath : MonoBehaviour
{
    private void Start()
    {
        GetComponent<HealthComponent>().OnDie.AddListener(Death);
    }

    private void Death(GameObject _gameObject)
    {
        Destroy(gameObject);
    }
}
