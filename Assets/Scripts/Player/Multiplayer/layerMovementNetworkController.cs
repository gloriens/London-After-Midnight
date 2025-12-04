using UnityEngine;
using Mirror;
using StarterAssets;
using UnityEngine.InputSystem;


public class PlayerMovementNetworkController : NetworkBehaviour
{
    [SerializeField] private GameObject playerCapsule;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private MeleeHitboxDetector meleeHitbox;

    private PlayerInput inputSys;

    private void Awake()
    {
        inputSys = playerCapsule.GetComponent<PlayerInput>();

        // Remote player için input & kamera kapanır
        if (!isLocalPlayer)
        {
            if (inputSys) inputSys.enabled = false;
            if (playerCamera) playerCamera.gameObject.SetActive(false);
            if (meleeHitbox) meleeHitbox.gameObject.SetActive(false);
        }
    }

    public override void OnStartLocalPlayer()
    {
        // Kamerayı sadece local aç
        if (playerCamera) playerCamera.gameObject.SetActive(true);

        // Input yalnızca local oyuncuda çalışır
        if (inputSys)
        {
            inputSys.enabled = true;
            inputSys.camera = playerCamera;
        }

        // Movement’i aç
        EnableMovement();

        if (meleeHitbox) meleeHitbox.gameObject.SetActive(true);
    }

    private void EnableMovement()
    {
        var c = playerCapsule.GetComponent<FirstPersonController>();
        if (c) c.enabled = true;

        var i = playerCapsule.GetComponent<StarterAssetsInputs>();
        if (i) i.enabled = true;
    }
}