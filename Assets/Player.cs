using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [Range(0.1f, 2f)]
    [SerializeField] private float fireRate = 0.5f;
    private float fireTimer;
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public int health;
    public int maxHealth = 3;
    public int enemyDamage = 1;
    public Slider healthBar;
    public GameObject background;
    private AssetBundle myLoadedAssetBundle;
    private string[] scenePaths;
    Vector2 movement;
    Vector2 mousePos;
    void Start()
    {
        background.SetActive(false);
        health = maxHealth;
        healthBar.value = health;
    }
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && fireTimer <= 0f)//Ativação do Tiro
        {
            Shoot();
            fireTimer = fireRate;
        } else {
            fireTimer -= Time.deltaTime;
        }
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        float angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
    void Shoot()
    {
        Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        col.gameObject.CompareTag("Enemy");
        if(col.gameObject.CompareTag("Enemy"))
        {
            health -= enemyDamage;
            healthBar.value = health;
            if(health <= 0)
            {
                background.SetActive(true);
                Destroy(this.gameObject);
            }
        }
    }
}
