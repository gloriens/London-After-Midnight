using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private ItemType itemType;

    public Iitem CreateItem()
    {
        switch (itemType)
        {
            case ItemType.Kebab:
                return new KebabItem();
        }

        return null;
    }

    public void OnPicked(IPlayer player)
    {
        var item = CreateItem();
        player.Inventory.SetItem(item);
        Destroy(gameObject);
    }
}