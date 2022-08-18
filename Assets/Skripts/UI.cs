using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Text timerText;//timer counter
    public Text nrEnemyText;//current number of enemies
    public Text hpText;//current player health value
    public Text scoreText;//current player score
    public float timer = 0.0f;//timer variable
    public int timeout=1;//timer update rate in seconds
    bool pauseGame=false;//pause
    public GameObject pauseText;//pause text
    void Start()
    {   
        pauseText.SetActive(false);//hide pause text
        //initialization of the game parameters on the screen
        timerText.text=Board.timeraund.ToString(); 
        nrEnemyText.text=EnemySpawn.nummberEnemy.ToString();
        hpText.text=PlayerMovement.health.ToString();
        scoreText.text=PlayerMovement.score.ToString();
    }

    // Update is called once per frame
    void Update()
    {   
         // pause
        if (Input.GetKeyDown(KeyCode. Escape)){
            if (pauseGame==false){
                pause();
            }
            else{
                resume();
            }
        }
        //timer count
        timer=timer+Time.deltaTime;
        if (timer>timeout){
            timer=timer-timeout;  
            timerText.text=(Board.timeraund-(int)Mathf.Round(Board.timer)).ToString();//round countdown
            }
        //updating interface values
        nrEnemyText.text=EnemySpawn.nummberEnemy.ToString();
        hpText.text=PlayerMovement.health.ToString();
        scoreText.text=PlayerMovement.score.ToString();
  
    }
        //enable pause mode
        public void pause(){
            Time.timeScale=0.0f;
            pauseText.SetActive(true);
            pauseGame=true;
        }
        //disable pause mode
        public void resume(){
            Time.timeScale=1.0f;
            pauseText.SetActive(false);
            pauseGame=false;
        }
}
