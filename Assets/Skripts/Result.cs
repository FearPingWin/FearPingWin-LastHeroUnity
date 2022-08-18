using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Result : MonoBehaviour
{
    public Text score;//player score text
    public Text place;//player place text
    public GameObject input;//field for entering a name with all objects
    public Text inputText;//field for entering a name
    public string nickname; //player name variable
    void Start()
    {
        //SaveResult.deleteFile("/topresults.lh");
        input.SetActive(false);//hide name entry
        // coefficient for points, if different from the base   
        Config loadConfig=SaveResult.loadConfig();
        if (loadConfig!=null){
            PlayerMovement.score=(int)(PlayerMovement.score*loadConfig.movementSpeed/1.5f*
            loadConfig.projectileSpeed/20.0f*loadConfig.nrShots/2.0);
        }
        if (PlayerMovement.score>0){
            score.text=PlayerMovement.score.ToString();
        }
        place.text=(EnemySpawn.nummberEnemy+1).ToString()+" of "+(EnemySpawn.startNrEnemy+1).ToString();
        List<ResultData> dataload=SaveResult.loadResults();
        if (dataload==null){
            input.SetActive(true);
        }else{
            // sort list
            if (dataload.Count>1){
                dataload.Sort(delegate(ResultData x, ResultData y) {
                return y.score.CompareTo(x.score);   });
            }  
            //save only the top 10 results  
            if (dataload.Count<10){
                input.SetActive(true);
            }
            else{
                if (PlayerMovement.score>dataload[9].score){
                    input.SetActive(true);
                    dataload.RemoveAt(9);
                    SaveResult.saveResult(dataload);
                }
            }
        }

    }
    //save result and player name to file
    public void enterButten(){
        
        List<ResultData> datasave=SaveResult.loadResults();
        if (datasave==null){
            nickname =inputText.text;
            List<ResultData> datanew=new List<ResultData>();
            ResultData data=new ResultData(nickname,PlayerMovement.score,DateTime.Now.ToString());
            datanew.Add(data);
            SaveResult.saveResult(datanew);
            input.SetActive(false);
        }else{
            nickname =inputText.text;
            ResultData data=new ResultData(nickname,PlayerMovement.score,DateTime.Now.ToString());
            datasave.Add(data);
            SaveResult.saveResult(datasave);
            input.SetActive(false);
        }

    }
}
