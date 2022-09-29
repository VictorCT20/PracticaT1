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
    private float timer2 = 2.0f;
    private GameObject zombie;
    void Start()
    {
        
    }
    void Update(){
        timer += Time.deltaTime;
        Debug.Log(timer);
        if(timer ==2.0f){
            timer = 0.0f;
            var zombiePosition = new Vector3(-2,-2,0);
            var o = Instantiate(zombie, zombiePosition, Quaternion.identity) as GameObject;
            var c = o.GetComponent<Zombie2Controller>();
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
