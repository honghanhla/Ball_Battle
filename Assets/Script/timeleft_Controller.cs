using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class timeleft_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    private int time;
    public Text text_timeleft;
    void Start()
    {
        time = 140;
        StartCoroutine("downcount_timeleft");
    }

    // Update is called once per frame
    void Update()
    {
    }
    IEnumerator downcount_timeleft() 
    {
        while(time > 0)
        {
            yield return new WaitForSeconds(1.0f);
            time = time -1 ;
            text_timeleft.text = time + " s";
            //Debug.Log("time: " + time);
        }
        
    }
}
