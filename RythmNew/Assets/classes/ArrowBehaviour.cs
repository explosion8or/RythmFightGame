using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArrowBehaviour : MonoBehaviour
{

    
    public float speed = .1f;
    public int scale = 20;

    private Vector3 targetScale;
    private Vector3 baseScale;

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
    
    Renderer m_ObjectRenderer;

    
    // Start is called before the first frame update
    void Start()
    {
        conductor = GameObject.Find("Conductor");
        ConductorBehavior conductorBehavior = conductor.GetComponent<ConductorBehavior>();

        conductorBehavior.OnHitEnd += ConductorBehavior_OnHitEnd;
        conductorBehavior.OnHitStart += ConductorBehavior_OnHitStart;
        gameObject.SetActive(false);

        baseScale = gameObject.transform.localScale;
        gameObject.transform.localScale = baseScale*scale;
        targetScale = baseScale*scale;


        musicSource = GetComponent<AudioSource>();
        songBpm = 140;
        secPerBeat = 60f / songBpm;
        dspSongTime = (float)AudioSettings.dspTime;
        currentHitLOL=0;

        //Fetch the GameObject's Renderer component
        m_ObjectRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {  
        songPosition = (float)(AudioSettings.dspTime - dspSongTime);
        //Debug.Log(songPosition);

        float oldSongPositionInBeats = songPositionInBeats;
        //determine how many beats since the song started
        songPositionInBeats = songPosition / secPerBeat;

        float yeet = (songPositionInBeats - currentHitLOL)/beatDiff;

        transform.localScale = Vector3.Lerp((baseScale*scale), targetScale, yeet);
        //Debug.Log(yeet);
        Color textureColor = m_ObjectRenderer.material.color;
        textureColor.a = yeet;
        m_ObjectRenderer.material.color = textureColor;
    }
    
    private void ConductorBehavior_OnHitStart(Hit hit){
        Debug.Log("start hit on beat: " + hit.BeatStart);
        
        
        beatDiff = hit.BeatEnd - hit.BeatStart;
        currentHitLOL = hit.BeatStart;


        gameObject.transform.localScale = baseScale*scale;
        gameObject.SetActive(true);
        targetScale = baseScale;
        
        
    } 

    private void ConductorBehavior_OnHitEnd(Hit hit){
        Debug.Log("end hit on beat: " + hit.BeatEnd);
        gameObject.SetActive(false);
        

    }

    
}