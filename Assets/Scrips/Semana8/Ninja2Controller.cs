using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Ninja2Controller : MonoBehaviour
{
    public int velocity = 0, velSalto = 5, salto = 2;
    public GameObject Kunai;

    [HideInInspector]
    public bool onLadder = false;
    public float climbSpeed = 3;
    public float exitHop = 3;
    private ManagerController gameManager;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;
    Collider2D cl;
    AudioSource audioSource;
    
    public TilemapCollider2D plataform;

    [HideInInspector]
    public bool isGrounded = true;

    [HideInInspector]
    public bool usingLadder = false;
    const int ANI_QUIETO = 0;
    const int ANI_CORRER = 1;
    const int ANI_SALTO = 2;
    const int ANI_ATACAR = 3;
    const int ANI_SALTO_ATACAR = 4;
    const int ANI_DISPARAR = 5;
    const int ANI_ESCALAR = 6;
    int direction = 1;
    int live = 3;
    int cont;
    Vector3 lastCheckpointPosition;
    private float timer = 0.0f;
    private float timer2 = 4.0f;
    public GameObject zombie;
    bool nu = true;
    void Start()
    {
        gameManager = FindObjectOfType<ManagerController>();
        Debug.Log("Iniciando script de player");
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        cl = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
        gameManager.MatarZombie(0);
    }

    // Update is called once per frame
    void Update()
    {
        crearZombie();
        rb.velocity = new Vector2(-velocity, rb.velocity.y);
        Movimiento();
        if(Input.GetKeyDown("x")){
            Disparar();
        }
    }
    void crearZombie(){
        if(nu){
            timer2 = Random.Range(3, 6);
            Debug.Log("Timer2 = " + timer2);
            nu = false;
        }
        
        timer += Time.deltaTime;
        //Debug.Log(timer);
        if(timer >= timer2){
            timer = 0.0f;
            var zombiePosition = transform.position + new Vector3(7,0,0);
            var o = Instantiate(zombie, zombiePosition, Quaternion.identity) as GameObject;
            var c = o.GetComponent<Zombie2Controller>();
            nu=true;
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
    public void Disparar(){
        ChangeAnimation(ANI_DISPARAR);
        var bulletPosition = transform.position + new Vector3(direction,0,0);
        var o = Instantiate(Kunai, bulletPosition, Quaternion.identity) as GameObject;
        var c = o.GetComponent<BulletsController>();
        var t = c.GetComponent<SpriteRenderer>();
        if(direction==-1){
            t.transform.Rotate(0, 0, 90); 
            c.SetLeftDirection();
        } 
        else {
            t.transform.Rotate(0, 0, -90); 
            c.SetRightDirection();
        }
    }
    void OnCollisionEnter2D(Collision2D other){
        cont=salto;
        ChangeAnimation(ANI_QUIETO);
        if(other.gameObject.name =="DarkHole")//para colisionar con el piso de fondo
        {
            if(lastCheckpointPosition != null)
                {
                    transform.position = lastCheckpointPosition;
                }
            
        } 
        if(other.gameObject.tag == "Oro"){
            gameManager.GanarMonedas();
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "Portal"){
            Debug.Log("Entra");
            SceneManager.LoadScene(ManagerController.Scena2);
        }
        if(other.gameObject.tag == "Enemy"){
            live = live-1;
            if(live == 0){
                Destroy(this.gameObject);
            }
        }
    } 
    void OnTriggerEnter2D(Collider2D other)//para reconocer el checkponit(transparente)
    {
        
        if(other.gameObject.tag =="Banner"){
            Debug.Log("Checkpoint");
            lastCheckpointPosition = transform.position;
            var x = transform.position.x;
            var y = transform.position.y;
            gameManager.GuardarPosicion(x,y);
            gameManager.SaveGame();
        }
    }
    private void ChangeAnimation(int a){
        animator.SetInteger("Estado", a);
    }
    private void OnTriggerStay2D(Collider2D other){
        if(other.CompareTag("escalera")){
            if(Input.GetAxisRaw("Vertical") != 0){
                rb.velocity = new Vector2(rb.velocity.x, Input.GetAxisRaw("Vertical") * climbSpeed);
                rb.gravityScale = 0;
                onLadder = true;
                plataform.enabled = false;
            }
            else if(Input.GetAxisRaw("Vertical") == 0 && onLadder){
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("escalera") && onLadder){
            rb.gravityScale = 1;
            onLadder = false;
            plataform.enabled = true;

            if(!isGrounded) 
                rb.velocity = new Vector2(rb.velocity.x, exitHop); 
        }
    }
}
