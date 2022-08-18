using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;

public class Board : MonoBehaviour
{
    public Transform parantBackground;// parent object for the board
    public GameObject cell_green;//safe cell
    public GameObject cell_yellow;//warning cell
    public GameObject cell_red;//dangerous cell
    public GameObject border;//barrier cell
    public GameObject cellForReplace;//temporary variable
    static public float timer = 0.0f;// current round duration
    static public int timeraund;// max round duration
    static public int[,] logicArrayBoard;
    public static int  widthBoard;//number of remaining safe cells
    static public int nummberGreenCell;// logic board
    int round=0;// round counter
    //creates a logic board
    public void  board (int n){
        nummberGreenCell=n*n;
		logicArrayBoard=new int [n,n];
		for (int i=0;i<n;i++) {
			for (int j=0;j<n;j++) {
				logicArrayBoard[i,j]=0;
			}
		}
    } 
    void Start()
    {   
        //creating a game board of a given width      
        parantBackground.position=new Vector2 (0,0);
        var cellsize = cell_green.GetComponent<SpriteRenderer>().bounds.size;
        for (int i = -1; i < widthBoard+1; i++) {
            for (int j=-1;j<widthBoard+1;j++){
                if (i==-1||i==widthBoard||j==-1 || j==widthBoard){
                    var position = new Vector3(i * (cellsize.x),j * (cellsize.y) , 0);
                    var clone=    Instantiate(border,position, Quaternion.identity,parantBackground);
                    clone.name="border"+(i+"_"+j);//cell name
                }else{
                    var position = new Vector3(i * (cellsize.x),j * (cellsize.y) , 0);
                    var clone=    Instantiate(cell_green,position, Quaternion.identity,parantBackground);
                    clone.name=(i+"_"+j);
                }
            }
        }
        board (widthBoard);
    }
    /*replacing a given cell with another
    x - x-coordinate
    y- y-coordinate
    cellTyp-  substituted cell (green,yellow,red)
    */
    public void replaceCell (int x, int y, GameObject cellTyp){
        string cellName=x +"_"+y; 
        cellForReplace = GameObject.Find(cellName); 
        cellForReplace.SetActive(false);  
        var cellsize = cell_green.GetComponent<SpriteRenderer>().bounds.size;
        var position = new Vector3(cellForReplace.transform.position.x,cellForReplace.transform.position.y, 0);
        var clone= Instantiate(cellTyp,position, Quaternion.identity,parantBackground);
        if (cellTyp.Equals(cell_yellow)){
            logicArrayBoard[x,y]=1;
            nummberGreenCell--;
        }
        if (cellTyp.Equals(cell_red)){
            logicArrayBoard[x,y]=2;
        } 
        clone.name=cellForReplace.name;
        Destroy(cellForReplace);
    }
    //selection and distribution of yellow cells
    public void randomCellOf(){
        int randomX,randomY;
        if (round<widthBoard/4){
            int a=0;
            randomX = Random.Range(0, widthBoard); 
            randomY = Random.Range(0, widthBoard); 
            while (a==0){
                if (randomX==0||randomX==widthBoard-1){
                    if (logicArrayBoard[randomX,randomY]==0){
                        a=1;
                        break;
                    }
                }
                if (randomY==0||randomY==widthBoard-1){
                    if (logicArrayBoard[randomX,randomY]==0){
                        a=1;
                        break;
                    }
                }
                randomX = Random.Range(0, widthBoard); 
                randomY = Random.Range(0, widthBoard);
            }   
            replaceCell(randomX,randomY,cell_yellow);
            selectNeighboringCells(widthBoard/2,randomX,randomY);
            round++;
        }
        else{
            randomX = Random.Range(0, widthBoard); 
            randomY = Random.Range(0, widthBoard);
            selectNeighboringCells(widthBoard/2,randomX,randomY);   
        }  
    }
    /*lava spread
    nummber - number of cells for spreading yellow cells
    x - x-coordinate initial cell
    y - y-coordinate initial cell
    */
    public void selectNeighboringCells(int nummber, int x, int y){
        if (nummber>0&&nummberGreenCell>0){
            int x1=x,y1=y;
            int cell=Random.Range(1,5);// one of 4 
            switch(cell){
                case 1: if (x-1<0){ x1=x+1; } else x1=x-1;
                    y1=y; 
                    break;  
                case 2: if (y+1>widthBoard-1){ y1=y-1;} else  y1=y+1;
                    x1=x;  
                    break;
                case 3: if (x+1>widthBoard-1){x1=x-1;} else x1=x+1;
                    y1=y;
                    break;
                case 4: if (y-1<0){y1=y+1;}else y1=y-1;  
                    x1=x;
                    break;                                        
            }
            if (logicArrayBoard[x1,y1]==0){
                nummber--;
                replaceCell(x1, y1,cell_yellow);
                selectNeighboringCells(nummber, x1, y1);
            }else{
                selectNeighboringCells(nummber, x1, y1);    
            }
        }   
    }
    //replacing all yellow cells with red ones
    public void replaceCellYellow(){
        for (int i=0;i<widthBoard;i++){
            for (int j=0; j<widthBoard;j++){
                if (logicArrayBoard[i,j]==1){
                    replaceCell(i,j,cell_red);     
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {   
        timer=timer+Time.deltaTime; //round timer
        if (timer>timeraund){
            timer=timer-timeraund;  //zeroing round timer 
            replaceCellYellow();    
            if (nummberGreenCell>0){
                randomCellOf();
                PlayerMovement.score=PlayerMovement.score+10;
            } 
        }
    }
}
