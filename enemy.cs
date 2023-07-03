using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{

    public int enemyHealth = 50;
    public float enemySpeed = 1.0f;
    private GameObject playerPos;
    private Rigidbody2D rb;
    Vector3 tarDirection;
    float angle;
    Quaternion q;

    public List<GameObject> pickupSpawns;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player");
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        tarDirection = playerPos.transform.position - transform.position;
        angle = Mathf.Atan2(tarDirection.y, tarDirection.x) * Mathf.Rad2Deg - 90;
        q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 180);
        
        if(enemyHealth <= 0)
        {
            EnemyDeath();
        }

        float enemyDistance = Vector3.Distance(playerPos.transform.position, this.transform.position);
        if(enemyDistance < 2)
        {
            playerPos.SendMessage("TakeDamage");
        }

        
    }

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, 
        playerPos.transform.position, enemySpeed * Time.fixedDeltaTime);
    }


    void EnemyDeath()
    {
        float ranNum = Random.Range(0.0f, 100.0f);
        if(ranNum < 25.0f)
        {
            int ranDrop = Random.Range(0, 4);
            Instantiate(pickupSpawns[ranDrop], transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        
        if(other.CompareTag("Bullet"))
        {
            enemyHealth = enemyHealth - 20;
            
        }
    }
}
