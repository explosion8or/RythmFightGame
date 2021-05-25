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

    public Sprite setSpriteUp;
    public Sprite setSpriteDown;
    public Sprite setSpriteLeft;
    public Sprite setSpriteRight;

    public int currentHitEnd;
    public int currentHitStart;

    private Hit[] hitList;

    public bool yeetFlag;

    
    // Start is called before the first frame update
    void Start()
    {
        conductor = GameObject.Find("Conductor");
        ConductorBehavior conductorBehavior = conductor.GetComponent<ConductorBehavior>();

        m_ObjectRenderer = GetComponent<Renderer>();

        conductorBehavior.OnWinHitEnd += ConductorBehavior_OnWinHitEnd;
        conductorBehavior.OnFailHitEnd += ConductorBehavior_OnFailHitEnd;
        conductorBehavior.OnHitStart += ConductorBehavior_OnHitStart;
        Color textureColor = m_ObjectRenderer.material.color;
        textureColor.a = 0;
        m_ObjectRenderer.material.color = textureColor;

        baseScale = gameObject.transform.localScale;
        gameObject.transform.localScale = baseScale*scale;
        targetScale = baseScale*scale;


        musicSource = GetComponent<AudioSource>();
        songBpm = 140;
        secPerBeat = 60f / songBpm;
        dspSongTime = (float)AudioSettings.dspTime;
        currentHitLOL=0;

        //Fetch the GameObject's Renderer component
        

        hitList = TextRead.IntakeHits();
        currentHitEnd = 0;
        currentHitStart = 0;
    }

    // Update is called once per frame
    void Update()
    {  
        
        if(currentHitStart < hitList.Length){
            if(hitList[currentHitStart].BeatStart<=songPositionInBeats){
                Color textureColorJ = m_ObjectRenderer.material.color;
                textureColorJ.a = 1;
                m_ObjectRenderer.material.color = textureColorJ;
                currentHitStart++;
                yeetFlag = true;
            }
        }


        if(currentHitStart < hitList.Length){
            if(hitList[currentHitEnd].BeatEnd<=songPositionInBeats && currentHitEnd <= hitList.Length){
                Color textureColorH = m_ObjectRenderer.material.color;
                textureColorH.a = 0;
                m_ObjectRenderer.material.color = textureColorH;
                currentHitEnd++;
                yeetFlag = false;
            }
        }

        

        
        songPosition = (float)(AudioSettings.dspTime - dspSongTime);
        //Debug.Log(songPosition);

        float oldSongPositionInBeats = songPositionInBeats;
        //determine how many beats since the song started
        songPositionInBeats = songPosition / secPerBeat;

        if(yeetFlag){
            float yeet = (songPositionInBeats - currentHitLOL)/beatDiff;
            transform.localScale = Vector3.Lerp((baseScale*scale), targetScale, yeet);
            //Debug.Log(yeet);
            Color textureColor = m_ObjectRenderer.material.color;
            textureColor.a = yeet-.3f;
            m_ObjectRenderer.material.color = textureColor;
        }
    }
    
    private void ConductorBehavior_OnHitStart(Hit hit){
        //Debug.Log("start hit on beat: " + hit.BeatStart);
        
        
        if(((int) hit.Direction) == 0){
            //up
            //Debug.Log("up");
            this.GetComponent<SpriteRenderer>().sprite = setSpriteUp;
        }else if(((int) hit.Direction) == 1){
            //down
            //Debug.Log("down");
            this.GetComponent<SpriteRenderer>().sprite = setSpriteDown;
        }else if(((int) hit.Direction) == 3){
            //left
            this.GetComponent<SpriteRenderer>().sprite = setSpriteLeft;
        }else if(((int) hit.Direction) == 2){
            //right
            this.GetComponent<SpriteRenderer>().sprite = setSpriteRight;
        }
        
        beatDiff = hit.BeatEnd - hit.BeatStart;
        currentHitLOL = hit.BeatStart;


        gameObject.transform.localScale = baseScale*scale;
        targetScale = baseScale;
        
        
        
    } 

    private void ConductorBehavior_OnFailHitEnd(Hit hit){
        Debug.Log("Failed a beat");
        
    }
    private void ConductorBehavior_OnWinHitEnd(Hit hit){
        //Debug.Log("end hit on beat: " + hit.BeatEnd);
        
        
    }
}