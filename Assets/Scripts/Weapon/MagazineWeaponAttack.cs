using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Weapon
{
    public class MagazineWeaponAttack : WeaponAttack, IWeaponMagazine
    {
        [SerializeField] private WeaponAttack WeaponAttackDecorator;
        [SerializeField] private int MagazineCapacity = 30;
        [SerializeField] private float TimeToReload = 1.0f;

        public int _MagazineCapacity { get { return MagazineCapacity; } }

        // TODO:Mark private
        public int AmmoInMagazine = 0;
        public int AmmoAmount = 0;

        private bool IsReloadingNow = false;

        public UnityEvent<MagazineWeaponAttack> AmmoAmountChanged;
        public UnityEvent MagazineReloaded;
        public UnityEvent AmmoReleased;
        public UnityEvent<int> OnIncreaseAmmo;
        public UnityEvent OnAttack;

        public MagazineWeaponAttack(WeaponAttack subWeaponAttack, int magazineCapacity = 30)
        {
            WeaponAttackDecorator = subWeaponAttack;
            MagazineCapacity = magazineCapacity;
        }

        public override bool Attack(Vector3 position, Vector3 direction)
        {
            if (IsReloadingNow || AmmoInMagazine == 0)
            {
                return false;
            }
            --AmmoInMagazine;

            AmmoAmountChanged?.Invoke(this);
            AmmoReleased?.Invoke();

            WeaponAttackDecorator.Attack(position, direction);
            OnAttack.Invoke();
            return true;
        }

        public int ChargeAmmo(int amount)
        {
            AmmoInMagazine += amount;
            if (AmmoInMagazine > MagazineCapacity)
            {
                var dif = AmmoInMagazine - MagazineCapacity;
                AmmoInMagazine = MagazineCapacity;

                AmmoAmountChanged?.Invoke(this);

                return dif;
            }
            return 0;
        }

        public void Reload()
        {
            if (!IsReloadingNow && AmmoAmount > 0 && AmmoInMagazine < MagazineCapacity)
            {
                StartReload();
                Invoke(nameof(EndReload), TimeToReload);
            }
        }

        private void StartReload()
        {
            IsReloadingNow = true;
            MagazineReloaded?.Invoke();
        }

        private void EndReload()
        {
            AmmoAmount = ChargeAmmo(AmmoAmount);
            IsReloadingNow = false;
        }

        public void IncreaseAmmo(int ammo)
        {
            AmmoAmount += ammo;
            OnIncreaseAmmo.Invoke(ammo);

            AmmoAmountChanged?.Invoke(this);

            if (AmmoAmount < MagazineCapacity)
                Reload();

        }
    }
}
