using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Text scoreText;
    public int purplescore = 0;
    public int slimescore=0;
    // even before starting the Start() method .. we'll set an instance 
    private void Awake(){
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {   
        slimescore=purplescore;
        scoreText.text = "Slime-Score:"+slimescore.ToString();
    }
    public void AddPurplePoint(){
        purplescore += 1;
        scoreText.text = "Slime-Score:"+purplescore.ToString();
    }
}
