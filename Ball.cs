using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System;

    public class Ball : MonoBehaviour
    {
    public Coords[] coords;

    public int[] getTimeStamps()
    {
        int[] tss = new int[coords.Length];
        for (int i = 0; i < coords.Length; i++)
        {
            tss[i] = coords[i].ts;
        }
        return tss;
    }

    public class Coords
    {
        private double PosX;
        public double x
        {
            get
            {
                return this.PosX;
            }
            set
            {
                this.PosX = value;
            }
        }
        private double PosY;
        public double y
        {
            get
            {
                return this.PosY;
            }
            set
            {
                this.PosY = value;
            }
        }
        public int ts { get; set; }
    }
}

