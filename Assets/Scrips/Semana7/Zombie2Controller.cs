using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie2Controller : MonoBehaviour
{
    float velocity = 3;
    int vida = 1;
    Rigidbody2D rb;
    SpriteRenderer sr;
    private ManagerController gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<ManagerController>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(vida<1){
            Destroy(this.gameObject);
            gameManager.MatarZombie(1);
        }
        rb.velocity = new Vector2(-velocity, rb.velocity.y);//hace que el zombie camine
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "barrera"){
            velocity *= -1;
            if(velocity<0)
                sr.flipX = false;
            else   
                sr.flipX = true;
        }
    }
    void OnCollisionEnter2D(Collision2D other){

        if(other.gameObject.tag == "Kunai"){
            vida = vida - 1;
        }
        if(other.gameObject.tag == "Espada"){
            vida = vida - 2;
        }

        if(other.gameObject.name =="DarkHole")//para colisionar con el piso de fondo
        {
            Destroy(this.gameObject);
        } 

    }
}
