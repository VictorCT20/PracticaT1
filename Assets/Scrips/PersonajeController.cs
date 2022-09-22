using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeController : MonoBehaviour
{
    public int velocity = 10, velSalto = 8, velCaida =5;
    public GameObject bullet;
    public AudioClip jumpClip;
    public AudioClip monedaClip;
    public AudioClip bulletClip;
    
    private GameManagerController gameManager;
    
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;
    Collider2D cl;
    AudioSource audioSource;

    // Start is called before the first frame update

    const int ANI_CORRER = 1;
    const int ANI_QUIETO = 0;
    const int ANI_SALTO = 2;
    const int ANI_MUERTO = 3;
    bool puedeSaltar = true, subir = false;
    bool muerto = false;
    Vector3 lastCheckpointPosition;
    int direction = 1;

    void Start()
    {
        gameManager = FindObjectOfType<GameManagerController>();
        Debug.Log("Iniciando script de player");
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        cl = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
        gameManager.LoadGame();
    }

    // Update is called once per frame
    void Update()
    {
        gameManager.GanarPuntos(0);
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
        else if(muerto == true){
            if(this.transform.localScale.x == 0.4f){
                this.transform.localScale += new Vector3(-0.1f, -0.1f, -0.1f);
            }
            else{
                ChangeAnimation(ANI_MUERTO);
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1){
                    Debug.Log($"Animation over");
                    this.transform.position = new Vector3(-6, 0, 0);
                }
            }
        }
        else if(subir== true && Input.GetKey(KeyCode.UpArrow)){
            rb.velocity = new Vector2(0, 2);
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
            audioSource.PlayOneShot(jumpClip);
        }
        if(Input.GetKeyDown("c")){
            var bulletPosition = transform.position + new Vector3(direction,0,0);
            var o = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject;
            var c = o.GetComponent<BulletController>();
            if(direction==-1) c.SetLeftDirection();
            else c.SetRightDirection();
            audioSource.PlayOneShot(bulletClip);
        }
    }
    void OnCollisionEnter2D(Collision2D other){
        puedeSaltar = true;
        subir = false;

        if(other.gameObject.tag == "Enemy"){
            muerto=true;
            Debug.Log("Estas muerto");

        }
        else muerto =false;
        if(other.gameObject.name == "DarkHole"){
            if(lastCheckpointPosition != null){
                transform.position = lastCheckpointPosition;
            }
        }
        if(other.gameObject.tag == "Oro"){
            gameManager.CogerMonedaOro();
            Destroy(other.gameObject);
            audioSource.PlayOneShot(monedaClip);
        }
        if(other.gameObject.tag == "Bronce"){
            gameManager.CogerMonedaBronce();
            Destroy(other.gameObject);
            audioSource.PlayOneShot(monedaClip);
        }
        if(other.gameObject.tag == "Plata"){
            gameManager.CogerMonedaPlata();
            Destroy(other.gameObject);
            audioSource.PlayOneShot(monedaClip);
        }

        
    }
    private void ChangeAnimation(int a){
        animator.SetInteger("Estado", a);
    }
    void OnTriggerEnter2D(Collider2D other){
        Debug.Log("trigger");
        lastCheckpointPosition = transform.position;
        if(other.gameObject.name =="Escalera"){
            subir = true;
        }
        if(other.gameObject.name =="Guardar"){
            gameManager.SaveGame();
        }
    
    }
}
