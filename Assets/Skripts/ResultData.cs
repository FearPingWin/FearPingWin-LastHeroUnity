using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResultData 
{
    public string nickname;//Player name
    public int score;//Player score
    public string date;//date at the time of the game
    //constructor
    public ResultData(string nickname, int score, string date){
        this.nickname=nickname;
        this.date=date;
        this.score=score;
    }
}
