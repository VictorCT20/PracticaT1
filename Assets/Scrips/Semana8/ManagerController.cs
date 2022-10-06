using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ManagerController : MonoBehaviour
{
    public Text ZombiesText;
    public Text MonedasText;
    private int score = 0;
    private int monedas = 0;
    public const int Scena1 = 1;
    public const int Scena2 = 0;
    public float tempx;
    public float tempy;
    public GameObject portal;
    void Start()
    {
        
    }
    public void SaveGame()
    {
        var filePath = Application.persistentDataPath + "/guardar.dat";
        FileStream file;

        if(File.Exists(filePath))
            file = File.OpenWrite(filePath);
        else
            file = File.Create(filePath);

        GameData data = new GameData();
        data.Score = score;
        data.posX = tempx;
        data.posY = tempy;

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadGame()
    {
        var filePath = Application.persistentDataPath + "/guardar.dat";
        FileStream file;

        if(File.Exists(filePath))
            file = File.OpenRead(filePath);
        else{
            Debug.LogError("No see encontro archivo");
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        GameData data = (GameData) bf.Deserialize(file);
        file.Close();

        //usar datos guardados
        score = data.Score;
        tempx = data.posX;
        tempy = data.posY;

        GanarPuntos(0);
    }
    public void ReiniciarSave()
    {
        var filePath = Application.persistentDataPath + "/guardar.dat";
        FileStream file;

        if(File.Exists(filePath))
            file = File.OpenWrite(filePath);
        else
            file = File.Create(filePath);

        GameData data = new GameData();
        data.Score = 0;
        data.posX = -5f;
        data.posY = -2f;

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }

    public void GuardarPosicion(float x, float y)
    {
        tempx = x;
        tempy = y;
    }

    public void GanarPuntos(int puntos){
        score += puntos;
        MatarZombie(0);
    }
    public void GanarMonedas(){
        monedas += 1;
        MatarZombie(0);

        if(score>=5 && monedas ==10){
            var portalPosition = new Vector3(62,-4,0);
            var o = Instantiate(portal, portalPosition, Quaternion.identity) as GameObject;
        }
    }

    public void MatarZombie(int a){
        score = score + a;
        PrintInScreenZombie();
        PrintInScreenMonedas();

        if(score==5 && monedas >=10){
            var portalPosition = new Vector3(62,-4,0);
            var o = Instantiate(portal, portalPosition, Quaternion.identity) as GameObject;
        }


    }
    private void PrintInScreenZombie(){
        ZombiesText.text = "Zombies: " + score;
    }
    private void PrintInScreenMonedas(){
        MonedasText.text = "Monedas: " + monedas;
        
    }

}
