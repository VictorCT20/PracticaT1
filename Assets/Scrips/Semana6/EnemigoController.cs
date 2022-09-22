using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoController : MonoBehaviour
{
    float velocity = 3;
    int vida = 3;
    Rigidbody2D rb;
    SpriteRenderer sr;
    private GameManagerController gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManagerController>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(vida<1){
            Destroy(this.gameObject);
        }
        rb.velocity = new Vector2(velocity, rb.velocity.y);//hace que el zombie camine
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "barrera"){
            velocity *= -1;
            if(velocity>0)
                sr.flipX = false;
            else   
                sr.flipX = true;
        }
    }
    void OnCollisionEnter2D(Collision2D other){

        if(other.gameObject.tag == "Oro"){
            vida = vida - 3;
        }
        if(other.gameObject.tag == "Plata"){
            vida = vida - 2;
        }
        if(other.gameObject.tag == "Bronce"){
            vida = vida - 1;
        }
    }
}
