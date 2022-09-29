using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegamanController : MonoBehaviour
{
    public int velocity = 2, velSalto = 5, salto = 2;
    private float timer = 0.0f;
    private float BalaMediana = 3.0f;
    private float BalaGrande = 5.0f;
    public GameObject bulletP;
    public GameObject bulletM;
    public GameObject bulletG;
    private GameManagerController gameManager;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;
    Collider2D cl;
    AudioSource audioSource;
    
    const int ANI_QUIETO = 0;
    const int ANI_CORRER = 1;
    const int ANI_SALTO = 2;
    const int ANI_DISPARAR = 3;
    int direction = 1;
    int cont;
    bool disparo = false;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManagerController>();
        Debug.Log("Iniciando script de player");
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        cl = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(disparo == true) timer = 0.0f;
        Movimiento();
        Disparar();
    }
    void Movimiento(){
        if(Input.GetKey(KeyCode.RightArrow)){
            rb.velocity = new Vector2(velocity, rb.velocity.y);
            sr.flipX = false;
            ChangeAnimation(ANI_CORRER);
            direction = 1;
        }
        else if(Input.GetKey(KeyCode.LeftArrow)){
            rb.velocity = new Vector2(-velocity, rb.velocity.y);
            sr.flipX = true;
            ChangeAnimation(ANI_CORRER);
            direction = -1;
        }
        else{
            rb.velocity = new Vector2(0, rb.velocity.y);
            ChangeAnimation(ANI_QUIETO);
        }
        if(Input.GetKeyDown(KeyCode.Space) && cont>0){
            rb.AddForce(new Vector2(0, velSalto), ForceMode2D.Impulse);
            ChangeAnimation(ANI_SALTO);
            cont--;
        }
    }
    void Disparar(){
        disparo = false;
        if(Input.GetKey("x")){
            timer += Time.deltaTime;
            ChangeAnimation(ANI_DISPARAR);
        }
        if(Input.GetKeyUp("x")) disparo = true;

        Debug.Log(timer);

            if(timer>BalaGrande && disparo == true){
                var bulletPosition = transform.position + new Vector3(direction,0,0);
                var o = Instantiate(bulletG, bulletPosition, Quaternion.identity) as GameObject;
                var c = o.GetComponent<BulletsController>();
                if(direction==-1) c.SetLeftDirection();
                else c.SetRightDirection();
            }
            else if(timer>BalaMediana && disparo == true){
                var bulletPosition = transform.position + new Vector3(direction,0,0);
                var o = Instantiate(bulletM, bulletPosition, Quaternion.identity) as GameObject;
                var c = o.GetComponent<BulletsController>();
                if(direction==-1) c.SetLeftDirection();
                else c.SetRightDirection();
            }
            else if(disparo == true){
                var bulletPosition = transform.position + new Vector3(direction,0,0);
                var o = Instantiate(bulletP, bulletPosition, Quaternion.identity) as GameObject;
                var c = o.GetComponent<BulletsController>();
                if(direction==-1) c.SetLeftDirection();
                else c.SetRightDirection();
            }
    }
    void OnCollisionEnter2D(Collision2D other){
        cont=salto;

    } 
    private void ChangeAnimation(int a){
        animator.SetInteger("Estado", a);
    }
}
