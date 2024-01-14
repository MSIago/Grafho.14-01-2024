using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;
    private Rigidbody2D rb;
    public float rotateSpeed = 0.1f;
    private int vida = 3;
    public UnityEvent morte;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if(!target) {
            GetTarget();
        } else {
            RotateTowardsTarget();
        }
    }
    private void FixedUpdate()
    {
        if(target){
            rb.velocity = transform.up * speed;
            Morrer();
        }
    }
    private void RotateTowardsTarget()
    {
        Vector2 targetDirection = target.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rotateSpeed);
    }
    private void GetTarget ()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public void TomarDano(int dano)
    {
        this.vida -= dano;
        Debug.Log("Deu " + dano + " de dano");
    }
    private void Morrer()
    {
        if(vida == 0){
            Destroy(this.gameObject);
            morte.Invoke();
        }
    }
}   
