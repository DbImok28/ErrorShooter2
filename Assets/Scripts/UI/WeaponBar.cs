using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponBar : MonoBehaviour
{
    public struct WeaponItem
    {
        public string name;
        public Sprite icon_active;
        public Sprite icon_unactive;
    }

    List<GameObject> weaponItems;
    List<WeaponItem> _weaponItems;

    private bool ItemsAreInited;

    public Text text;

    private PlayerInventory inventory;
    public void Init(PlayerController player,PlayerInventory inventory)
    {
        player.WeaponChanged.AddListener(SetValue);

        this.inventory = inventory;

        if (!ItemsAreInited)
        {
            InitWeaponItems();
        }
         
        
    }

    public void SetActive(GameObject weaponItem, WeaponItem _weaponItem)
    {
        weaponItem.transform.GetChild(0).GetComponent<Image>().sprite = _weaponItem.icon_active;
       
        
    }
    public void SetUnactive(GameObject weaponItem, WeaponItem _weaponItem)
    {
        weaponItem.transform.GetChild(0).GetComponent<Image>().sprite = _weaponItem.icon_unactive;
    }

    public void InitWeaponItems()
    {

        weaponItems = new List<GameObject>();
        _weaponItems = new List<WeaponItem>();

        GameObject ItemTemplate = transform.GetChild(0).gameObject;

        
        foreach (GameObject go in inventory.Weapons)
        {
            Weapon weapon = go.GetComponentInChildren<Weapon>();

            
            WeaponItem wi = new WeaponItem();
            wi.name = weapon.WeaponName;
            wi.icon_active = weapon.icon_active;
            wi.icon_unactive = weapon.icon_unactive;

            _weaponItems.Add(wi);

            

            GameObject item = Instantiate(ItemTemplate, transform);
            item.transform.GetChild(1).GetComponent<Text>().text = weapon.WeaponName;
            item.transform.GetChild(0).GetComponent<Image>().sprite = weapon.icon_unactive;

            weaponItems.Add(item);

        }

        ItemsAreInited = true;
        

        


        Destroy(ItemTemplate);
    }

    public void SetValue(Weapon weaponCur)
    {
        //Debug.Log(weaponCur);
        //text.text = weaponCur.WeaponName;

        //Debug.Log($" wi c {weaponItems.Count}");

        for (int i = 0; i < weaponItems.Count; i++)
        {
            
            SetUnactive(weaponItems[i], _weaponItems[i]);

            if (_weaponItems[i].name == weaponCur.WeaponName)
            {
                SetActive(weaponItems[i], _weaponItems[i]);
            }
        }

        
    }
}
