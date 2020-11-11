using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    private WeaponData weaponEquipped;
    public GameObject weaponObject;
    //[SerializeField]
    //GameObject weapon;
    //[SerializeField]
    //GameObject armor;
    //[SerializeField]
    //GameObject ability;

    //public Weapon Weapon { get => weapon.GetComponent<Weapon>(); }
    //public GameObject Armor { get => armor;}
    //public GameObject Ability { get => ability;}

    public void Start()
    {
        if (weaponEquipped != null) { SetWeapon(weaponEquipped); }
    }

    public void SetWeapon(WeaponData newWeapon)
    {
        //Destroy old copy of weapon if any
        //if (Weapon != null)
        //{
        //    Destroy(Weapon);
        //}
        ////Position COPY of weapon and parent it
        //Instantiate(newWeapon, transform.position, Quaternion.identity, transform);
        this.weaponObject.GetComponent<Weapon>().data = newWeapon;
        this.weaponObject.GetComponent<Weapon>().Initialize();
    }

    public Weapon GetWeapon()
    {
        return weaponObject.GetComponent<Weapon>();
    }
    //public void AddArmor(GameObject armor)
    //{

    //    //Set up reference
    //    this.armor = armor;
    //}
    //public void AddAbility(GameObject newAbility)
    //{
    //    //Check if has a weapon component
    //    Ability a = newAbility.GetComponent<Ability>();
    //    if (a == null)
    //    {//Not a ability
    //        return;
    //    }
    //    if (newAbility != null)
    //    {
    //        Destroy(newAbility);
    //    }
    //    Instantiate(newAbility, transform.position, Quaternion.identity, transform);
    //    this.ability = newAbility;
    //}

}
