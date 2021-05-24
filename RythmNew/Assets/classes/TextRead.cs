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

        int numberOfHits = 15;
        Hit[] HitList = new Hit[numberOfHits];
        
        //hit with dummy values

        for (int i = 0; i < numberOfHits; i++) {
            Hit newHit;
            newHit.BeatStart = 4+(i*4);
            newHit.BeatEnd = 7+(i*4);

            if(i<4){
                newHit.Direction = (SwipeDirection) i;
            }else{
                newHit.Direction = 0;
            }
            

            HitList[i] = newHit;
        }
        return HitList;

       
    }
}
