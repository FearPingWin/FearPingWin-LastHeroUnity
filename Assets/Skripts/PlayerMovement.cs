using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour

{
    static public int score=0;//player points
    static public int health=300;//player health
    public float moveSpeed=1.5f;//player tank speed
    public Rigidbody2D rb;//the physical body of the tank
    public Camera cam;//player tracking camera
    Vector2 movement;//motion variable
    Vector2 mousePos;//mouse position on the screen
    private float sizeCell=2.048f;//game cell side size
    public float timer = 0.0f;//variable timer
    public float timeout=0.5f;//timer value in seconds
    //checking if the player is on the red cell
    void whereIAm(){
    int x =   (int)Mathf.Round(rb.position.x/sizeCell); 
    int y =   (int)Mathf.Round(rb.position.y/sizeCell); 
        if(x>=0&&y>=0 &&x<=Board.widthBoard-1 && y<=Board.widthBoard-1){
            if (Board.logicArrayBoard[x,y]==2){
                    health-=2;//RedCell HP -2
            }    
        }
    }
    void Update()
    {   //mouse movement tracking    
        movement.x=Input.GetAxisRaw("Horizontal") ;
        movement.y=Input.GetAxisRaw("Vertical") ;
        mousePos=cam.ScreenToWorldPoint(Input.mousePosition) ;
        //player died
        if (health<=0){
           SceneManager.LoadScene("5_Result");
        }
        timer=timer+Time.deltaTime;
        if (timer>timeout){
            timer=timer-timeout;  
            whereIAm();   
        }
    }
    void FixedUpdate() {
        //rotation of the tank in the direction of the mouse
        rb.MovePosition (rb.position+movement* moveSpeed*Time.fixedDeltaTime); 
        Vector2 lookDir=mousePos-rb.position;
        float angle  = Mathf.Atan2(lookDir.y,lookDir.x)* Mathf.Rad2Deg-90f;
        rb.rotation=angle;
    }
    //collision with an enemy or projectile
    void OnCollisionEnter2D(Collision2D collInfo) {
        if (collInfo.collider.name.Substring(0, 5)=="Enemy"){
            health-=3;//Collision -3 HP
        }
        if (collInfo.collider.name.Substring(0, 5)=="bulle"
            &&collInfo.collider.name.Substring(0, 13)!="bullet_Player"){
                health-=15;//shoot -15 HP
        }    
    }
} 
