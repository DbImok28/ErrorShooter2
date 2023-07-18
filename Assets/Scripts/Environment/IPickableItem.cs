using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IPickableItem
{
    public void PickUp(GameObject go);

    public void HideAfterPickUp();

    public bool IsPickedUp();

    public UnityEvent<GameObject> OnPickUp { get; }
}
