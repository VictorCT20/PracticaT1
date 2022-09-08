using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class GameManagerController : MonoBehaviour
{
    public Text bulletText;
    public int bullet;
    // Start is called before the first frame update
    void Start()
    {
        bullet = 5;
    }
    public int Bullet() {
        return bullet;
    }
    public void GastarBala(){
        bullet -= 1;
        PrintBulletInScreen();
    }
    private void PrintBulletInScreen(){
        bulletText.text = "Balas: " + bullet + "/5";
    }
}
