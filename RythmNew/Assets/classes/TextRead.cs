using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Hit {
    public float BeatStart;
    public float BeatEnd;
    public SwipeDirection Direction;
}

public static class TextRead 
{
    public static Hit[] IntakeHits()
    {

        int numberOfHits = 7;
        Hit[] HitList = new Hit[numberOfHits];
        
        //hit with dummy values

        for (int i = 0; i < numberOfHits; i++) {
            Hit newHit;
            newHit.BeatStart = 4+(i*4);
            newHit.BeatEnd = 8+(i*4);
            newHit.Direction = 0;

            HitList[i] = newHit;
        }
        return HitList;

       
    }
}
