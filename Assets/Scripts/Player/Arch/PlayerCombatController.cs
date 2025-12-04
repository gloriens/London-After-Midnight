using Mirror;
using StarterAssets;
using UnityEngine;

public class PlayerCombatController : NetworkBehaviour
{
    private Player player;
    private StarterAssetsInputs inputs;
    private Camera cam;

    private void Awake()
    {
        player = GetComponent<Player>();
        inputs = GetComponentInChildren<StarterAssetsInputs>();
        cam = Camera.main;
    }

    private void Update()
    {
        if (!isLocalPlayer) return;

        if (inputs.attack)
        {
            inputs.attack = false;
            TryUseItem();
        }

        if (inputs.interact)
        {
            inputs.interact = false;
            TryPickup();
        }
    }

    private void TryUseItem()
    {
        if (!player.Inventory.HasItem) return;

        var weapon = player.Inventory.GetItem() as IWeapon;
        if (weapon == null) return;

        var target = player.MeleeHitbox.GetClosestTarget(player.Transform) as Player;
        if (target == null) return;

        var targetId = target.GetComponent<NetworkIdentity>();
        if (targetId == null) return;

        CmdUseItem(targetId);
    }

    [Command]
    private void CmdUseItem(NetworkIdentity targetIdentity)
    {
        var attackerPlayer = GetComponent<Player>();
        var targetPlayer = targetIdentity.GetComponent<Player>();

        if (attackerPlayer == null || targetPlayer == null)
            return;

        // self-hit yok
        if (attackerPlayer == targetPlayer)
            return;

        // 1) range check (server authority)
        float distance = Vector3.Distance(attackerPlayer.Transform.position, targetPlayer.Transform.position);
        if (distance > 2.0f)
            return;

        // 2) item check
        if (!attackerPlayer.Inventory.HasItem)
            return;

        var weapon = attackerPlayer.Inventory.GetItem() as IWeapon;
        if (weapon == null)
            return;

        // 3) damage apply (server-side)
        weapon.Use(attackerPlayer, targetPlayer);
    }

    private void TryPickup()
    {
        if (cam == null) return;

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, 3f))
        {
            var pickup = hit.collider.GetComponent<ItemPickup>();
            if (pickup != null)
            {
                pickup.OnPicked(player);
            }
        }
    }
}
