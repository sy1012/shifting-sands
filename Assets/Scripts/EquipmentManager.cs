using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    private WeaponData weaponEquipped;
    public GameObject weaponObject;
    private RuneData runeEquipped;
    public GameObject runeObject;
    private ArmourData armourEquipped;
    public GameObject armourObject;

    public void Start()
    {
        if (weaponEquipped != null) { SetWeapon(weaponEquipped); }
        if (runeEquipped != null) { SetRune(runeEquipped); }
        if (armourEquipped != null) { SetArmour(armourEquipped); }
    }

    public void SetWeapon(WeaponData newWeapon)
    {
        this.weaponObject.GetComponent<Weapon>().data = newWeapon;
        if (newWeapon != null){ this.weaponObject.GetComponent<Weapon>().Initialize(); }
    }

    public Weapon GetWeapon()
    {
        Debug.Log(weaponObject);
        return weaponObject.GetComponent<Weapon>();
    }

    public void SetRune(RuneData newRune)
    {
        this.runeObject.GetComponent<RuneObject>().data = newRune;
        if (GetWeapon().data != null) { this.weaponObject.GetComponent<Weapon>().Initialize(); }
    }

    public RuneObject GetRune()
    {
        return runeObject.GetComponent<RuneObject>();
    }

    public void SetArmour(ArmourData newArmour)
    {
        this.armourObject.GetComponent<ArmourObject>().data = newArmour;
    }

    public ArmourObject GetArmour()
    {
        return armourObject.GetComponent<ArmourObject>();
    }
}
