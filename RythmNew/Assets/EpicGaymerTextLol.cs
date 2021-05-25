using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class  EpicGaymerTextLol : MonoBehaviour
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
        m_TextComponent.text = "";
    }

    void Update() {
        songPosition = (float)(AudioSettings.dspTime - dspSongTime);
        //Debug.Log(songPosition);
        float oldSongPositionInBeats = songPositionInBeats;
        //determine how many beats since the song started
        songPositionInBeats = songPosition / secPerBeat;
        

        if(runText){
            Kewlade = songPositionInBeats;
            runText = false;
        }

        if(Kewlade<=songPositionInBeats - 1 ){
            m_TextComponent.text = "";
        }
    }

    // Update is called once per frame
    private void ConductorBehavior_OnHitStart(Hit hit){
        
    } 

    private void ConductorBehavior_OnWinHitEnd(Hit hit){
        VertexGradient textGradient = GetComponent<TMP_Text>().colorGradient;
        textGradient.bottomLeft = new Color32(11, 255, 25, 255);
        textGradient.bottomRight = new Color32(11, 255, 25, 255);
        textGradient.topLeft = new Color32(0, 100, 10, 255);
        textGradient.topRight = new Color32 (0, 100, 10, 255);
        GetComponent<TMP_Text>().colorGradient = textGradient;
        runText = true;
        m_TextComponent.text = "Pass";
    }

    private void ConductorBehavior_OnFailHitEnd(Hit hit){
        //Debug.Log("end hit on beat: " + hit.BeatEnd);
        VertexGradient textGradient = GetComponent<TMP_Text>().colorGradient;
        textGradient.bottomLeft = new Color32(45, 0, 0, 255);
        textGradient.bottomRight = new Color32(55, 0, 10, 255);
        textGradient.topLeft = new Color32(70, 0, 0, 255);
        textGradient.topRight = new Color32 (255, 0, 10, 255);
        GetComponent<TMP_Text>().colorGradient = textGradient;
        runText = true;
        m_TextComponent.text = "Trash   ";
    }
}
