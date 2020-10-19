
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_Enemy_Controller : MonoBehaviour
{
    
    public bool is_Inactive;
    public Material material_player;
    public Material material_enemy;
    public Material material_inactive;
    public GameObject fence_Player;
    public GameObject fence_Eneny;
    public GameObject gate_player;
    public GameObject gate_enemy;
    private MeshRenderer mesh;
    private float time_Inactive;
    private float time_spawn;
    public bool is_attacker;
    public bool is_player;
    public bool is_hold_ball;
    private bool  is_Spawn_Time;
    public GameObject land;
    public GameObject ball;
    //public GameObject parent_ball;
    //private Animator ball_animator;
    private float normal_speed_attacker;
    private float normal_speed_defender;
    private float Carrying_Speed;
    private GameObject highlight;
    private GameObject range_detect;
    public GameObject parent_player, parent_enemy;
    public bool is_detect = false;
    public GameObject attacker_is_detected;
    public Vector3 pos_spawn;
    // Start is called before the first frame update
    void Awake()
    {
        is_Inactive = true;
        is_Spawn_Time = true;
        is_hold_ball = false;
    }
    void Start()
    {
        highlight = this.transform.GetChild(0).gameObject;
        range_detect = this.transform.GetChild(1).gameObject;
        highlight.SetActive(false);
        range_detect.SetActive(false);
        mesh = gameObject.GetComponent<MeshRenderer>();
        mesh.material = material_inactive; // in spawn time player in grey
        land = GameObject.Find("land");
        ball = land.GetComponent<land_Controller>().ball;
        //parent_ball = GameObject.Find("Ball_parent");
        //ball_animator = ball.gameObject.GetComponent<Animator>();
        fence_Eneny = GameObject.Find("fence_Enemy");
        fence_Player = GameObject.Find("fence_Player");
        gate_enemy = GameObject.Find("gate_Enemy");
        gate_player = GameObject.Find("gate_Player");
        parent_player = GameObject.Find("Player_parent");
        parent_enemy = GameObject.Find("Enemy_parent");
        is_detect = false;
        ///////////////////////////////
        time_spawn = 0.5f;
        normal_speed_attacker = 1.5f;
        normal_speed_defender = 1.0f;
        Carrying_Speed = 0.75f;
        is_attacker = land.gameObject.GetComponent<land_Controller>().is_player_attacker;
        if(is_attacker)
        {
            time_Inactive = 2.5f;
        }
        else
        {
            time_Inactive = 4.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(is_Spawn_Time)
        {
            StartCoroutine("start_spawn");
        }
        else if(is_Inactive)
        {
            StartCoroutine("start_Inactive");
        }
    }
    void FixedUpdate()
    {
        if( (is_attacker && is_player) ||  //is attacker
            (!is_attacker && !is_player))
        {
            if( !land.gameObject.GetComponent<land_Controller>().ball_is_hold ) //if ball isn't hold by any attacker
            {
                if(!is_Inactive)
                    catch_ball();
            }
            else
            {
                if(!is_Inactive)
                {
                    if(!is_hold_ball)// go to fence of Defender
                    {
                        goto_fence(is_player);
                    }
                    else if(is_hold_ball) // hold ball to go gate of Defender
                    {
                        highlight.SetActive(true);
                        goto_gate();
                    }
                }
                
            }
        }
        else //is defender
        {
            if(!is_Spawn_Time)
            {
                range_detect.SetActive(true); //show range detection
            }
            if(is_detect) // 
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(attacker_is_detected.transform.position.x, this.transform.position.y, attacker_is_detected.transform.position.z), normal_speed_defender * Time.deltaTime);
            }
        }
    }
    void goto_fence(bool player)
    {
        if(player)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(this.transform.position.x, this.transform.position.y, fence_Eneny.transform.position.z), normal_speed_attacker * Time.deltaTime);
        }
        else
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(this.transform.position.x, this.transform.position.y, fence_Player.transform.position.z), normal_speed_attacker * Time.deltaTime);
        }
    }
    void catch_ball()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(ball.transform.position.x, this.transform.position.y, ball.transform.position.z), normal_speed_attacker * Time.deltaTime);
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == fence_Eneny)
        {
            this.gameObject.SetActive(false);
        }
        if(other.gameObject == fence_Player)
        {
            this.gameObject.SetActive(false);
        }
        if(other.gameObject == ball)
        {
          // ball_animator.SetBool("is_hold", true);
            this.transform.position = new Vector3(ball.transform.position.x, this.transform.position.y, ball.transform.position.z);
            land.GetComponent<land_Controller>().ball_is_hold = true;
           //ball.gameObject.GetComponent<ball_Controller>().is_hold = true;
            is_hold_ball = true;
            ball.SetActive(false); // hide the ball
        }
        if(other.gameObject == gate_enemy && is_player && is_hold_ball)
        {
            this.gameObject.SetActive(false);
            finsh_match(is_player);
        }
        else if(other.gameObject == gate_player && !is_player && is_hold_ball)
        {
            this.gameObject.SetActive(false);
            finsh_match(is_player);
        }
        if((other.gameObject == fence_Eneny && is_player) ||
            (other.gameObject == fence_Player && !is_player))
        {
            Destroy(this);
        }
        if(other.gameObject.name == "range_detect" )
        {
            if(is_player && is_attacker && other.gameObject.transform.parent.gameObject.GetComponent<player_Enemy_Controller>().is_player == false) //is attacker
            {
                //Debug.Log("hanh debug 111111 other ");
                other.gameObject.transform.parent.gameObject.GetComponent<player_Enemy_Controller>().is_detect = true;
                other.gameObject.transform.parent.gameObject.GetComponent<player_Enemy_Controller>().attacker_is_detected = this.gameObject;
                //other.gameObject.transform.parent.gameObject.transform.position = Vector3.MoveTowards(other.gameObject.transform.parent.gameObject.transform.position, this.gameObject.transform.position, normal_speed_defender * Time.deltaTime);
            }
        }
        if(     other.gameObject.name == "player" 
                && is_detect 
                && other.gameObject.transform.parent.gameObject.GetComponent<player_Enemy_Controller>().is_hold_ball)
        //  && other.gameObject.transform.parent.gameObject.GetComponent<player_Enemy_Controller>().is_detect && is_player && is_attacker)
        {
            Debug.Log("hanh debug 111111 cash player ");
            other.gameObject.transform.parent.gameObject.GetComponent<player_Enemy_Controller>().is_Inactive = true;
            this.is_Inactive = true;
            this.transform.position = pos_spawn;
            // if(is_player && is_attacker && other.gameObject.transform.parent.gameObject.GetComponent<player_Enemy_Controller>().is_detect)// player is attacker
            // {
            //     is_Inactive = true;
            // }
            // if(!is_player && !is_attacker && other.gameObject.tag == "Player") // enemy is attacker
            // {
            //     is_Inactive =true;
            // }
        }
        // if(is_player && is_attacker && other.gameObject.transform.parent.gameObject.GetComponent<player_Enemy_Controller>().is_detect )//player is attacker
        // if(is_detect && other.gameObject.tag == "Player" && )
        // {
        //     Debug.Log("hanh debug 111111 cash player ");
        // }
    }
    void finsh_match(bool player)
    {
        //remove all child of parent player and enemy
        //Debug.Log("count child of parent player: "+ parent_player.);
        foreach (Transform child_player in parent_player.transform)
            Destroy(child_player.gameObject);
        foreach (Transform child_enemy in parent_enemy.transform)
            Destroy(child_enemy.gameObject);
        if(player) // player is attacker
        {
            land.GetComponent<land_Controller>().score_player ++;
        }
        else //enemy is attacker
        {
            land.GetComponent<land_Controller>().score_enemy ++;
        }
        land.GetComponent<land_Controller>().is_show_msg = true;
    }
    public void goto_gate()
    {
        if(is_player && is_attacker) // player is attacker
        {
           // Debug.Log("go to gate position: " + gate_enemy.gameObject.transform.position);
            //go to gate enemy
            this.transform.position = Vector3.MoveTowards(this.transform.position, gate_enemy.gameObject.transform.position, Carrying_Speed * Time.deltaTime);
        }
        else if(!is_player && !is_attacker)//enemy is attacker
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, gate_player.gameObject.transform.position, Carrying_Speed * Time.deltaTime);
        }
    }
    IEnumerator start_spawn() 
    {
        yield return new WaitForSeconds(time_spawn);
        is_Spawn_Time = false;
        is_Inactive = false;
        if(is_player)
        {
            mesh.material = material_player;
        }
        else
        {
            mesh.material = material_enemy;
        }
    }
    IEnumerator start_Inactive() 
    {
        yield return new WaitForSeconds(time_Inactive);
        is_Inactive = false;
        mesh.material = material_inactive;
    }
}
