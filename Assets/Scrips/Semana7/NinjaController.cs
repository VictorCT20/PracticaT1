using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaController : MonoBehaviour
{
    public int velocity = 0, velSalto = 5, salto = 2;
    public GameObject Kunai;
    private GameObjectController gameManager;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;
    Collider2D cl;
    AudioSource audioSource;
    const int ANI_QUIETO = 0;
    const int ANI_CORRER = 1;
    const int ANI_SALTO = 2;
    const int ANI_ATACAR = 3;
    const int ANI_SALTO_ATACAR = 4;
    const int ANI_DISPARAR = 5;
    int direction = 1;
    public bool isGrounded = true;
    int cont;
    int disparo = -1;
    void Start()
    {
        gameManager = FindObjectOfType<GameObjectController>();
        Debug.Log("Iniciando script de player");
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        cl = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
        gameManager.MatarZobie(0);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(-velocity, rb.velocity.y);
        Movimiento();
        if(Input.GetKeyDown("x")){
            Ataque();
        }
        if(Input.GetKeyDown("z")){
            CambioDisparo();
        }
    }
    void Movimiento(){

        if(Input.GetKeyDown(KeyCode.Space)) 
            Jump();
        
        

        if(Input.GetKeyDown(KeyCode.LeftArrow))
            WalkToLeft();
        else if(Input.GetKeyUp(KeyCode.LeftArrow))
            StopWalk();
        else if(Input.GetKeyDown(KeyCode.RightArrow))
            WalkToRight();
        else if(Input.GetKeyUp(KeyCode.RightArrow))
            StopWalk();

        
    }
    public void WalkToLeft()
    {
        velocity = 5;
        sr.flipX = true;
        ChangeAnimation(ANI_CORRER);
        direction = -1;
    }
    public void WalkToRight()
    {
        velocity = -5;
        sr.flipX = false;
        ChangeAnimation(ANI_CORRER);
        direction = 1;
    }
    public void StopWalk()
    {
        velocity = 0;
        ChangeAnimation(ANI_QUIETO);
        rb.velocity = new Vector2(0, rb.velocity.y);
    }
    public void Jump()
    {
        if(cont>0){
            isGrounded = false;
            rb.AddForce(new Vector2(0, velSalto), ForceMode2D.Impulse);
            ChangeAnimation(ANI_SALTO);
            cont--;
        }

    }

    public void CambioDisparo(){
        disparo = disparo*-1;
        gameManager.TipoAtaque = gameManager.TipoAtaque*-1;
        gameManager.MatarZobie(0);
    }
    public void Ataque(){
        
        if(disparo==1){
            Disparar();
        }
        else if(disparo==-1){
            ChangeAnimation(ANI_ATACAR);
        }
        
    }
    void Disparar(){
        ChangeAnimation(ANI_DISPARAR);
        var bulletPosition = transform.position + new Vector3(direction,0,0);
        var o = Instantiate(Kunai, bulletPosition, Quaternion.identity) as GameObject;
        var c = o.GetComponent<BulletsController>();
        if(direction==-1) c.SetLeftDirection();
        else c.SetRightDirection();
        
    }
    void OnCollisionEnter2D(Collision2D other){
        cont=salto;
        ChangeAnimation(ANI_QUIETO);

    } 
    private void ChangeAnimation(int a){
        animator.SetInteger("Estado", a);
    }
}
