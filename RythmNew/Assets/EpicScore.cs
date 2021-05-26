using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class  EpicScore : MonoBehaviour
{
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
    public float Kewlade = 1000;
    public bool runText;
    private TMP_Text m_TextComponent;
    public int currentScore;
    
    // Start is called before the first frame update
    void Start()
    {
        
        conductor = GameObject.Find("Conductor");
        ConductorBehavior conductorBehavior = conductor.GetComponent<ConductorBehavior>();

        conductorBehavior.OnWinHitEnd += ConductorBehavior_OnWinHitEnd;
        conductorBehavior.OnFailHitEnd += ConductorBehavior_OnFailHitEnd;
        conductorBehavior.OnHitStart += ConductorBehavior_OnHitStart;

        
        musicSource = GetComponent<AudioSource>();
        songBpm = 140;
        secPerBeat = 60f / songBpm;
        dspSongTime = (float)AudioSettings.dspTime;
        runText = false;

        
        m_TextComponent = GetComponent<TMP_Text>();
        currentScore = 0;
        m_TextComponent.text = "score is " + currentScore;
    }

    void Update() {
        
    }

    // Update is called once per frame
    private void ConductorBehavior_OnHitStart(Hit hit){
        
    } 

    private void ConductorBehavior_OnWinHitEnd(Hit hit){
        currentScore += 100;
        m_TextComponent.text = "score is " + currentScore;
        
    }

    private void ConductorBehavior_OnFailHitEnd(Hit hit){
        currentScore -= 50;
        m_TextComponent.text = "score is " +  currentScore;
    }
}
