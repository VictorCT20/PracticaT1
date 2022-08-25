using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaballeroController : MonoBehaviour
{
    public int velocityCam = 3, velocityCorr = 7, velSalto = 3;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;
    Collider2D cl;
    // Start is called before the first frame update
    const int ANI_QUIETO = 0;
    const int ANI_CORRER = 1;
    const int ANI_CAMINAR = 2;
    const int ANI_SALTO = 3;
    const int ANI_ATAQUE = 4;
    bool puedeSaltar = true;
    void Start()
    {
        Debug.Log("Iniciando script de player");
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        cl = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.RightArrow)){
            sr.flipX = false;
            if(Input.GetKey("x")){
                rb.velocity = new Vector2(velocityCorr, rb.velocity.y);
                ChangeAnimation(ANI_CORRER);
            }else{
                rb.velocity = new Vector2(velocityCam, rb.velocity.y);
                ChangeAnimation(ANI_CAMINAR);
            }
            
        }
        else if(Input.GetKey(KeyCode.LeftArrow)){
            sr.flipX = true;
            if(Input.GetKey("x")){
                rb.velocity = new Vector2(-velocityCorr, rb.velocity.y);
                ChangeAnimation(ANI_CORRER);
            }else{
                rb.velocity = new Vector2(-velocityCam, rb.velocity.y);
                ChangeAnimation(ANI_CAMINAR);
            }
        }
        else if(Input.GetKey("z")){
            rb.velocity = new Vector2(0, rb.velocity.y);
            ChangeAnimation(ANI_ATAQUE);
        }
        else{
            rb.velocity = new Vector2(0, rb.velocity.y);
            ChangeAnimation(ANI_QUIETO);
        }
        if(Input.GetKeyDown(KeyCode.Space) && puedeSaltar==true){
            //rb.velocity = new Vector2(rb.velocity.x, velSalto);
            rb.AddForce(new Vector2(0, velSalto), ForceMode2D.Impulse);
            ChangeAnimation(ANI_SALTO);
            puedeSaltar = false;    
        }
    }
    void OnCollisionEnter2D(Collision2D other){
        puedeSaltar = true;
    }
    private void ChangeAnimation(int a){
        animator.SetInteger("Estado", a);
    }


}
