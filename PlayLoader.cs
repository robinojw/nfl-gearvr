using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;


// [ExecuteInEditMode]


public class PlayLoader : MonoBehaviour {

    //public GameObject[] railArr;
    //private string d;
    //public List<string> b;
    //public Rail[] rails;
    //public Play playerParent;
    //public float x;
    //public float y;
    //public Rail nodes;
    //public Movement MV;
    //public GameObject gameNode;
    //public GameObject gamePlayer;
    protected bool applicationQuit = false;
    public bool IsPlaying = false;
    public float PlayTimer = 0;
    private int biggestTimeStamp = 0;
    private GameObject menu = null;
    Movement move;
    public Material m_Path;




    //Playloader file path
    public string path;


    //Application State
    public void OnApplicationQuit()
    {
        this.applicationQuit = true;
    }

    public void SetPlay(string path)
    {
        this.path = path;
    }
    public void Play()
    {
        IsPlaying = true;
        GetMenu().SetActive(false);
    }
    public void Pause()
    {
        IsPlaying = false;
        GetMenu().SetActive(true);
    }
    public void Reset()
    {
        PlayTimer = 0;
        Play();
    }
  
    public void Forward()
    {
        PlayTimer += 500;
        IsPlaying = true;
        IsPlaying = false;
    }
    public void Rewind()
    {
        PlayTimer -= 500;
        Play();
        System.Threading.Thread.Sleep(10);
        Pause();
    }
    public void Update()
    {
        if (IsPlaying)
        {
            PlayTimer += Time.unscaledDeltaTime * 1000;
            IsPlaying &= PlayTimer < biggestTimeStamp;
           
        }

    }

    public void End()
    {
        PlayTimer = biggestTimeStamp;
    }

    private GameObject GetMenu()
    {
        if (menu == null)
        {
            menu = GameObject.Find("PlayMenu");
        }
        return menu;
    }
    // Use this for initialization
    void Start()
    {
        GameObject container;
        GameObject newRail;
        GameObject ballRail;
        biggestTimeStamp = 0;




        //Iterate through all files in the directory and parse them to the Play class
        foreach (TextAsset file in Resources.LoadAll(path ,typeof(TextAsset)))
        {
            if (file.name == "ball")
            {
                string jsonStrings = file.text;
                Ball ballData = JsonMapper.ToObject<Ball>(jsonStrings);

                //Create ball game object
                GameObject ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                ball.transform.localScale = new Vector3(2, 2, 2);
                ball.transform.position = new Vector3((float)ballData.coords[0].x,
                                                      0, (float)ballData.coords[0].y);
            
                ball.name = "Ball";

                container = new GameObject();
                container.name = "Ball";
                ballRail = new GameObject();
                ballRail.name = "Rail";


                ballRail.transform.parent = container.transform;
                ball.transform.parent = container.transform;

                ball.AddComponent<BallMovement>();

                Rail ballrail = ballRail.AddComponent<Rail>();

                ballrail.ballSetup(ballData.coords, this);

                ball.GetComponent<Renderer>().material.color = Color.blue;

                float[] ballSpeeds = ballrail.ballSetup(ballData.coords, this);
                int[] ballStamps = ballData.getTimeStamps();
                if (ballStamps[ballStamps.Length - 1] > biggestTimeStamp)
                {
                    biggestTimeStamp = ballStamps[ballStamps.Length - 1];
                }

                //ball.GetComponent<Renderer>().material.color = Color.yellow;

                ball.GetComponent<BallMovement>().setRail(ballRail.GetComponent<Rail>());
                ball.GetComponent<BallMovement>().setTimeStamps(ballStamps);
                ball.GetComponent<BallMovement>().setSpeeds(ballSpeeds);

                ball.GetComponent<BallMovement>().setRail(ballRail.GetComponent<Rail>());
                if (this.applicationQuit)
                {
                    Destroy(ball);
                    Destroy(ballrail);
                }
            }
            else
            {
                //string jsonString = File.ReadAllText(file);
                string jsonString = file.text;
                Play playData = JsonMapper.ToObject<Play>(jsonString);
               

                //Instantiate new player game object
                GameObject player = Instantiate(Resources.Load("player")) as GameObject;

                player.transform.Rotate(new Vector3(0, 180, 0));

                //player.AddComponent<Remove>();

                //Apply material to object
               
                player.transform.position = new Vector3((float)playData.coords[0].posX,
                                                        2.21f, (float)playData.coords[0].posY);
                player.name = "Player";

                //player.tsransform.position = new Vector3(transform.position.x, transform.position.y, 10);


                container = new GameObject();
                container.name = playData.name;

                newRail = new GameObject();

                newRail.name = "Rail";

                newRail.transform.parent = container.transform;
                player.transform.parent = container.transform;

                player.AddComponent<Movement>();

                Rail rail = newRail.AddComponent<Rail>();

                float[] speeds = rail.setup(playData.coords, this);
                int[] timeStamps = playData.getTimeStamps();
                if(timeStamps[timeStamps.Length - 1] > biggestTimeStamp)
                {
                    biggestTimeStamp = timeStamps[timeStamps.Length - 1];
                }

                player.GetComponent<Movement>().setRail(newRail.GetComponent<Rail>());
                player.GetComponent<Movement>().setTimeStamps(timeStamps);
                player.GetComponent<Movement>().setSpeeds(speeds);
                //player.GetComponent<Movement>().setRail(newRail);

                //player.GetComponent<Movement>().setSpeed(playData.coords);
                if (this.applicationQuit)
                {
                    Destroy(player.GetComponent<Movement>());
                    Destroy(player);
                    Destroy(rail);
                }
            }
        }
    }

    //void Start()
    //{

    //    //Iterate through all files in the directory and parse them to the Play class
    //    foreach (string file in Directory.GetFiles("Assets/Resources/best", "*.json"))
    //    {
    //        GameObject player = GameObject.CreatePrimitive(PrimitiveType.Capsule);
    //        //Rail Rail = player.AddComponent<Rail>();
    //        //Read all text in file
    //        jsonString = File.ReadAllText(file);

    //        //Rail = GameObject;
    //        //AddComponentMenu rail =

    //        //Rail = player.AddComponent<Rail>();

    //        //Rail.transform.parent = player.transform;

    //        //Rail

    //        GameObject Rail = GameObject.CreatePrimitive(PrimitiveType.Cube);

    //        Rail.AddComponent<Rail>();

    //        Rail.transform.parent = player.transform;


    //        //Rail.setPlayer(player);


    //        //Map Json data to play class objects

    //        // Debug.Log(file + "");
    //        Play playData = JsonMapper.ToObject<Play>(jsonString);
    //        //Debug.Log(playData.name);
    //        //Debug.Log(playData.coords.Length);

    //        //Create player object 
    //        //GameObject player = GameObject.CreatePrimitive(PrimitiveType.Capsule);
    //        //Set dimensions
    //        player.transform.localScale = new Vector3(3, 3, 3);

    //        //Set the positon of the player to the first X, Y position in the array
    //        player.transform.position = new Vector3((float)playData.coords[0].posX,
    //                                                0, (float)playData.coords[0].posY);

    //        player.name = playData.name;



    //        //player.transform.parent = playerParent.transform;

    //        //Add movement component
    //        //Movement component = player.AddComponent<Movement>();


    //        Rail.transform.position = new Vector3((float)playData.coords[0].posX,
    //                                               0, (float)playData.coords[0].posY);

    //        //component.rail = newNode;

    //        node = new GameObject[5000];

    //        float[] speedArr = new float[5000];


    //        //Every time coords is passed into the Coords class, call the functions in
    //        //this loop.
    //        for (int i = 0; i < playData.coords.Length; i++)
    //        {
    //            //Create new node in the rail and assign x, y values to posX and posY
    //            node[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    //            //set name in editor to node
    //            node[i].name = "node";

    //            node[i].transform.position = new Vector3((float)playData.coords[i].posX,
    //            0, (float)playData.coords[i].posY);

    //            //Set node as a child of the Rail
    //            node[i].transform.parent = Rail.transform;

    //            //If the rail has more than one node calculate the distance 
    //            //between the current object and the previous object
    //            if (i > 0)
    //            {
    //                float distance = Vector3.Distance(node[i - 1].transform.position,
    //                                                 node[i].transform.position);

    //                float speed = distance / (playData.coords[i].ts - playData.coords[i - 1].ts) * 1000;

    //                speedArr[i] = speed;

    //            }
    //        }

    //        GameObject railClass = GameObject.FindWithTag(name);
    //        railClass.setSpeeds(speedArr);
    //        railClass.

    //        //Destroy game objects on exit
    //        //player = gamePlayer;

    //        if (this.applicationQuit)
    //        {
    //            Destroy(player);
    //            //Destroy(gameNode);
    //            Destroy(Rail);
    //        }

    //    }
    //}
}
