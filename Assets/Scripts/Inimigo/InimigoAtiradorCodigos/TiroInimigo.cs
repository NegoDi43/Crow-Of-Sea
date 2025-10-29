using UnityEngine;

public class TiroInimigo : MonoBehaviour
{
    public float speed = 8f;
    public float lifeTime = 2f;

    private Vector2 direction;
    private float dano;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void SetDirection(Vector2 dir, float danoInimigo)
    {
        direction = dir.normalized;
        dano = danoInimigo;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
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
            if (playerStatus != null)
            {
                playerStatus.ReceberDano(dano);
            }
            Destroy(gameObject);
        }
    }
}
