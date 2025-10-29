using UnityEngine;

public class Projetil : MonoBehaviour
{
    public Status status;       // Status do player para calcular dano
    public float lifeTime = 2f;
    public float speed = 10f;

    private Vector2 direction;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        if (direction != Vector2.zero)
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyStatus enemy = other.GetComponent<EnemyStatus>();
        if (enemy != null)
        {
            enemy.ReceberDano(status.GetDanoMaximo());
        }
        Destroy(gameObject);
    }

}
