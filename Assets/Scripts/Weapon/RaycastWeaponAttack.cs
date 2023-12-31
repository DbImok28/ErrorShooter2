﻿using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Weapon
{
    public class RaycastWeaponAttack : WeaponAttack
    {
        [SerializeField] private float Damage = 1;
        [SerializeField] private float MaxShootDistance = 4000;

        [SerializeField] private GameObject DebugShootSource;
        private GameObject DebugShootPointSphere;
        [SerializeField] private bool ShowDebugSphere = true;

        [SerializeField] private GameObject[] ProjectileShootSources;
        [SerializeField] private GameObject ProjectileGameObject;
        [SerializeField] private bool ShowProjectileSphere = true;

        private void Start()
        {
            // Debug code
            if (ShowDebugSphere && DebugShootSource != null)
            {
                DebugShootPointSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                Destroy(DebugShootPointSphere.GetComponent<Collider>());
                DebugShootPointSphere.GetComponent<MeshRenderer>().material.color = Color.green;
                DebugShootPointSphere.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            }
        }

        private void Update()
        {
            if (DebugShootSource != null && DebugShootPointSphere)
            {
                if (Physics.Raycast(DebugShootSource.transform.position, DebugShootSource.transform.forward, out RaycastHit hit, MaxShootDistance))
                {
                    DebugShootPointSphere.transform.localPosition = hit.point;
                }
            }
        }

        public override bool Attack(Vector3 position, Vector3 direction)
        {
            //print(position);
            if (Physics.Raycast(position, direction, out RaycastHit hit, MaxShootDistance))
            {
                var health = hit.collider.gameObject.GetComponent<HealthComponent>();
                if (health)
                {
                    health.TakeDamage(Damage);
                }

                // Debug code
                if (ShowDebugSphere)
                {
                    GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    Destroy(sphere.GetComponent<Collider>());
                    sphere.transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
                    sphere.transform.localPosition = hit.point;
                    sphere.transform.localRotation = Quaternion.identity;
                    sphere.GetComponent<Renderer>().material.color = Color.grey;
                    Destroy(sphere, 1);
                }

                if (ShowProjectileSphere)
                {
                    foreach (var source in ProjectileShootSources)
                    {
                        var projectile = Instantiate(ProjectileGameObject/*, source.transform*/);
                        projectile.transform.SetPositionAndRotation(source.transform.position, Quaternion.LookRotation(direction, Vector3.up));
                        //projectile.transform.SetPositionAndRotation(source.transform.position, source.transform.rotation);
                    }
                }
                return true;
            }
            return false;
        }
    }
}
