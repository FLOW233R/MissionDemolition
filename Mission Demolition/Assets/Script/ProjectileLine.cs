/****
 * Created By: Siyu Yang
 * Date Created: 2/15/2022
 * 
 * Last Edited: 2/15/2022
 * Last Edited by: Siyu Yang
 * 
 * Description: Projectile Line
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLine : MonoBehaviour
{
    static public ProjectileLine S;// Dingleton

    [Header("Set in Inspector")]
    public float minDist = 0.1f;

    private LineRenderer line;
    private GameObject _poi;
    private List<Vector3> points;

    private void Awake()
    {
        S = this;//set the singleton
        line = GetComponent<LineRenderer>();//get a reference to the LineRenderer
        line.enabled = false;//Disable the lineranderer until it's needed
        points = new List<Vector3>();//initialize the points List
    }

    public GameObject poi
    {
        get
        {
            return (_poi);
        }
        set
        {
            _poi = value;
            if (_poi != null)
            {
                line.enabled = false;//when poi is set to something new, it resets everything
                points = new List<Vector3>();
                AddPoint();
            }
        }
    }

    public void Clear()
    {
        _poi = null;
        line.enabled = false;
        points = new List<Vector3>();
    }

    public void AddPoint()
    {
        Vector3 pt = _poi.transform.position;
        if (points.Count > 0 && (pt - lastPoint).magnitude < minDist)
        {
            return;
        }// end if
        if (points.Count == 0)
        {
            Vector3 launchPosDiff = pt - SlingShot.LAUNCH_POI;//to be defined
            points.Add(pt + launchPosDiff);
            points.Add(pt);
            line.positionCount = 2;
            line.SetPosition(0, points[0]);//sets the first two points
            line.SetPosition(1, points[1]);

            line.enabled = true;//enables the lineRenderer
        }//end if
        else
        {
            points.Add(pt);//normal behavior of adding a point
            line.positionCount = points.Count;
            line.SetPosition(points.Count - 1, lastPoint);
            line.enabled = true;
        }//end else
    }//end addpoint

    public Vector3 lastPoint()
    {
        get
        {
            if (points == null)
            {
                return (Vector3.zero);
            }// end if
            return (points[points.Count - 1]);
        }//end get
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
