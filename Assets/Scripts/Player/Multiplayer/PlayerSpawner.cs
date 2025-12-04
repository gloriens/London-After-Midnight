using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;

    void Start()
    {
        SpawnLocalPlayer();
    }

    private void SpawnLocalPlayer()
    {
        Instantiate(playerPrefab, transform.position, transform.rotation);
    }
}
