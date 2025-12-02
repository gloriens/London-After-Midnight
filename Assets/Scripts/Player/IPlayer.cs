using UnityEditor;
using UnityEngine; 

public interface IPlayer
{
    float MaxHealth { get; }
    float Health { get; }
    Inventory Inventory { get; }
    Transform Transform { get; } 
    void TryUseItem();
    void Stab(float damage);
}