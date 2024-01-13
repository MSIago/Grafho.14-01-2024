using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiro : MonoBehaviour
{
    [SerializeField]private int dano = 1;
    [Range(1,10)]
    [SerializeField] private float speed = 8f;
    [Range(1,10)]
    [SerializeField] private float lifeTime = 3f;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }
    private void FixedUpdate()
    {
        rb.velocity = transform.up * speed;
    }
    void OnTriggerEnter2D(Collider2D objeto)
    {
        objeto.CompareTag("Enemy");
        if (objeto.CompareTag("Enemy")){
            Debug.Log("Tiro Acertou " + objeto.tag);
            Destroy(this.gameObject);
            objeto.SendMessage("TomarDano", this.dano);
        }
    }
}