using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Config 
{   public int fieldSide;// game board width
    public int roundTime;// round time in seconds
    public float movementSpeed;//enemy tank speed
    public int nrShots;//the number of shots per second of an enemy tank
    public float projectileSpeed;//enemy tank shot speed
    //constructor
    public Config(int fieldSide, int roundTime, float movementSpeed,int nrShots,float projectileSpeed){
        this.fieldSide=fieldSide;
        this.roundTime=roundTime;
        this.movementSpeed=movementSpeed;
        this.nrShots=nrShots;
        this.projectileSpeed=projectileSpeed;
    }
}
