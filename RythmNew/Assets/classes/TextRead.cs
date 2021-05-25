using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public struct Hit {
    public float BeatStart;
    public float BeatEnd;
    public SwipeDirection Direction;
}

public static class TextRead 
{
    public static Hit[] IntakeHits()
    {
        //string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Public\TestFolder\WriteLines2.txt");
        int numberOfHits = 96;
        Hit[] HitList = new Hit[numberOfHits];
        
        //hit with dummy values

        for (int i = 0; i < numberOfHits; i++) {
            Hit newHit;
            newHit.BeatStart = 13f+(i*4);
            newHit.BeatEnd = 16+(i*4);

            if(i<4){
                newHit.Direction = (SwipeDirection) i;
            }else{
                newHit.Direction = (SwipeDirection) Random.Range(0, 4);
            }
            

            HitList[i] = newHit;
        }
        return HitList;  
    }

}
