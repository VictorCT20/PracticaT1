using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameObjectController : MonoBehaviour
{
    public Text ZombiesText;
    public Text AtaqueText;
    private int score = 0;
    public int TipoAtaque = 1;
    private float timer = 0.0f;
    private float timer2 = 4.0f;
    public GameObject zombie;
    bool nu = true;
    void Start()
    {
        
    }
    void Update(){
        crearZombie();
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
            float po =  Random.Range(-2.0f, 18.0f);
            Debug.Log("po = " + po);
            var zombiePosition = new Vector3(po,-2,0);
            var o = Instantiate(zombie, zombiePosition, Quaternion.identity) as GameObject;
            var c = o.GetComponent<Zombie2Controller>();
            nu=true;
        }
    }

    public void MatarZobie(int a){
        score = score + a;
        PrintInScreenZombie();
        PrintInScreenAtaque();
    }
    private void PrintInScreenZombie(){
        ZombiesText.text = "Zombies: " + score;
    }
    private void PrintInScreenAtaque(){
        if(TipoAtaque == 1) AtaqueText.text = "Espada";
        else if(TipoAtaque == -1) AtaqueText.text = "Kunai";
        
    }
}
