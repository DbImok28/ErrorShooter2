using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableInfo : MonoBehaviour
{
    // Start is called before the first frame update

    /*
    public Text text;

    public void Init(PlayerEnvironmentInteraction pei)
    {
        //Debug.Log("int init");
        pei.OnInteractableChanged.AddListener(SetValue);
        pei.Blabla.AddListener(Bla);
    }

    public void SetValue(string value)
    {
        Debug.Log("pei set value");
        text.text = value;
    }

    public void Bla(string value)
    {
        text.text = "bla";
    }
    */

    private bool ItemIsInited;

    public void PrintSmth(string msg)
    {
        ItemInfoTextField.text = msg;
    }

    public void Init(PlayerEnvironmentInteraction pei)
    {
        

        if (!ItemIsInited)
        {
            Debug.Log("ii init");
            this.playerEnvironmentInteraction = pei;

            

            playerEnvironmentInteraction.InteractableChanged.AddListener( OnInteractabeAssigned);

            //Debug.Log("interactable " + playerEnvironmentInteraction.Goid);

            ItemIsInited = true;
        }
        
        
    }

    public Text ItemInfoTextField;

    private GameObject currentInteractable;
    private PlayerEnvironmentInteraction playerEnvironmentInteraction;
    private Interactable interactable;

    private void SetItemInfoText(Interactable interactable)
    {
        ItemInfoTextField.text = interactable.ItemInfo;
    }

    private void SetItemInfoTextDefault(Interactable interactable)
    {
        ItemInfoTextField.text = "";
    }

    public void OnInteractabeAssigned(Interactable interactable)
    {
        
        this.interactable = interactable;

        SetItemInfoText(interactable);

        this.interactable.InteractableMouseEnter.AddListener(SetItemInfoText);
        this.interactable.InteractableMouseLeave.AddListener(SetItemInfoTextDefault);
        
        //Debug.Log(interactable.ItemInfo);
        
    }
    

    // Update is called once per frame
    void Update()
    {

    }

}
