using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletController : MonoBehaviour
{
    private int velocity = 5;
    private GameManagerController gameManager;
    Rigidbody2D rb;
    Collider2D cl;
    // Start is called before the first frame update
    public void SetRightDirection(){
        velocity = 5;
    }
    public void SetLeftDirection(){
        velocity = -5;
    }

    void Start()
    {
        gameManager = FindObjectOfType<GameManagerController>();
        rb = GetComponent<Rigidbody2D>();
        cl = GetComponent<Collider2D>();
        Destroy(this.gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(velocity, 0);
    } 
    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag =="Enemy")//para colisionar con el piso de fondo
        {
            Destroy(other.gameObject);
            //gameManager.GastarBala();
        } 
        Destroy(this.gameObject);
    }
}
