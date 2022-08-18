using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Rating : MonoBehaviour
{
    public Transform entryContainer;//table container
    public Transform template;//result line template
    private List<ResultData> listResult=new List<ResultData>();//variable to read results from file

    public void Awake() {   
        template.gameObject.SetActive(false);//hide template
        float templateHeight = 60f;//spacing between rows in a table
        List<ResultData> dataload=SaveResult.loadResults();//loading results
        //sort results
        if (dataload!=null){
            dataload.Sort(delegate(ResultData x, ResultData y) {
                return y.score.CompareTo(x.score);
            });
        

        //creation of 10 lines according to a template with filling in data from a file    
        for (int i = 0; i < dataload.Count; i++) {
            Transform entryTransform = Instantiate(template, entryContainer);
            entryTransform.name=entryTransform.name+"_"+i;
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
            entryRectTransform.gameObject.SetActive(true);
            int rank = i + 1;
            string rankString;
            switch (rank) {
                default:
                    rankString = rank + "TH"; break;
                case 1: rankString = "1ST"; break;
                case 2: rankString = "2ND"; break;
                case 3: rankString = "3RD"; break;
            }
            entryTransform.Find("TP").GetComponent<Text>().text = rankString;
            entryTransform.Find("TN").GetComponent<Text>().text = dataload[i].nickname;
            entryTransform.Find("TS").GetComponent<Text>().text = dataload[i].score.ToString();
            entryTransform.Find("TD").GetComponent<Text>().text = dataload[i].date;
        }
        }
    }
}
