using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowInsideLol : MonoBehaviour
{
    public float speed = .1f;
    

    GameObject conductor;

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

    public float currentHitLOL;

    public float beatDiff;
    
    Renderer ye_ObjectRenderer;
    // Start is called before the first frame update
    void Start()
    {
        conductor = GameObject.Find("Conductor");
        ConductorBehavior conductorBehavior = conductor.GetComponent<ConductorBehavior>();

        conductorBehavior.OnHitEnd += ConductorBehavior_OnHitEnd;
        conductorBehavior.OnHitStart += ConductorBehavior_OnHitStart;
        gameObject.SetActive(false);

       


        musicSource = GetComponent<AudioSource>();
        songBpm = 140;
        secPerBeat = 60f / songBpm;
        dspSongTime = (float)AudioSettings.dspTime;
        currentHitLOL=0;

        //Fetch the GameObject's Renderer component
        ye_ObjectRenderer = GetComponent<Renderer>();
    }

    void Update() {
        songPosition = (float)(AudioSettings.dspTime - dspSongTime);
        //Debug.Log(songPosition);

        float oldSongPositionInBeats = songPositionInBeats;
        //determine how many beats since the song started
        songPositionInBeats = songPosition / secPerBeat;

        float yeet = (songPositionInBeats - currentHitLOL)/beatDiff;
        Color textureColor = ye_ObjectRenderer.material.color;
        textureColor.a = yeet+.5f;
        ye_ObjectRenderer.material.color = textureColor;
    }

    // Update is called once per frame
     private void ConductorBehavior_OnHitStart(Hit hit){
        gameObject.SetActive(true);
        
        
        
        beatDiff = hit.BeatEnd - hit.BeatStart;
        currentHitLOL = hit.BeatStart;
        gameObject.SetActive(true);
    } 

    private void ConductorBehavior_OnHitEnd(Hit hit){
        gameObject.SetActive(false);
        

    }
}
