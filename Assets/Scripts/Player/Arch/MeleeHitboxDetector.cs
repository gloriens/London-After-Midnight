using System.Collections.Generic;
using UnityEngine;

public class MeleeHitboxDetector : MonoBehaviour
{
    private readonly List<IPlayer> targets = new List<IPlayer>();

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<IPlayer>();
        if (player != null && !targets.Contains(player))
        {
            targets.Add(player);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<IPlayer>();
        if (player != null)
        {
            targets.Remove(player);
        }
    }

    public IPlayer GetClosestTarget(Transform from)
    {
        CleanList();   // multiplayer için kritik

        IPlayer closest = null;
        float closestDist = float.MaxValue;

        foreach (var t in targets)
        {
            if (t == null) continue;

            float d = Vector3.Distance(from.position, t.Transform.position);
            if (d < closestDist)
            {
                closestDist = d;
                closest = t;
            }
        }

        return closest;
    }

    private void CleanList()
    {
        for (int i = targets.Count - 1; i >= 0; i--)
        {
            if (targets[i] == null)
                targets.RemoveAt(i);
        }
    }
}