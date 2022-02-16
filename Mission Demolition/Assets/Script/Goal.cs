/****
 * Created By: Siyu Yang
 * Date Created: 2/15/2022
 * 
 * Last Edited: 2/15/2022
 * Last Edited by: Siyu Yang
 * 
 * Description: the goal of the catle needs to react when hit by the projectile
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    static public bool goalMet = false;// a static fielf accessible by code anywhere

    private void OnTriggerEnter(Collider other)
    {
     //when thw trigger is hit by something
     //check to see if its a projectile
     if (other.gameObject.tag == "Projectile")
        {
            Goal.goalMet = true;//if so set goalMet to true
            Material mat = GetComponent<Renderer>().material;//also set the alpha of the color to higher opacity
            Color c = mat.color;
            c.a = 1;
            mat.color = c;
        }//end if
    }//end OnTriggerEnter
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
