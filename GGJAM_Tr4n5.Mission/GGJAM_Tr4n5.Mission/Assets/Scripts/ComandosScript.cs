using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ComandosScript : MonoBehaviour
{
    public int pos1;
    public int pos2;
    public int pos3;
    public int pos4;
    public int pos5;
    public int pos6;
    public int pos7;
    public int pos8;
    public int pos9;
    public int pos10;

    public int[] pos = new int[10];

    // Use this for initialization
    void Update()
    {
        pos[0] = pos1;
        pos[1] = pos2;
        pos[2] = pos3;
        pos[3] = pos4;
        pos[4] = pos5;
        pos[5] = pos6;
        pos[6] = pos7;
        pos[7] = pos8;
        pos[8] = pos9;
        pos[9] = pos10;

        /*
        Debug.Log(pos[0].ToString()+ pos[1].ToString() + 
                                        pos[2].ToString()+ 
                                        pos[3].ToString()+ 
                                        pos[4].ToString()+ 
                                        pos[5].ToString()+ 
                                        pos[6].ToString()+ 
                                        pos[7].ToString()+ 
                                        pos[8].ToString()+ 
                                        pos[9].ToString()); */
    }
}