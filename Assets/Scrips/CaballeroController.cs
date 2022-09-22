using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaballeroController : MonoBehaviour
{
    //public int velocityCam = 3, velocityCorr = 7, velSalto = 3;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;
    Collider2D cl;
    //public GameObject bullet;
    private GameManagerController gameManager;
    // Start is called before the first frame update
    /*const int ANI_QUIETO = 0;
    const int ANI_CORRER = 1;
    const int ANI_CAMINAR = 2;
    const int ANI_SALTO = 3;
    const int ANI_ATAQUE = 4;
    const int ANI_MUERTE = 5;
    bool puedeSaltar = true, puedeSaltar2 = true;
    Vector3 lastCheckpointPosition;
    bool check = true, muerto=false;*/
    void Start()
    {
        Debug.Log("Iniciando script de player");
        gameManager = FindObjectOfType<GameManagerController>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        cl = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(muerto==false){
            Debug.Log("Camina");
            rb.velocity = new Vector2(velocityCam, rb.velocity.y);
            ChangeAnimation(ANI_CAMINAR);
        }else ChangeAnimation(ANI_MUERTE);
        
        if(Input.GetKeyDown("c")){
            if(gameManager.bullet>0){
                var bulletPosition = transform.position + new Vector3(1,0,0);
                var o = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject;
                var c = o.GetComponent<BulletController>();
                gameManager.GastarBala();
            }
            else Debug.Log("No tienes balas");
            
        }
        if(Input.GetKeyDown(KeyCode.Space) && puedeSaltar2==true){
            //rb.velocity = new Vector2(rb.velocity.x, velSalto);
            if(puedeSaltar==false){
                rb.AddForce(new Vector2(0, velSalto), ForceMode2D.Impulse);
                ChangeAnimation(ANI_SALTO);
                puedeSaltar2=false;
            }else{
                rb.AddForce(new Vector2(0, velSalto), ForceMode2D.Impulse);
                ChangeAnimation(ANI_SALTO);
                puedeSaltar = false; 
            } 
               
        }
        */
    }
    /*
    void OnCollisionEnter2D(Collision2D other){
        puedeSaltar = true;
        puedeSaltar2 = true;
        if(other.gameObject.tag == "Enemy"){
            muerto=true;
            Debug.Log("Estas muerto");

        }
        if(other.gameObject.name =="DarkHole")//para colisionar con el piso de fondo
        {
            if(lastCheckpointPosition != null)
                {
                    transform.position = lastCheckpointPosition;
                }
            
        } 
    }
    void OnTriggerEnter2D(Collider2D other)//para reconocer el checkponit(transparente)
    {
        if(other.gameObject.name == "SignArrow" && check == true){
            Debug.Log("Trigger");//aplicar la pocion isTrigger en la configuracion
            lastCheckpointPosition = transform.position;
        }
        else if(other.gameObject.name == "Sign"){
            Debug.Log("Trigger");//aplicar la pocion isTrigger en la configuracion
            lastCheckpointPosition = transform.position;
            check=false;
        }
        
    }
    private void ChangeAnimation(int a){
        animator.SetInteger("Estado", a);
    }
    */

}
