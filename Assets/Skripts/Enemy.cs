using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    static public float distanceTime=0.2f;//projectile lifetime
    public int health=300;//tank health
    public Transform firePoint;// projectile launch site
    public GameObject bulletPrefab;//projectile
    public GameObject self;//the object itself
    public float bulletForce=20f;//shot speed
    public float timer = 0.0f;//temporary variable
    private float timerDeltaShoot = 0.0f;//shot timer
    public float timeout=0.25f;// reaction time for next action
    public float moveSpeed;//tank speed
    public Rigidbody2D rb;//variable for assigning tank
    Vector2 movement;//temporary variable
    private float sizeCell=2.048f;//game cell size
    public GameObject findEnemy;//enemy game object
    public GameObject field;// temporary variable for cell
    private string target;//target name
    private float timerShoot;//one shot time
    void Start()
    {   //loading tank parameters from a file
        Config loadConfig=SaveResult.loadConfig();
        if (loadConfig!=null){
            moveSpeed=loadConfig.movementSpeed;
            bulletForce=loadConfig.projectileSpeed;
            timerShoot=1.0f/loadConfig.nrShots;
        }else{
            moveSpeed=1.5f;
            bulletForce=20f;
            timerShoot=1/2.0f;
            }
    }
    // Update is called once per frame
    void Update()
    {   //shoot at a given rate 
        timerDeltaShoot=timerDeltaShoot+Time.deltaTime;
        if (timerDeltaShoot>timerShoot){
            timerDeltaShoot=timerDeltaShoot-timerShoot;  
            shoot();
        }
        //next action
        timer=timer+Time.deltaTime;
        if (timer>timeout){
            timer=timer-timeout;              
            //on the red zone subtract hp
            if (whereIAm()==2){
                health-=1;     
            }
            target=scanEnemy(self.name); //new target for shots
            //not on a green cell, looking for a green one
            if (whereIAm()!=0){
                moveDirection(findRandomGreenCell());
            }    
            // randomly move if the green cell and the enemy are far away
            if (whereIAm()==0&& distatceToEnemy(self.name)>3*sizeCell){
                moveDirection(moveRandom());
            }   
            // run away from the enemy if on green cells
            if (whereIAm()==0&& distatceToEnemy(self.name)<3*sizeCell){
                moveDirection(runAway());
            }
        }
        // tank destroyed
        if (health<=0){
            Destroy(self);
            EnemySpawn.nummberEnemy--;//reduce enemy counter
            PlayerMovement.score=PlayerMovement.score+50;//increase player points
        } 
        turnToTarget(); //turn to nearest target  
    }
    //returns Vector2 in the direction away from target
    public Vector2 runAway(){
        if (target !=null){
            Vector2 move =GameObject.Find(target).GetComponent<Rigidbody2D>().position; 
            movement=new Vector2(rb.position.x-move.x, rb.position.y-move.y);  
            return movement;
        }
    return rb.position;    
    }
    //returns Vector2 in the direction towards the goal
    public Vector2 runToPoint(){
        if (target !=null){
            Vector2 move =GameObject.Find(target).GetComponent<Rigidbody2D>().position; 
            movement=new Vector2(-rb.position.x+move.x, -rb.position.y+move.y); 
            //Debug.Log("run to "+move.x + " y "+move.y+" "+self.name);
            return movement;
        }
    return rb.position;    
    }
    //returns random Vector2 
    public Vector2 moveRandom(){
        return movement=new Vector2(Random.Range(-Board.widthBoard, Board.widthBoard+1),Random.Range(-Board.widthBoard, Board.widthBoard+1));
    }
    //movement - direction of movement
    void moveDirection(Vector2 movement){
        double koef= (double)Mathf.Sqrt(   (movement.x*movement.x+movement.y*movement.y)/ (moveSpeed*moveSpeed)    );// 
        if (koef!=0){
            movement.x= (float) (movement.x/koef);
            movement.y= (float) (movement.y/koef);
        }
        if (movement.x!=float.NaN && movement.y !=float.NaN){
                    rb.velocity=movement;
        }
    }
    //turn towards the target
    void turnToTarget(){
        if (target!=null && GameObject.Find(target)!=null){
            findEnemy = GameObject.Find(target);
            Vector2 lookDir=findEnemy.GetComponent<Rigidbody2D>().position- self.GetComponent<Rigidbody2D>().position; 
            float angle =Mathf.Atan2(lookDir.y,lookDir.x)* Mathf.Rad2Deg-90f;
            rb.rotation=angle;
        }
    }
    //fire a shot
    void shoot(){
        bulletPrefab.name="bullet_"+self.name;
        GameObject bullet=Instantiate(bulletPrefab,firePoint.position,firePoint.rotation);
        Rigidbody2D rb=bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        Destroy(bullet,distanceTime);// projectile range
    }
    //
    void OnCollisionEnter2D(Collision2D collInfo) {
            if (collInfo.collider.name.Substring(0, 5)=="Enemy" ||collInfo.collider.name.Substring(0, 5)=="Playe" ){
                health-=3;//Collision with Player or other Enemy -3 HP
            }
            if (collInfo.collider.name.Substring(0, 5)=="bulle" 
                &&collInfo.collider.name.Substring(0, 14)!="bullet_"+self.name){
                    health-=15;//shot hit us
                    if (collInfo.collider.name.Substring(0, 8)=="bullet_P"){
                        PlayerMovement.score=PlayerMovement.score+15;
                    }
            }
    }
    //returns the danger value of the cell on which the tank is standing
    int whereIAm(){
    int x =   (int)Mathf.Round(rb.position.x/sizeCell); 
    int y =   (int)Mathf.Round(rb.position.y/sizeCell); 
        if(x>=0&&y>=0 &&x<=Board.widthBoard-1 && y<=Board.widthBoard-1){
            if (  Board.logicArrayBoard[x,y]==0){
                return 0;//GreenCell
            }else if (Board.logicArrayBoard[x,y]==1)
                return 1;//"YellowCell
                    else {  
                    return 2; //RedCell 
                }
        }
        return 2;//RedCell 
    }
    /*    returns the name of the nearest enemy
    myName - searcher name
    */
    public string scanEnemy(string myName){
        float min_dist=100000;
        string closeEnemy=null;
        string enemyName="";
        Vector2 myPosition =rb.position;
        for (int i=-1;i<EnemySpawn.nummberEnemy;i++){
            if (i==-1){
                enemyName="Player";
            }else{
                enemyName="Enemy_";
                enemyName=enemyName+i;
            }
                if (myName!=enemyName &&  GameObject.Find(enemyName)!=null){
                    findEnemy = GameObject.Find(enemyName);
                    float dist= Vector2.Distance(myPosition,findEnemy.GetComponent<Rigidbody2D>().position);
                    if (min_dist>dist){
                        min_dist=dist;
                        closeEnemy=enemyName;
                    } 
                }
        } 
        return closeEnemy;
    }
    /*returns the distance to the nearest enemies
    myName - searcher name
    */
    public float distatceToEnemy(string myName){
        float min_dist=100000;
        string closeEnemy=null;
        string enemyName="";
        Vector2 myPosition =rb.position;
        for (int i=-1;i<EnemySpawn.nummberEnemy;i++){
            if (i==-1){
                enemyName="Player";
            }else{
                enemyName="Enemy_";
                enemyName=enemyName+i;
            }
                if (myName!=enemyName &&  GameObject.Find(enemyName)!=null){
                    findEnemy = GameObject.Find(enemyName);
                    float dist= Vector2.Distance(myPosition,findEnemy.GetComponent<Rigidbody2D>().position);
                    if (min_dist>dist){
                        min_dist=dist;
                        closeEnemy=enemyName;
                    } 
                }
        } 
        return min_dist;
    }
    // returns Vector2 to the safe cell
    public Vector2 findRandomGreenCell(){
        if (Board.nummberGreenCell>0){
            int cell=Random.Range(0,Board.nummberGreenCell+1); 
            int count =0;
            for (int i=0; i< Board.widthBoard;i++){
                for (int j=0; j< Board.widthBoard;j++){
                    if (Board.logicArrayBoard[i,j]==0){
                        if(count==cell){
                            field=GameObject.Find(i+"_"+j);
                            Vector2 move =(Vector2)field.transform.position;
                            movement=new Vector2(-rb.position.x+move.x, -rb.position.y+move.y);
                            return movement;
                        }
                        count++; 
                    }
                }
            }                   
        }
        return new Vector2(0,0);
    }
    
}
