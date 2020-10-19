using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ball_Controller : MonoBehaviour
{
    public GameObject land_player, land_enemy;
    public float land_w, land_h;
    public float pos_x, pos_z;
    public GameObject land;
    // private int current_match;
    // private int int_match;
    public bool is_player_attacker;
    // public int score_player;
    // public int score_enemy;
    // public Text player_tag, enemy_tag;
    // private bool is_begin;
    // public GameObject ball_prefab;
    public bool is_hold;
    public GameObject  parent;
    public bool is_spawn_ball;
    public float x,y,z;
    // public GameObject msg_result;
    // public bool is_show_msg;
    // Start is called before the first frame update
    void Start()
    {
        is_spawn_ball = false;
        // int_match = 5;
        // current_match = 0;
        // score_player =  score_enemy = 0;
        is_player_attacker = land.GetComponent<land_Controller>().is_player_attacker ;
        // is_begin = false;
        // is_show_msg = false;
        // msg_result.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // if(!is_begin && current_match < int_match)
        // {
        //     start_new_match();
        //     is_begin = true;
        // }
        // if(is_show_msg)
        // {
        //     Debug.Log("is show result match: " );
        //     show_result_match();
        // }
        // else
        // {
        //     msg_result.SetActive(false);
        // }
    }
    // void show_result_match()
    // {
    //     msg_result.SetActive(true);
    //     Text msg = msg_result.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
    //     msg.text = "SET: " + current_match + "\n Player: " + score_player + "\n Enemy: " + score_enemy;
    //     //StartCoroutine("show_msg");
    // }
    // IEnumerator show_msg() 
    // {
    //     yield return new WaitForSeconds(3.0f);
    //     is_show_msg = false;
    //     msg_result.SetActive(false);   
    // }
    // void start_new_match()
    // {
    //     current_match ++;
    //     if( current_match % 2 == 0)
    //     {
    //         is_player_attacker = false;
    //     }
    //     else
    //     {
    //         is_player_attacker = true;
    //     }
    //     Update_tag();
    //     Mesh _mesh;
    //     if(is_player_attacker)
    //     {
    //         Debug.Log("is player attacker: " );
    //         _mesh = land_player.gameObject.transform.GetComponent<MeshFilter> ().mesh;
    //         pos_x = land_player.gameObject.transform.position.x;
    //         pos_z = land_player.gameObject.transform.position.z;
    //     }
    //     else
    //     {
    //         Debug.Log("is enemy attacker: " );
    //         _mesh = land_enemy.gameObject.transform.GetComponent<MeshFilter> ().mesh;
    //         pos_x = land_enemy.gameObject.transform.position.x;
    //         pos_z = land_enemy.gameObject.transform.position.z;
    //     }
    //     land_w = _mesh.bounds.size.x;
    //     land_h = _mesh.bounds.size.z;
       
    //     Vector3 randpos = new Vector3();
    //     randpos.x = Random.Range(-land_w/2f + 1, land_h/2f - 1) + pos_x;
    //     randpos.y = 0.25f;
    //     randpos.z = Random.Range(-land_w/2f + 1, land_h/2f - 1) + pos_z;
    //     Debug.Log("randpos.x: " + randpos.x);
    //     Debug.Log("randpos.z: " + randpos.z);
    //     parent.transform.position = new Vector3(randpos.x , randpos.y, randpos.z);
    //     //this.transform.position = new Vector3(0,0,0);
    //     is_hold = false;
    // }
    // void Update_tag()
    // {
    //     if(is_player_attacker)
    //     {
    //         player_tag.text = "Player - Attacker";
    //         enemy_tag.text = "Enemy - Defender";
    //     }
    //     else
    //     {
    //         player_tag.text = "Player - Defender";
    //         enemy_tag.text = "Enemy - Attacker";
    //     }
    // }
}
