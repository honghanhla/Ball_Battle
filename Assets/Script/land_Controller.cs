using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class land_Controller : MonoBehaviour
{
    public GameObject land_player;
    public GameObject land_enemy;
    public GameObject prefab_player;
    public GameObject prefab_enemy;
    private GameObject  myprefab;
    public GameObject prefab_ball;
    public GameObject parent_player;
    public GameObject parent_enemy;
    public GameObject ball;
    public int point_spawn_attacker;
    public int point_spawn_defender;
    public int current_match;
    public int int_match;
    public bool is_player_attacker;
    public int score_player;
    public int score_enemy;
    public Text player_tag, enemy_tag;
    public bool is_begin;
    public GameObject msg_result;
    public bool is_show_msg;
    public bool ball_is_hold;
    public float pos_x, pos_z;
    public float land_w, land_h;
    // Start is called before the first frame update
    void Start()
    {
        point_spawn_attacker = 2;
        point_spawn_defender = 3;
        int_match = 5;
        current_match = 0;
        score_player =  score_enemy = 0;
        is_player_attacker = true;// in begin, player is attacker
        is_begin = false;
        is_show_msg = false;
        msg_result.SetActive(false);
        //ball = GameObject.Find("Ball");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("touch here");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
 
            RaycastHit hit = new RaycastHit();
    
            if (Physics.Raycast (ray, out hit))
            {
                if (hit.collider.CompareTag("border"))
                {
                     Debug.Log("touch in border no spawn player and enemy");
                }
                else if (hit.collider.CompareTag("land") && !is_show_msg)
                {
                    //Debug.Log("touch on land");
                    Vector3 pos = hit.point;
                    if(hit.collider.gameObject == land_player && is_spawn(true)) // tao player
                    {
                        myprefab = Instantiate(prefab_player);
                        myprefab.transform.parent = parent_player.transform;
                        myprefab.gameObject.GetComponent<player_Enemy_Controller>().is_player = true;
                        myprefab.gameObject.GetComponent<player_Enemy_Controller>().pos_spawn = new Vector3(pos.x, 0.4f, pos.z);
                        myprefab.transform.position = new Vector3(pos.x, 0.4f, pos.z);
                    }
                    else if(hit.collider.gameObject == land_enemy && is_spawn(false)) //tao enemy
                    {
                        myprefab = Instantiate(prefab_enemy);
                        myprefab.transform.parent = parent_enemy.transform;
                        myprefab.gameObject.GetComponent<player_Enemy_Controller>().is_player = false;
                        myprefab.gameObject.GetComponent<player_Enemy_Controller>().pos_spawn = new Vector3(pos.x, 0.4f, pos.z);
                        myprefab.transform.position = new Vector3(pos.x, 0.4f, pos.z);
                    }
                }
                else if((hit.collider.CompareTag("land") && is_show_msg))
                {
                    is_show_msg = false;
                    is_begin = false;
                    msg_result.SetActive(false);
                }
            }
        }
        if(!is_begin && current_match < int_match)
        {
            start_new_match();
            is_begin = true;
        }
        if(is_show_msg)
        {
           // Debug.Log("is show result match: " );
            show_result_match();
        }
    }
    void show_result_match()
    {
        msg_result.SetActive(true);
        Text msg = msg_result.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        msg.text = "SET: " + current_match + "\n Player: " + score_player + "\n Enemy: " + score_enemy;
		//is_begin = false;
		//is_show_msg = false;
        //StartCoroutine("show_msg");
    }
    IEnumerator show_msg() 
    {
        if(is_show_msg)
        {
			//Debug.Log("hanh debug 11111111111111" );
            yield return new WaitForSeconds(1.0f);
            is_show_msg = false;
            msg_result.SetActive(false);
            //start_new_match();
            is_begin = false;
        }
        
    }
    void start_new_match()
    {
        current_match ++;
        if( current_match % 2 == 0)
        {
            is_player_attacker = false;
        }
        else
        {
            is_player_attacker = true;
        }
        parent_enemy.GetComponent<ParentPlayer_Controller>().current_emegy = 0;
        parent_player.GetComponent<ParentPlayer_Controller>().current_emegy = 0;
        Update_tag();
        Mesh _mesh;
        if(is_player_attacker)
        {
            Debug.Log("is player attacker: " );
            _mesh = land_player.gameObject.transform.GetComponent<MeshFilter> ().mesh;
            pos_x = land_player.gameObject.transform.position.x;
            pos_z = land_player.gameObject.transform.position.z;
        }
        else
        {
            Debug.Log("is enemy attacker: " );
            _mesh = land_enemy.gameObject.transform.GetComponent<MeshFilter> ().mesh;
            pos_x = land_enemy.gameObject.transform.position.x;
            pos_z = land_enemy.gameObject.transform.position.z;
        }
        land_w = _mesh.bounds.size.x;
        land_h = _mesh.bounds.size.z;
       
        Vector3 randpos = new Vector3();
        randpos.x = Random.Range(-land_w/2f + 2, land_h/2f - 2) + pos_x;
        randpos.y = 0.25f;
        randpos.z = Random.Range(-land_w/2f + 2, land_h/2f - 2) + pos_z;
        Debug.Log("randpos.x: " + randpos.x);
        Debug.Log("randpos.z: " + randpos.z);
        ball = Instantiate(prefab_ball);
        ball.transform.position = new Vector3(randpos.x, randpos.y, randpos.z);  
        ball_is_hold = false;
    }
     void Update_tag()
    {
        if(is_player_attacker)
        {
            player_tag.text = "Player - Attacker";
            enemy_tag.text = "Enemy - Defender";
        }
        else
        {
            player_tag.text = "Player - Defender";
            enemy_tag.text = "Enemy - Attacker";
        }
    }
    bool is_spawn(bool player)
    {
        float points;
        // bool is_attacker = Game.gameObject.GetComponent<ball_Controller>().is_player_attacker;
        Debug.Log(" is_player_attacker: " + is_player_attacker);
        //get points
        if(player) // is player
        {
            points = parent_player.gameObject.GetComponent<ParentPlayer_Controller>().current_emegy;
            if(is_player_attacker) // player is attacker
            {
                if(points >= point_spawn_attacker)
                {
                    points = points - point_spawn_attacker;
                    //Debug.Log(" land_controller current_emegy: " + points);
                    parent_player.gameObject.GetComponent<ParentPlayer_Controller>().current_emegy = points;
                    parent_player.gameObject.GetComponent<ParentPlayer_Controller>().player_emegy_text.text = points + " points";
                    parent_player.gameObject.GetComponent<ParentPlayer_Controller>().is_updated_points = true;
                    return true;
                }
            }
            else // player is defender
            {
                if(points >= point_spawn_defender)
                {
                    points = points - point_spawn_defender;
                    parent_player.gameObject.GetComponent<ParentPlayer_Controller>().current_emegy = points;
                    parent_player.gameObject.GetComponent<ParentPlayer_Controller>().player_emegy_text.text = points + " points";
                    parent_player.gameObject.GetComponent<ParentPlayer_Controller>().is_updated_points = true;
                    return true;
                }
            }
        }
        else // is enemy
        {
            points = parent_enemy.gameObject.GetComponent<ParentPlayer_Controller>().current_emegy;
            if(is_player_attacker) // enemy is defender
            {
                if(points >= point_spawn_defender)
                {
                    points = points - point_spawn_defender;
                    parent_enemy.gameObject.GetComponent<ParentPlayer_Controller>().current_emegy = points;
                    parent_enemy.gameObject.GetComponent<ParentPlayer_Controller>().player_emegy_text.text = points + " points";
                    parent_enemy.gameObject.GetComponent<ParentPlayer_Controller>().is_updated_points = true;
                    return true;
                }
            }
            else //enemy is attacker
            {
                if(points >= point_spawn_attacker)
                {
                    points = points - point_spawn_attacker;
                    parent_enemy.gameObject.GetComponent<ParentPlayer_Controller>().current_emegy = points;
                    parent_enemy.gameObject.GetComponent<ParentPlayer_Controller>().player_emegy_text.text = points + " points";
                    parent_enemy.gameObject.GetComponent<ParentPlayer_Controller>().is_updated_points = true;
                    return true;
                }
            }
        }
        return false;
    }
}
