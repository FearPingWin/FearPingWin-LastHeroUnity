using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public class Options : MonoBehaviour
{
    public Text fieldSide;//game board width
    public Text roundTime;//round time
    public Text movementSpeed;//enemy tank speed
    public Text nrShots;//the number of shots per second of an enemy tank
    public Text projectileSpeed;//enemy tank shot speed
    //increase current board size by 1
    public void plusBTfieldSide(){
        int a=int.Parse(fieldSide.text); 
        if (a<25)
            a++;
        fieldSide.text=a.ToString();
    }
    //decrease current board size by 1
    public void minusBTfieldSide(){
        int a=int.Parse(fieldSide.text); 
        if (a>5)
            a--;
        fieldSide.text=a.ToString();
    }
    //increase current round time by 5 seconds
    public void plusBTroundTime(){
        int a=int.Parse(roundTime.text); 
        if (a<90)
            a=a+5;
        roundTime.text=a.ToString();
    }
    //decrease current round time by 5 seconds
    public void minusBTroundTime(){
        int a=int.Parse(roundTime.text); 
        if (a>5)
            a=a-5;
        roundTime.text=a.ToString();
    }
    //increase current enemy tank speed by 0,25
    public void plusBTmovementSpeed(){     
        float a=float.Parse(movementSpeed.text,  CultureInfo.InvariantCulture) ;
        if (a<10){
            a=a+0.25f;
            string str= a.ToString().Replace (",", ".");
            movementSpeed.text=str;
        }
    }
    //decrease current enemy tank speed by 0,25
    public void minusBTmovementSpeed(){
        float a=float.Parse(movementSpeed.text,  CultureInfo.InvariantCulture) ;
        if (a>0.5){
            a=a-0.25f;
            string str= a.ToString().Replace (",", ".");
            movementSpeed.text=str;
        }
    } 
    //increase current the number of shots per second of an enemy tank by 1
    public void plusBTnrShots(){
        int a=int.Parse(nrShots.text); 
        if (a<10)
            a++;
        nrShots.text=a.ToString();
    }
    //decrease current the number of shots per second of an enemy tank by 1
    public void minusBTnrShots(){
        int a=int.Parse(nrShots.text); 
        if (a>1)
            a--;
        nrShots.text=a.ToString();
    } 
    //increase current enemy tank shot speed by 5
    public void plusBTprojectileSpeed(){
        int a=int.Parse(projectileSpeed.text); 
        if (a<40)
            a=a+5;
        projectileSpeed.text=a.ToString();
    }
    //decrease current enemy tank shot speed by 5
    public void minusBTprojectileSpeed(){
        int a=int.Parse(projectileSpeed.text); 
        if (a>15)
            a=a-5;
        projectileSpeed.text=a.ToString();
    } 
    //restore default settings
    public void defaultBT(){
        SaveResult.saveConfig(10, 15, 1.5f,2,20);
        fieldSide.text=10.ToString();
        roundTime.text=15.ToString();
        movementSpeed.text=1.5f.ToString().Replace (",", ".");
        nrShots.text=2.ToString();
        projectileSpeed.text=20.ToString();

    }
    //save user settings
    public void saveBT(){
        SaveResult.saveConfig(
            int.Parse(fieldSide.text), 
            int.Parse(roundTime.text), 
            float.Parse(movementSpeed.text,  CultureInfo.InvariantCulture),
            int.Parse(nrShots.text),
            int.Parse(projectileSpeed.text)
            );
    }
    void Start()
    {   //load custom settings
        Config loadConfig=SaveResult.loadConfig();  
        if (loadConfig!=null){
            fieldSide.text=loadConfig.fieldSide.ToString();
            roundTime.text=loadConfig.roundTime.ToString();
            movementSpeed.text=loadConfig.movementSpeed.ToString().Replace (",", ".");
            nrShots.text=loadConfig.nrShots.ToString();
            projectileSpeed.text=loadConfig.projectileSpeed.ToString();
        }
    }

}
