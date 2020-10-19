using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParentPlayer_Controller : MonoBehaviour
{
    private int max_emegy;
    public float current_emegy;
    public Text player_emegy_text;
    public bool is_updated_points;
    // Start is called before the first frame update
    void Start()
    {
        max_emegy = 6;
        current_emegy = 0.0f;
        player_emegy_text.text ="0 point";
        //is_updated_points = true;
        StartCoroutine("calc_emegy");
    }

    // Update is called once per frame
    void Update()
    {
        //StartCoroutine("calc_emegy");
        // if(current_emegy < max_emegy)
        // {
        //     is_updated_points = true;
        // }
    }
    IEnumerator calc_emegy() 
    {
        //for (float ft = current_emegy; ft <= max_emegy; ft += 0.5f)
        while(current_emegy <= max_emegy)
        {
            //Debug.Log("current_enegy: " + current_emegy);
            yield return new WaitForSeconds (1.0f);
            current_emegy += 0.5f;
            
            if(current_emegy > max_emegy)
            {
                current_emegy = max_emegy;
            }
            player_emegy_text.text = current_emegy + " point";
        }
    }
}
