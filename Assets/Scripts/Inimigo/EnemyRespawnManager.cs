using UnityEngine;
using System.Collections.Generic;

public class EnemyRespawnManager : MonoBehaviour
{
    public static EnemyRespawnManager Instance { get; private set; }
    private List<EnemyRespawner> respawners = new List<EnemyRespawner>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        respawners.AddRange(FindObjectsOfType<EnemyRespawner>());
    }

    public void DisableAllRespawns()
    {
        foreach (var r in respawners)
            r.enabled = false;
    }

    public void EnableAllRespawns()
    {
        foreach (var r in respawners)
            r.enabled = true;
    }

    public void ForceRespawnAll()
    {
        foreach (var r in respawners)
            r.ForceRespawn();
    }
}
