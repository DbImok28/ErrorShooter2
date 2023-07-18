using ECM.Controllers;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : BaseFirstPersonController, ISaveable, IPauseHandler
{
    [Header("Inventory")]
    [SerializeField] public PlayerInventory Inventory;
    [Header("Weapon")]
    [SerializeField] public GameObject WeaponSocket;

    public bool PlayerIsMoving;
    public bool PlayerIsRunning;

    public UnityEvent<Weapon> WeaponChanged;

    private GameObject ActiveWeaponGameObject;
    private Weapon ActiveWeapon;
    public int ActiveWeaponIndex = -1;
    private bool WeaponInHand = false;

    private PlayerEnvironmentInteraction environmentInteraction;


    private PauseManager pauseManager;
    private bool isPaused;

    public void StartSwapWeapon()
    {
        WeaponInHand = false;
    }
    public bool AssignWeaponToSocket(GameObject weaponGameObject)
    {
        var newWeapon = weaponGameObject.GetComponentInChildren<Weapon>();
        if (newWeapon != null)
        {
            ActiveWeaponGameObject = weaponGameObject;
            ActiveWeapon = newWeapon;

            //Debug.Log("active weapon " + ActiveWeapon);
            //WeaponChanged?.Invoke(ActiveWeapon);

            return true;
        }
        return false;
    }



    public bool AssignWeaponToSocket(int index)
    {
        if (index >= 0 && index < Inventory.Weapons.Count)
        {
            ActiveWeaponIndex = index;
            var weaponGameObject = Inventory.Weapons[index];
            weaponGameObject.SetActive(true);

            if (!AssignWeaponToSocket(weaponGameObject))
            {
                weaponGameObject.SetActive(false);
                return false;
            }

            //WeaponChanged?.Invoke(ActiveWeapon);

            return true;
        }
        return false;
    }
    public void DestroyWeaponFromSocket()
    {
        if (ActiveWeaponGameObject != null)
        {
            ActiveWeaponGameObject.SetActive(false);
            ActiveWeaponGameObject = null;
            ActiveWeapon = null;
        }
    }
    public void EndSwapWeapon()
    {
        if (ActiveWeapon != null)
        {
            WeaponInHand = true;
        }
    }

    public void SwapWeapon(int index)
    {
        if (index >= 0 && index < Inventory.Weapons.Count)
        {
            StartSwapWeapon();
            // Hide weapon anim
            DestroyWeaponFromSocket();

            if (AssignWeaponToSocket(index))
            {
                // Show weapon Anim


                EndSwapWeapon();

                if (ActiveWeapon != null)
                {
                    WeaponChanged?.Invoke(ActiveWeapon);
                }


            }
        }
    }
    public void SwapToNextWeapon()
    {
        SwapWeapon((ActiveWeaponIndex + 1) % Inventory.Weapons.Count);

    }

    protected override void HandleInput()
    {

        if (isPaused)
            return;

        if (GetComponent<HealthComponent>().IsDead)
            return;

        moveDirection = new Vector3
        {
            x = Input.GetAxisRaw("Horizontal"),
            y = 0.0f,
            z = Input.GetAxisRaw("Vertical")
        };

        if(moveDirection.x!=0 || moveDirection.z != 0)
        {
            PlayerIsMoving = true;
        }
        else
        {
            PlayerIsMoving = false;
        }

        run = Input.GetButton("Fire3");

        if (run)
        {
            PlayerIsMoving = false;
            PlayerIsRunning = true;
        }
        else
        {
            PlayerIsRunning = false;
        }

        jump = Input.GetButton("Jump");

        crouch = Input.GetKey(KeyCode.C);

        if (WeaponInHand)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                ActiveWeapon.Press();
            }
            if (Input.GetButtonUp("Fire1"))
            {
                ActiveWeapon.Release();
            }
            if (Input.GetKey(KeyCode.R))
            {
                ActiveWeapon.Magazine.Reload();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                SwapToNextWeapon();


            }
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            environmentInteraction.PickUpKey();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            environmentInteraction.AttachKey();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            environmentInteraction.SaveCheckpoint();
        }
        environmentInteraction.Interact();

    }


    public void Start()
    {
        if (Inventory == null)
        {
            Inventory = GetComponent<PlayerInventory>();
        }
        SwapWeapon(0);

        environmentInteraction = GetComponent<PlayerEnvironmentInteraction>();
        environmentInteraction.Init(Inventory);

    }

    public void SaveData(ref GameData gameData)
    {
        gameData.playerX = transform.position.x;
        gameData.playerY = transform.position.y;
        gameData.playerZ = transform.position.z;
    }

    public void LoadData(GameData gameData)
    {
        transform.position = new Vector3(gameData.playerX, gameData.playerY, gameData.playerZ);
    }

    public void SetPaused(bool isPaused)
    {
        this.isPaused = isPaused;

        //Debug.Log($"PLAYER SET PAUSED {isPaused}");
    }
}
