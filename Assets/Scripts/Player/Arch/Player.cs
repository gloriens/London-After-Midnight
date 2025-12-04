using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour, IPlayer
{
    [SerializeField] private float maxHealth = 100f;
    public float MaxHealth => maxHealth;

    [SyncVar]
    private float _health;
    public float Health => _health;

    public Inventory Inventory { get; private set; }
    public Transform Transform => transform;

    [SerializeField] private MeleeHitboxDetector meleeHitbox;
    public MeleeHitboxDetector MeleeHitbox => meleeHitbox;

    private void Awake()
    {
        Inventory = new Inventory();
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        _health = maxHealth;
    }

    [Server]
    public void Stab(float damage)
    {
        if (_health <= 0) return;

        _health -= damage;

        if (_health <= 0)
        {
            NetworkServer.Destroy(gameObject);
        }
    }
}