/****
 * Created By: Siyu Yang
 * Date Created: 2/15/2022
 * 
 * Last Edited: 2/15/2022
 * Last Edited by: Siyu Yang
 * 
 * Description: SlingShot Countrol
 */
using System.Collections;
using UnityEngine;

public class SlingShot : MonoBehaviour
{
    static private SlingShot s;

    [Header("Set in Inspector")]// fields set in the Unity Inspector pane
    public GameObject prefabProjectile;
    public float velocityMult = 8f;

    [Header("Set Dynamically")]//fields set dynamically
    public GameObject launchPoint;
    public Vector3 launchPos;
    public GameObject projectile;
    public bool aimingMode;
    private Rigidbody projectileRigibody;

    static public Vector3 LAUNCH_POS
    {
        get
        {
            if (s == null) return Vector3.zero;
            return s.launchPos;
        }
    }

    void Awake()
    {
        s = this;
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
        launchPos = launchPointTrans.position;
    }
    void OnMouseEnter()
    {
        //print("SlingShot: OnMouseEnter()");
        launchPoint.SetActive(true);
    }

    void OnMouseExit()
    {
        //print("SlingShot: OnMouseExit()");
        launchPoint.SetActive(false);
    }

    void OnMouseDown()
    {
        aimingMode = true;//The player has pressed the mouse button while over Slingshot
        projectile = Instantiate(prefabProjectile) as GameObject;//Instantiate a Projectile
        projectile.transform.position = launchPos;// Start it at the launchPoint
        projectile.GetComponent<Rigidbody>().isKinematic = true;//set it to is kinematic for now
        projectileRigibody = projectile.GetComponent<Rigidbody>();
        projectileRigibody.isKinematic = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!aimingMode) return;//If slingshot is not in aimingMode, dont run this code
        Vector3 mousePos2D = Input.mousePosition;//Get the current mouse position in 2D screen coordinates
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        Vector3 mouseDelta = mousePos3D - launchPos;//Find the delta from the launchPos to the mousePos3D
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;//Limit mouseDelta to the radius of thr Slingshot Spherecollider
        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        Vector3 projPos = launchPos + mouseDelta;//Move the projectile to this new position
        projectile.transform.position = projPos;

        if (Input.GetMouseButtonUp(0))// the mouse has been released
        {
            aimingMode = false;
            projectileRigibody.isKinematic = false;
            projectileRigibody.velocity = -mouseDelta * velocityMult;
            FollowCam.POI = projectile;
            projectile = null;
        }
    }
}
