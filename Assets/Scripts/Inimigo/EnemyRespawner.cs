using UnityEngine;
using System.Collections;

public class EnemyRespawner : MonoBehaviour
{
    [Header("Configuração de Respawn")]
    public GameObject enemyPrefab;        // Prefab do inimigo
    public Transform spawnPoint;          // Posição onde o inimigo nasce
    public float respawnDelay = 15f;      // Tempo em segundos até reaparecer

    private GameObject currentEnemy;
    private bool isRespawning;

    private void Start()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab == null || spawnPoint == null)
        {
            Debug.LogWarning($"[Respawner] Prefab ou SpawnPoint não configurado em {name}");
            return;
        }

        currentEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        var status = currentEnemy.GetComponent<EnemyStatus>();
        if (status != null)
            status.OnEnemyDeath += HandleEnemyDeath;
    }

    private void HandleEnemyDeath(EnemyStatus enemy)
    {
        if (!isRespawning)
            StartCoroutine(RespawnEnemy());
    }

    private IEnumerator RespawnEnemy()
    {
        isRespawning = true;
        yield return new WaitForSeconds(respawnDelay);

        if (currentEnemy != null)
        {
            currentEnemy.GetComponent<EnemyStatus>().Reviver();
            currentEnemy.transform.position = spawnPoint.position;
            currentEnemy.transform.rotation = spawnPoint.rotation;
        }
        else
        {
            SpawnEnemy();
        }

        isRespawning = false;
    }

    // Opcional: pode ser chamado quando o jogador se afasta muito
    public void ForceRespawn()
    {
        if (currentEnemy != null)
            currentEnemy.GetComponent<EnemyStatus>().Reviver();
    }
}
