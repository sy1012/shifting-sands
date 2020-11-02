using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Breakable", menuName = "Enviroment Breakable")]
public class EnviromentBreakableData : ScriptableObject, IDamagable
{
    public int quality;
    public int health;
    public Sprite sprite;
    public Sprite[] destructionAnimation;
    public Sprite[] beingHitAnimation;

    public virtual void OnDestroy()
    {
        throw new System.NotImplementedException();
    }

    public virtual void TakeDamage(int damage)
    {
        throw new System.NotImplementedException();
    }
}
