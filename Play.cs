using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System;

[System.Serializable]
public class Play : MonoBehaviour {
    public string name { get; set; }
    //public int number { get; set; }
    //public string position { get; set; }
    //public string team { get; set; }
    public Coords[] coords;

    public int[] getTimeStamps()
    {
        int[] tss = new int[coords.Length];
        for(int i = 0; i < coords.Length; i++)
        {
            tss[i] = coords[i].ts;
        }
        return tss;
    }

    public class Coords
    {
        private double PosX;
        public double posX
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
        public double posY
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


//[System.Serializable]
//public class Person
//{
//    // C# 3.0 auto-implemented properties
//    public string Name { get; set; }
//    public int Age { get; set; }
//    public DateTime Birthday { get; set; }
//    public Coords[] coords;
//    public class Coords
//    {
//        public double posX { get; set; }
//        public double posY { get; set; }
//        public long ts { get; set; }
//    }


//}
