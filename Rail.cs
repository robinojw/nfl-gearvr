using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;


public class Rail : MonoBehaviour
{
    //Array of node objects
    public GameObject[] nodes;
    public GameObject[] pathNodes;
    public GameObject[] ballNodes;
    public PlayLoader playLoader;
    public LineRenderer lineRenderer;
    float timer;
    public Vector3 currentPositionHolder;
    private Vector3 startPosition;
    int CurrentNode = 0;
    Movement move;


    public Rail() : base()
    {
        //Debug.Log("Rail Constructor");
    }

    //public void setup(Play.Coords[] coords) {
    //    //speeds = new float[coords.Length];

    //    nodes = new GameObject[coords.Length];
    //    //Vector3 lastPosition = new Vector3((float)coords[0].posX, 0, (float)coords[0].posY);
    //    //int lastTimeStamp = 0;

    //    //Every time coords is passed into the Coords class, call the functions in
    //    //this loop.
    //    for (int i = 0; i < coords.Length; i++)
    //    {
    //        //Create new node in the rail and assign x, y values to posX and posY
    //        nodes[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);


    //        //set name in editor to node
    //        nodes[i].name = "node";

    //        nodes[i].transform.position = new Vector3((float) coords[i].posX,
    //        0, (float) coords[i].posY);

    //        //Set node as a child of the Rail
    //        nodes[i].transform.parent = this.transform;

    //        //if (i > 0)
    //        //{
    //        //    float distance = Vector3.Distance(lastPosition, nodes[i].transform.position);
    //        //    speeds[i] = distance / (coords[i].ts - lastTimeStamp) * 1000;

    //        //}
    //        //else { speeds[i] = 1; } //TODO WRONG

    //        //lastTimeStamp = coords[i].ts;
    //        //lastPosition = nodes[i].transform.position;
    //    }
    //}



    public void Start()
    {
        //CheckNode();
    }

    void Update()
    {
        //timer += Time.unscaledDeltaTime * speeds[CurrentNode]; // TODO look at me. Cant you see. Im fabulous baby.
        //if(transform.position != currentPositionHolder) { ///TODO what if theyre standing still? Dinlo
        //    transform.position = Vector3.Lerp(startPosition, currentPositionHolder, timer);
        //}
        //else {
        //    if(CurrentNode < nodes.Length - 1 ) {
        //        CurrentNode++;
        //        CheckNode();
        //    }
    
        //}
    }



    public float[] ballSetup(Ball.Coords[] coords, PlayLoader loader)
    {
        this.playLoader = loader;

        float[] speeds= new float[coords.Length];
        float[] timeStamps = new float[coords.Length];
        pathNodes = new GameObject[coords.Length];
        nodes = new GameObject[coords.Length];
        Vector3 lastPosition = new Vector3((float)coords[0].x, 0, (float)coords[0].y);
        int lastTimeStamp = 0;

        //Every time coords is passed into the Coords class, call the functions in
        //this loop.

        for (int i = 0; i < coords.Length; i++)
        {
            //Create new node in the rail and assign x, y values to posX and posY
            nodes[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            //Use this to debug the position of nodes in a rail
            //nodes[i].transform.localScale = new Vector3(0, 0, 0);

            //Player path creation
            pathNodes[i] = GameObject.CreatePrimitive(PrimitiveType.Plane);
            pathNodes[i].transform.position = new Vector3((float)coords[i].x,
            0, (float)coords[i].y);
            pathNodes[i].transform.parent = this.transform;
            pathNodes[i].name = "Path";
            pathNodes[i].GetComponent<Renderer>().material = playLoader.m_Path;
            pathNodes[i].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            nodes[i].GetComponent<Renderer>().enabled = false;


            //set name in editor to node
            nodes[i].name = "node";

            nodes[i].transform.position = new Vector3((float)coords[i].x,
            0, (float)coords[i].y);

            //Set node as a child of the Rail
            nodes[i].transform.parent = this.transform;

            if (i > 0)
            {
                float distance = Vector3.Distance(lastPosition, nodes[i].transform.position);
                speeds[i] = distance / (coords[i].ts - lastTimeStamp) * 1000;

            }
            else {
                float distance = Vector3.Distance(lastPosition, nodes[i].transform.position);
                speeds[i] = distance / (coords[i].ts - lastTimeStamp) * 1000; } //TODO WRONG

            lastTimeStamp = coords[i].ts;
            lastPosition = nodes[i].transform.position;


            GameObject currentPosition = nodes[i];

        }
        return speeds;
    }
    public float[] setup(Play.Coords[] coords, PlayLoader loader)
    {
        this.playLoader = loader;

        float[] speeds = new float[coords.Length];
        float[] timeStamps = new float[coords.Length];

        nodes = new GameObject[coords.Length];
        pathNodes = new GameObject[coords.Length];
        Vector3 lastPosition = new Vector3((float)coords[0].posX, 0, (float)coords[0].posY);
        int lastTimeStamp = 0;

        //Every time coords is passed into the Coords class, call the functions in
        //this loop.
       

        for (int i = 0; i < coords.Length; i++)
        {
            //Create new node in the rail and assign x, y values to posX and posY
            nodes[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            //Player path creation
            pathNodes[i] = GameObject.CreatePrimitive(PrimitiveType.Plane);
            pathNodes[i].transform.position = new Vector3((float)coords[i].posX,
            0, (float)coords[i].posY);
            pathNodes[i].transform.parent = this.transform;
            pathNodes[i].name = "Path";
            pathNodes[i].GetComponent<Renderer>().material = playLoader.m_Path;
            pathNodes[i].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            nodes[i].GetComponent<Renderer>().enabled = false;


            //Use this to debug the position of nodes in a rail
            //nodes[i].transform.localScale = new Vector3(0, 0, 0);

            //nodes[i].GetComponent<Renderer>().material.color = Color.yellow;


            //set name in editor to node
            nodes[i].name = "node";

            nodes[i].transform.position = new Vector3((float)coords[i].posX,
            2.21f, (float)coords[i].posY);

            //Set node as a child of the Rail
            nodes[i].transform.parent = this.transform;


            if (i > 0)
            {
                float distance = Vector3.Distance(lastPosition, nodes[i].transform.position);
                speeds[i] = distance / (coords[i].ts - lastTimeStamp) * 1000;

            }
            else { speeds[i] = 1; } //TODO WRONG

            lastTimeStamp = coords[i].ts;
            lastPosition = nodes[i].transform.position;


            GameObject currentPosition = nodes[i];

        }

        return speeds;
    }

    //public float[] ballSetup(Ball.Coords[] coords, PlayLoader loader)
    //{
    //    this.playLoader = loader;
    //    float[] ballSpeeds = new float[coords.Length];
    //    float[] ballStamps = new float[coords.Length];


    //    ballNodes = new GameObject[coords.Length];

    //    Vector3 lastPosition = new Vector3((float)coords[0].x, 0, (float)coords[0].y);
    //    int lastTimeStamp = 0;



    //    //Every time coords is passed into the Coords class, call the functions in
    //    //this loop.

    //    for (int i = 0; i < coords.Length; i++)
    //    {
    //        //Create new node in the rail and assign x, y values to posX and posY
    //        ballNodes[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);

    //        //Use this to debug the position of nodes in a rail
    //        //nodes[i].transform.localScale = new Vector3(0, 0, 0);

    //        //set name in editor to node
    //        ballNodes[i].name = "node";

    //        ballNodes[i].transform.position = new Vector3((float)coords[i].x,
    //        0, (float)coords[i].y);

    //        //Set node as a child of the Rail
    //        ballNodes[i].transform.parent = this.transform;

    //        if (i > 0)
    //        {
    //            float distance = Vector3.Distance(lastPosition, ballNodes[i].transform.position);
    //            ballSpeeds[i] = distance / (coords[i].ts - lastTimeStamp) * 1000;

    //        }
    //        else { ballSpeeds[i] = 1; } //TODO WRONG
    //        ballStamps[i] = coords[i].ts;

    //        lastTimeStamp = coords[i].ts;
    //        lastPosition = ballNodes[i].transform.position;


    //        GameObject currentPosition = ballNodes[i];

    //    }
    //    return ballSpeeds;
    //}

    public bool IsPlaying()
    {
        return playLoader.IsPlaying;
    }

    public float getPlayTime()
    {
        return playLoader.PlayTimer;
    }


    //void CheckNode()
    //{
    //    timer = 0;
    //    currentPositionHolder = nodes[CurrentNode].transform.position;
    //    startPosition = transform.position;
    //}

    public Vector3 LinearPosition(int seg, float ratio)
    {

        Vector3 p1 = nodes[seg].transform.position;
        Vector3 p2 = nodes[seg + 1].transform.position;
        
        return Vector3.Lerp(p1, p2, ratio);
    }

    public Vector3 LinearBallPosition(int Bseg, float Bratio)
    {
        Vector3 b1 = ballNodes[Bseg].transform.position;
        Vector3 b2 = ballNodes[Bseg + 1].transform.position;

        return Vector3.Lerp(b1, b2, Bratio);
    }



    //Catmull Positioning, transition across rail
    //public Vector3 CatmullPosition(int seg, float ratio)
    //{
    //    {
    //        Vector3 p1, p2, p3, p4;

    //        if (seg == 0)
    //        {
    //            p1 = nodes[seg].position;
    //            p2 = p1;
    //            p3 = nodes[seg + 1].position;
    //            p4 = nodes[seg + 2].position;
    //        }
    //        else if (seg == nodes.Length - 2)
    //        {
    //            p1 = nodes[seg - 1].position;
    //            p2 = nodes[seg].position;
    //            p3 = nodes[seg + 1].position;
    //            p4 = p3;
    //        }
    //        else
    //        {
    //            p1 = nodes[seg - 1].position;
    //            p2 = nodes[seg].position;
    //            p3 = nodes[seg + 1].position;
    //            p4 = nodes[seg + 2].position;
    //        }

    //        float t2 = ratio * ratio;
    //        float t3 = t2 * ratio;

    //        //Float 

    //        //XYZ catmull calculation algorithm
    //        float x = 0.5f * ((2.0f * p2.x)
    //                          + (-p1.x + p3.x)
    //                          * ratio + (2.0f * p1.x - 5.0f * p2.x + 4 * p3.x - p4.x)
    //                          * t2 + (-p1.x + 3.0f * p2.x - 3.0f * p3.x + p4.x)
    //                          * t3);

    //        float y = 0.5f * ((2.0f * p2.y)
    //                       + (-p1.y + p3.y)
    //                       * ratio + (2.0f * p1.y - 5.0f * p2.y + 4 * p3.y - p4.y)
    //                       * t2 + (-p1.y + 3.0f * p2.y - 3.0f * p3.y + p4.y)
    //                       * t3);

    //        float z = 0.5f * ((2.0f * p2.z)
    //                          + (-p1.z + p3.z)
    //                       * ratio + (2.0f * p1.z - 5.0f * p2.z + 4 * p3.z - p4.z)
    //                       * t2 + (-p1.z + 3.0f * p2.z - 3.0f * p3.z + p4.z)
    //                          * t3);

    //        return new Vector3(x, y, z);

    //    }
    //}



    public Quaternion Orientation(int seg, float ratio)
    {
        Quaternion q1 = nodes[seg].transform.rotation;
        Quaternion q2 = nodes[seg + 1].transform.rotation;

        return Quaternion.Lerp(q1, q2, ratio);

    }

    public Quaternion BallOrientation(int seg, float ratio)
    {
        Quaternion q1 = ballNodes[seg].transform.rotation;
        Quaternion q2 = ballNodes[seg + 1].transform.rotation;

        return Quaternion.Lerp(q1, q2, ratio);

    }

    ////Draw dotted line between nodes in editor mode
    //private void OnDrawGizmos()
    //{
    //    for (int i = 0; i < nodes.Length - 1; i++)
    //    {
    //        Handles.DrawLine(nodes[i].transform.position, nodes[i + 1].transform.position);
    //                }
    //}
}