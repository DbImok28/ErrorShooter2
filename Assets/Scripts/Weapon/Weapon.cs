using Assets.Scripts.Weapon;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour, ITriggerPull
{
    public string WeaponName;
    public GameObject ShootSource;
    public TriggerPull[] TriggerPulls;
    public WeaponAimedAttack Aim;
    public MagazineWeaponAttack Magazine;

    public Sprite icon_active;
    public Sprite icon_unactive;


    private int CurrentTriggerPull = 0;

    public void SwitchTrigger(int index)
    {
        if (index < TriggerPulls.Length)
        {
            TriggerPulls[CurrentTriggerPull].Release();
            CurrentTriggerPull = index;
        }
    }

    public void Press()
    {
        TriggerPulls[CurrentTriggerPull].Press();
    }

    public void Release()
    {
        TriggerPulls[CurrentTriggerPull].Release();
    }

    private void Update()
    {
        Aim.SetShootPositionAndDirection(ShootSource.transform.position, ShootSource.transform.forward);
    }

    public override string ToString()
    {
        return WeaponName;
    }
}
