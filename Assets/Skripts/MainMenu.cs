using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Play button action
    public void playButton(){
        SceneManager.LoadScene("4_Game");
        PlayerMovement.score=0;
        PlayerMovement.health=300;
        Config loadConfig=SaveResult.loadConfig();
        if (loadConfig!=null){
            Board.widthBoard=loadConfig.fieldSide;
            Board.timeraund=loadConfig.roundTime;
        }else{
            Board.widthBoard=10;
            Board.timeraund=15;
        }
    }
    //Instruction button action
    public void instructionButton(){
        SceneManager.LoadScene("2_Instruction");
    }
    //rating button action
    public void ratingButton(){
        SceneManager.LoadScene("3_Rating");
    }
    //menu button action
    public void menuButton(){
        SceneManager.LoadScene("1_Menu");
    }
    //options button action
    public void optionsButton(){
        SceneManager.LoadScene("6_Options");
    }
    //quit button action
    public void quitButton(){
        Application.Quit();
    }
}
