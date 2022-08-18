using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnemySpawn : MonoBehaviour
{
    public Transform  parentEnemy;//parent object for enemies
    public GameObject enemyTank;//enemy object
    static public int  nummberEnemy;//number of enemies
    static public int startNrEnemy;//initial number of enemies

    // Start is called before the first frame update
    void Start()
    {
        //create enemies depending on the length of the game board
        nummberEnemy=(int)(Board.widthBoard*Board.widthBoard)/9;
        startNrEnemy=nummberEnemy;
        for(int i=0;i<nummberEnemy;i++){
            var position = new Vector2(Random.Range(1, Board.widthBoard)*2.048f,Random.Range(1, Board.widthBoard)*2.048f);
            var Enemy=    Instantiate(enemyTank,position, Quaternion.identity, parentEnemy);
            Enemy.name=("Enemy_"+i); 
        }
   
    }

    // Update is called once per frame
    void Update()
    {
        //ran out of enemies load scene results
        if (nummberEnemy==0){
            SceneManager.LoadScene("5_Result");   
        }
    }
}
