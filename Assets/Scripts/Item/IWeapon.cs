using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon : Iitem
{
    void Use(IPlayer player, IPlayer target);
    float damage { get; }
}
