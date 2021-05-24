using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ConductorBehavior : MonoBehaviour
{

    //Song beats per minute
    //This is determined by the song you're trying to sync up to
    public float songBpm;

    //The number of seconds for each song beat
    public float secPerBeat;

    //Current song position, in seconds
    public float songPosition;

    //Current song position, in beats
    public float songPositionInBeats;

    //How many seconds have passed since the song started
    public float dspSongTime;

    //an AudioSource attached to this GameObject that will play the music.
    public AudioSource musicSource;

    //the number of beats in each loop
    public float beatsPerLoop;

    //the total number of loops completed since the looping clip first started
    public int completedLoops = 0;

    //The current position of the song within the loop in beats.
    public float loopPositionInBeats;

    //The current relative position of the song within the loop measured between 0 and 1.
    public float loopPositionInAnalog;

    //Conductor instance
    public static ConductorBehavior instance;

    //how many beats off you can be
    public float errorMargin;

    public int currentStartHit;
    public int currentEndHit;

    public bool hitsLeft;
    public bool startHitsLeft;
  

    public Hit[] hitList;

    //delegates
    public delegate void SendHitDelegate(Hit Hit);
    //events
    public event SendHitDelegate OnHitStart;
    public event SendHitDelegate OnHitEnd;

    void Awake()
    {
        instance = this;
        SwipeDetector.OnSwipe += SwipeDetector_OnSwipe;
    }

    // Start is called before the first frame update
    void Start()
    {

        gameObject.tag = "Conductor";
        //Load the AudioSource attached to the Conductor GameObject
        musicSource = GetComponent<AudioSource>();

        //song bpm for battle time
        songBpm = 140;

        errorMargin = .3f;

        //Calculate the number of seconds in each beat
        secPerBeat = 60f / songBpm;

        //make a queue with all be beats in it from TextRead
        

        hitList = TextRead.IntakeHits();

        //debug for start hits
        /*
        foreach(Hit hit in hitList){
            Debug.Log(hit.BeatStart);
        }*/

        currentStartHit=0;
        currentEndHit= 0;
        hitsLeft = true;
        startHitsLeft=true;

        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;

        //Start the music
        musicSource.Play();

        
    }

    
    // Update is called once per frame
    void Update()
    {
        //determine how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime);
        //Debug.Log(songPosition);

        float oldSongPositionInBeats = songPositionInBeats;
        //determine how many beats since the song started
        songPositionInBeats = songPosition / secPerBeat;
        
        
        if((int)oldSongPositionInBeats != (int)songPositionInBeats){
            Debug.Log((int)songPositionInBeats);
        }

        
        
        //check to see if there are hits left 
        if(hitsLeft){
            //check to end hit
            if(hitList[currentEndHit].BeatEnd <= songPositionInBeats){
                
                OnHitEnd?.Invoke(hitList[currentEndHit]);

                if(currentEndHit+1<hitList.Length){
                    currentEndHit++;
                }
                else{
                    hitsLeft = false;
                }

            }
            //check to start hit
            if(hitList[currentStartHit].BeatStart <= songPositionInBeats && startHitsLeft){
                
                OnHitStart?.Invoke(hitList[currentStartHit]);

                if(currentStartHit+1<hitList.Length){
                    currentStartHit++;
                }
                else{
                    startHitsLeft = false;
                }
            }

            
            
        }
        


        //calculate the loop position
        
        if (songPositionInBeats >= (completedLoops + 1) * beatsPerLoop)
            completedLoops++;
        loopPositionInBeats = songPositionInBeats - completedLoops * beatsPerLoop;

        loopPositionInAnalog = loopPositionInBeats / beatsPerLoop;
        
    }



    private void SwipeDetector_OnSwipe(SwipeData data)
    {
        Debug.Log(IsOnBeat());
    }

    public bool IsOnBeat(){    
        float beatError = songPositionInBeats - /*Math.Truncate*/(int)(songPositionInBeats);
        if(beatError<= errorMargin || beatError >= 1-errorMargin){
            return false;
        }else{
            return true;
        }
    }
}
