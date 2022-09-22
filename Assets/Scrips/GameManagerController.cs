using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary; 
public class GameManagerController : MonoBehaviour
{
    public Text PuntosText;
    public Text OroText;
    public Text PlataText;
    public Text BronceText;
    public int monedaOro = 0;
    public int monedaPlata = 0;
    public int monedaBronce = 0;
    public int score = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SaveGame(){
        var filePath = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if(File.Exists(filePath))
            file = File.OpenWrite(filePath);
        else 
            file = File.Create(filePath);

        GameData data = new GameData{
            MonedaOro = monedaOro,
            MonedaPlata = monedaPlata,
            MonedaBronce = monedaBronce,
            Score = score
        };
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        Debug.Log("Data guardada");
        file.Close();
    }
    public void LoadGame(){
        var filePath = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if(File.Exists(filePath)){
            file = File.OpenRead(filePath);
        }   
        else {
            Debug.LogError("No se encontró archivo");
            return;
        }
        
        BinaryFormatter bf = new BinaryFormatter();
        GameData data = (GameData) bf.Deserialize(file);
        file.Close();
        monedaOro = data.MonedaOro;
        monedaPlata = data.MonedaPlata;
        monedaBronce = data.MonedaBronce;
        score = data.Score;
        Debug.Log(filePath);
        PrintPointsInScreen();
        PrintOroInScreen();
        PrintPlataInScreen();
        PrintBronceInScreen();
    }
    public void CogerMonedaOro(){
        monedaOro += 1;
    }
    public void CogerMonedaPlata(){
        monedaPlata += 1;
    }
    public void CogerMonedaBronce(){
        monedaBronce += 1;
    }
    public void GanarPuntos(int puntos){
        score += puntos;
        PrintPointsInScreen();
        PrintOroInScreen();
        PrintPlataInScreen();
        PrintBronceInScreen();

    }
    private void PrintOroInScreen(){
        OroText.text = "Oro: " + monedaOro;
    }
    private void PrintPlataInScreen(){
        PlataText.text = "Plata: " + monedaPlata;
    }
    private void PrintBronceInScreen(){
        BronceText.text = "Bronce: " + monedaBronce;
    }
    private void PrintPointsInScreen(){
        PuntosText.text = "Puntos: " + score;
    }
}
