﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class WeaponData : ItemData
{
    // animation to play when attacking
    public int animationNumber;
    public float speed;               // speed at which the weapon attacks in seconds
    public Vector2 hitBoxSize;        // the size of the hit box to be created when attacking
    public int damage;                // how much damage does it do upon hitting something
    public float knockback;           // How much knockback is applied on hit.
}
