using UnityEngine;

public class Player : MonoBehaviour, IPlayer
{
    [SerializeField] private float maxHealth = 1f;
    public float MaxHealth { get; private set; }
    public float Health { get; private set; }
    
    
    public Inventory Inventory { get; private set; }
    
    
    public Transform Transform => transform;

    [SerializeField] private MeleeHitboxDetector meleeHitbox;

    private void Awake()
    {
        MaxHealth = maxHealth;
        Health = MaxHealth;
        
        Inventory = new Inventory();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            TryUseItem();

        CheckPickup();
    }

    private void CheckPickup()
    {
        if (!Input.GetKeyDown(KeyCode.E))
            return;

        Camera cam = Camera.main;
        if (cam == null) return;

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, 3f))
        {
            var pickup = hit.collider.GetComponent<ItemPickup>();
            if (pickup != null)
            {
                pickup.OnPicked(this);
            }
        }
    }

    public void TryUseItem()
    {
        Debug.Log("meleeHitbox = " + meleeHitbox);
        Debug.Log("HasItem = " + Inventory.HasItem);
        Debug.Log("Item = " + Inventory.GetItem());
    
        var weapon = Inventory.GetItem() as IWeapon;
        Debug.Log("weapon = " + weapon);

        if (!Inventory.HasItem) return;
        if (weapon == null) return;

        var target = meleeHitbox.GetClosestTarget(transform);
        Debug.Log("target = " + target);

        if (target == null) return;
        if ((target as MonoBehaviour) == null) return;

        weapon.Use(this, target);
    }


    public void Stab(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
        Debug.Log("Oyuncu bıçaklandı. Damage: " + damage);
    }
}