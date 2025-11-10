using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private EnemyStatus status;  // Status do inimigo que gerou o ataque
    [SerializeField] private float speed = 8f;
    [SerializeField] private float lifeTime = 0.4f;
    private Vector2 direction;


    void Start()
    {
        Destroy(gameObject, lifeTime);
        status = GameObject.FindGameObjectWithTag("InimigoTerrestre").GetComponent<EnemyStatus>();
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Status playerStatus = other.GetComponent<Status>();
            if (playerStatus != null && status != null)
            {
                playerStatus.ReceberDano(status.GetDanoMaximo());
            }
            Destroy(gameObject);
        }
    }
}
