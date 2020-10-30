using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class WeaponData : ItemData
{
    new ItemTypes.Type itemType = ItemTypes.Type.weapon;
    public Sprite[] spriteAnimation;  // animation to play when attacking
    public float speed;               // speed at which the weapon attacks in seconds
    public Vector2 hitBoxSize;        // the size of the hit box to be created when attacking
    public int damage;                // how much damage does it do upon hitting something
}
