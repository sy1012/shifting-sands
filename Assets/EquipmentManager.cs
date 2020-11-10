using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    [SerializeField]
    Weapon weapon;
    [SerializeField]
    GameObject armor;
    [SerializeField]
    GameObject ability;

    public Weapon Weapon { get => weapon; }
    public GameObject Armor { get => armor;}
    public GameObject Ability { get => ability;}

    public void AddWeapon(GameObject newWeapon)
    {
        //Check if has a weapon component
        Weapon w = newWeapon.GetComponent<Weapon>();
        if (w == null)
        {//Not a Weapon
            return;
        }
        //Destroy old copy of weapon if any
        if (Weapon != null)
        {
            Destroy(Weapon);
        }
        //Position COPY of weapon and parent it
        Instantiate(newWeapon, transform.position, Quaternion.identity, transform);
        this.weapon = newWeapon.GetComponent<Weapon>();
    }
    public void AddArmor(GameObject armor)
    {

        //Set up reference
        this.armor = armor;
    }
    public void AddAbility(GameObject newAbility)
    {
        //Check if has a weapon component
        Ability a = newAbility.GetComponent<Ability>();
        if (a == null)
        {//Not a Weapon
            return;
        }
        if (newAbility != null)
        {
            Destroy(newAbility);
        }
        Instantiate(newAbility, transform.position, Quaternion.identity, transform);
        this.ability = newAbility;
    }

}
