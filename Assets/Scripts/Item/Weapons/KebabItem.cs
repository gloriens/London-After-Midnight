using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KebabItem : IWeapon
{
    public float damage { get; }

    public KebabItem()
    {
        damage = 1;
    }
    
    public void Use(IPlayer player, IPlayer target)
    {
        if (target == null) return;
        if (target.Transform == null) return; // destroyed objeyi yakalar
        target.Stab(damage);
    }
}
