using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Movement : MonoBehaviour
{

    public Rail rail;
    public PlayMode mode;


    public bool isReversed;
    public bool isLooping;
    public bool pingPong;
    public PlayLoader playClass;
    //public float[] currentSpeed;
    //public GameObject[] nodes;
    public GameObject[] nodes;

    private int currentSeg;
    public float transition;
    private bool isCompleted;
    public float moveSpeed = 10f;
    public int i = 0;
    public float[] speeds;
    public int[] timeStamps;

    public void Update()
    { 

        if (!rail)
            return;

        if (rail.IsPlaying())
            Play();
    }

    public void setRail(Rail rail) {
        this.rail = rail;
    }


    public void Play(bool forward = true)
    {
   

        //currentSpeed = new float[5000];
        while(currentSeg > 0 && timeStamps[currentSeg - 1] > rail.getPlayTime())
        {
            --currentSeg;
        }
        while (timeStamps[currentSeg + 1] < rail.getPlayTime())
        {
            transition = 0;
            if (++currentSeg == rail.nodes.Length - 1)
            {
                isCompleted = true;
                return;
            }
        }

        //distance between these 2 nodes
        float m = (rail.nodes[currentSeg + 1].transform.position - rail.nodes[currentSeg].transform.position).magnitude;
        float s = 0;
        if (Math.Round(m * 100) != 0) //if the two nodes are at the same place
        {
            s = (Time.unscaledDeltaTime * 1 / m) * speeds[currentSeg];
        }

        //float s = (Time.unscaledDeltaTime * 1 / m) * moveSpeed; // TODO me
        transition += (forward) ? s : -s;

        //if (transition > 1)
        //{
            //transition = 0;
            //currentSeg++;

            //if(currentSeg == rail.nodes.Length - 1)
            //{
            //    if (isLooping)
            //    {
            //        if(pingPong) {
            //            transition = 1;
            //            currentSeg = rail.nodes.Length - 2;
            //            isReversed = !isReversed;
            //        }
            //        else {
            //            currentSeg = 0;
            //        }
            //    }
            //    else {
            //        isCompleted = true;
            //        return;
            //    }
            //}
        //}


        //else if(transition < 0){
        
        //    transition = 1;
        //    currentSeg--;
        //} 

        //Change animation of ball along rails
        transform.position = rail.LinearPosition(currentSeg, transition);
        transform.rotation = rail.Orientation(currentSeg, transition);
    }

    public void setSpeeds(float[] speeds)
    {
        this.speeds = speeds;
    }

    public void setTimeStamps(int[] timeStamps)
    {
        this.timeStamps = timeStamps;
    }
}