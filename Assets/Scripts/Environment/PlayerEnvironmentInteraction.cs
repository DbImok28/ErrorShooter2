using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Linq;
using System;

public class PlayerEnvironmentInteraction : MonoBehaviour, ICanOpenDoor
{

    public float СanReadNoteRadius;
    public float CanPickUpItemRadius = 2;
    public float CanOpenDoorRadius = 2;
    public float CanInteractRadius = 4;

    private Note nearestNote;

    public GameObject currentInteractable;

    public UnityEvent<Interactable> InteractableChanged;



    private PlayerInventory inventory;

    public Camera FPSCamera;

    public int Goid { get { return gameObject.GetInstanceID(); } }


    public void Init(PlayerInventory playerInventory)
    {
        this.inventory = playerInventory;

        if (FPSCamera == null)
        {
            FPSCamera = GetComponentInChildren<Camera>();
        }



        //InteractableAssigned += InvokeMsg;
    }

    public PlayerInventory GetInventory()
    {
        return inventory;
    }

    public void PickUpKey()
    {
        //Debug.Log("PickUpKey");

        GameObject key;

        if (KeyIsNear(out key) && !key.GetComponent<KeyForDoor>().isPickedUp)
        {
            inventory.KeysNames.Add(key.name);

            inventory.KeyPickedUp?.Invoke(inventory.KeysNames);

            //Анимация подбора ключа

            key.GetComponent<KeyForDoor>().isPickedUp = true;
            key.GetComponent<KeyForDoor>().PickUp(gameObject);
        }
    }

    public bool KeyIsNear(out GameObject key)
    {
        Debug.Log("KeyIsNear");

        var colliders = Physics.OverlapSphere(gameObject.transform.position, CanPickUpItemRadius);

        foreach (var collider in colliders)
        {
            if (collider.gameObject.GetComponent<KeyForDoor>())
            {
                key = collider.gameObject;
                return true;
            }
        }

        key = null;
        return false;
    }

    public void AttachKey()
    {

        //Debug.Log("Attach key");

        GameObject keyDoor;

        if (KeyDoorIsNear(out keyDoor))
        {
            string matchingKey;

            if (TryMatchKey(keyDoor, out matchingKey))
            {

                //Анимация прикладывания ключа

                //Анимация открытия двери

            }
        }
    }

    //Работает только тогда, когда обьект со скриптом Cardreader вложен в обьект со скриптом KeyDoor
    public bool KeyDoorIsNear(out GameObject keyDoor)
    {
        var colliders = Physics.OverlapSphere(gameObject.transform.position, CanPickUpItemRadius);
        foreach (var collider in colliders)
        {
            if (collider.gameObject.GetComponentInParent<KeyDoor>())
            {
                keyDoor = collider.gameObject.transform.parent.gameObject;
                return true;
            }
        }

        keyDoor = null;
        return false;

    }

    public bool TryMatchKey(GameObject keyDoor, out string matchingKey)
    {
        //Debug.Log("TryMatchKey");

        KeyDoor _keyDoor = keyDoor.GetComponent<KeyDoor>();
        if (_keyDoor)
        {
            string _matchingKey;

            if (_keyDoor.PlayerHasMatchingKey(inventory.KeysNames, out _matchingKey))
            {
                matchingKey = _matchingKey;
                return true;
            }
        }

        matchingKey = null;
        return false;
    }

    public bool PickableItemIsNear(out GameObject pickable)
    {
        var colliders = Physics.OverlapSphere(gameObject.transform.position, CanPickUpItemRadius);

        foreach (var collider in colliders)
        {
            IPickableItem _pickable = collider.gameObject.GetComponent<IPickableItem>();

            if (_pickable!=null) {
                pickable = collider.gameObject;
                return true;
            }
        }

        pickable = null;
        return false;
    }

    public void PickUpItem()
    {

        GameObject item;

        if (PickableItemIsNear(out item) && !item.GetComponent<IPickableItem>().IsPickedUp())
        {
            Debug.Log("pickable item is near");
            /*
            inventory.KeysNames.Add(key.name);

            inventory.KeyPickedUp?.Invoke(inventory.KeysNames);

            //Анимация подбора ключа

            key.GetComponent<KeyForDoor>().isPickedUp = true;
            key.GetComponent<KeyForDoor>().PickUp(gameObject);
            */
            item.GetComponent<IPickableItem>().PickUp(this.gameObject);

        }
    }



    public bool CheckpointIsNear(out GameObject checkpoint)
    {
        var colliders = Physics.OverlapSphere(gameObject.transform.position, CanPickUpItemRadius);

        foreach (var collider in colliders)
        {
            if (collider.gameObject.GetComponent<Checkpoint>())
            {

                checkpoint = collider.gameObject;
                return true;

            }
        }

        checkpoint = null;
        return false;
    }

    public void SaveCheckpoint()
    {

        GameObject checkpoint;

        if (CheckpointIsNear(out checkpoint))
        {
            if (checkpoint.TryGetComponent<Checkpoint>(out Checkpoint _checkpoint))
            {
                _checkpoint.Save();
            };
        }
    }

    public void Interact()
    {

        RaycastHit hit;

        Ray ray = FPSCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, CanInteractRadius))
        {

            //Debug.Log("Physics.Raycast(ray, out hit, CanInteractRadius) START");
            Transform objectHit = hit.transform;
            if (objectHit.gameObject.TryGetComponent(out Interactable interactable))
            {
                //Debug.Log("objectHit.gameObject.TryGetComponent(out Interactable interactable) START");
                if (currentInteractable == interactable.gameObject)
                {
                    return;
                }
                if (currentInteractable)
                {
                    currentInteractable.GetComponent<Interactable>().ReleaseInteraction();

                }


                currentInteractable = interactable.gameObject;
                currentInteractable.GetComponent<Interactable>().Interact();

                InteractableChanged?.Invoke(currentInteractable.GetComponent<Interactable>());

                //Debug.Log("objectHit.gameObject.TryGetComponent(out Interactable interactable) END");

                //Debug.Log("environment " + Goid);


                //Debug.Log("Physics.Raycast(ray, out hit, CanInteractRadius) END");
            }
            else
            {
                if (currentInteractable)
                {
                    currentInteractable.GetComponent<Interactable>().ReleaseInteraction();
                    currentInteractable = null;
                }
            }
        }

    }

}
