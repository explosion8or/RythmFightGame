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

    public static int numberOfHits = 68;
    public static Hit[] HitList = new Hit[numberOfHits];  
    public static Hit[] IntakeHits()
    {
        //string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Public\TestFolder\WriteLines2.txt");
        ;
        
        
        //hit with dummy values

        yeetu(3, 12.5f, 16, 4, 0);
        yeetu(1, 28.5f, 30, 2, 4);
        yeetu(3, 32.5f, 36, 4, 6);
        yeetu(1, 44.5f, 46, 2, 10);
        yeetu(12, 51.5f, 53, 2, 12);
        yeetu(9, 77.5f, 80, 4, 26);
        yeetu(32, 112.5f, 114, 2, 35);
        


        return HitList;  
    }

    public static void yeetu(int count, float SoS, float EoS, int timePer, int hitNumba){
        for (int i = 0; i <= count; i++) {
            Hit newHit;
            newHit.BeatStart = SoS+(i*timePer);
            newHit.BeatEnd = EoS+(i*timePer);

            if(i<4){
                newHit.Direction = (SwipeDirection) i;
            }else{
                newHit.Direction = (SwipeDirection) Random.Range(0, 4);
            }
            HitList[hitNumba+i] = newHit;
        }
      
    }
}
