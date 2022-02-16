/****
 * Created By: Siyu Yang
 * Date Created: 2/15/2022
 * 
 * Last Edited: 2/15/2022
 * Last Edited by: Siyu Yang
 * 
 * Description: follow camera
 */
using System.Collections;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    static public GameObject POI;//the static point of interest

    [Header("Set in Inspector")]
    public float easing = 0.05f;
    public Vector2 minXY = Vector2.zero;

    [Header("Set Dynamically")]
    public float camZ;//the desired Zpos of the camera

    private void Awake()
    {
        camZ = this.transform.position.z;
    }

    private void FixedUpdate()
    {
        // if (POI == null) return;//return is there is no poi
        // Vector3 destination = POI.transform.position;//Force destination.z to be camZ to keep the camera for enough away

        Vector3 destination;
        if (POI == null)
        {
            destination = Vector3.zero;
        }
        else
        {
            destination = POI.transform.position;// Get the position of the poi
            if (POI.tag == "Projectile")
            {
                if (POI.GetComponent<Rigidbody>().IsSleeping())
                {
                    POI = null;
                    return;
                }
            }
        }
        destination.x = Mathf.Max(minXY.x, destination.x);//Limit the X to minimum values
        destination.y = Mathf.Max(minXY.y, destination.y);//Limit the y to minimum values
        destination = Vector3.Lerp(transform.position, destination, easing);
        destination.z = camZ;//set the camera to the destination
        transform.position = destination;
        Camera.main.orthographicSize = destination.y + 10;//set the orthographicSize of the camera to keep ground in view
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
