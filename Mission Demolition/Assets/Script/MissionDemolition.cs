/****
 * Created By: Siyu Yang
 * Date Created: 2/15/2022
 * 
 * Last Edited: 2/15/2022
 * Last Edited by: Siyu Yang
 * 
 * Description: View both the slingshot and castle
 */
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode
{
    idle,
    playing,
    levelEnd
}
public class MissionDemolition : MonoBehaviour
{
    static private MissionDemolition S;//s private Singleton

    [Header("Set in Inspector")]
    public Text uitlevel;//The UIText level Text
    public Text uitShots;//the uitext shots text
    public Text uitButton;//the uitext button text
    public Vector3 castlePos;//the place to put castles
    public GameObject[] castles;// an array of the castles

    [Header("Set Dynamically")]
    public int level;//the current level
    public int levelMax;//the number of levels
    public int shotsTaken;
    public GameObject castle;//the current castle
    public GameMode mode = GameMode.idle;
    public string showing = "Show Slingshot";//followCam mode

    // Start is called before the first frame update
    void Start()
    {
        S = this;//define the singleton

        levelMax = castles.Length;
        StartLevel();
    }//end start


    void StartLevel()
    {
        if (castle != null)//get rid of the old castle if one exists
        {
            Destroy(castle);
        }//end if

        GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");
        foreach(GameObject pTemp in gos)
        {
            Destroy(pTemp);
        }

        castle = Instantiate<GameObject>(castles[level]);
        castle.transform.position = castlePos;
        shotsTaken = 0;
        SwitchView("wShow Both");//reset the camera
        ProjectileLine.S.Clear();

        Goal.goalMet = false;//reset the goal

        UpdateGUI();

        mode = GameMode.playing;
    }//end startLevel

    void UpdateGUI()
    {
        uitlevel.text = "Level: " + (level + 1) + "of" + levelMax;
        uitShots.text = "Shots Taken: " + shotsTaken;
    }// end updategui()

    // Update is called once per frame
    void Update()
    {
        UpdateGUI();

        if((mode == GameMode.playing) && Goal.goalMet)
        {
            mode = GameMode.levelEnd;//change mod to shop checking for level end
            SwitchView("Show Both");// zoom out
            Invoke("NextLevel", 2f);
        }// end if
    }// end uodate

    void NextLevel()
    {
        level++;
        if (level == levelMax)
        {
            level = 0;
        }
        StartLevel();
    }// end nextLevel()

    public void SwitchView(string eView = "")
    {
        if (eView == "")
        {
            eView = uitButton.text;
        }// end if
        showing = eView;
        switch (showing)
        {
            case "Show Slingshot":
                FollowCam.POI = null;
                uitButton.text = "Show Castle";
                break;

            case "Show Castle":
                FollowCam.POI = S.castle;
                uitButton.text = "Show Both";
                break;

            case "Show Both":
                FollowCam.POI = GameObject.Find("ViewBoth");
                uitButton.text = "Show Slingshot";
                break;
        }
    }
}
