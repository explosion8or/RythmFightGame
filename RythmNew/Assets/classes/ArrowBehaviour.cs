using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArrowBehaviour : MonoBehaviour
{

    
    public float speed = 3f;
    public int scale = 15;

    private Vector3 targetScale;
    private Vector3 baseScale;

    GameObject conductor;
    // Start is called before the first frame update
    void Start()
    {
        conductor = GameObject.Find("Conductor");
        ConductorBehavior conductorBehavior = conductor.GetComponent<ConductorBehavior>();

        conductorBehavior.OnHitEnd += ConductorBehavior_OnHitEnd;
        conductorBehavior.OnHitStart += ConductorBehavior_OnHitStart;
        //gameObject.SetActive(false);

        baseScale = gameObject.transform.localScale;
        gameObject.transform.localScale = baseScale*scale;
        targetScale = baseScale*scale;
    }

    // Update is called once per frame
    void Update()
    {  
        transform.localScale = Vector3.Lerp (gameObject.transform.localScale, targetScale, speed * Time.deltaTime);

    }
    
    private void ConductorBehavior_OnHitStart(Hit hit){
        Debug.Log("start hit on beat: " + hit.BeatStart);
        

        gameObject.transform.localScale = baseScale*scale;
        //gameObject.SetActive(true);
        targetScale = baseScale;
    } 

    private void ConductorBehavior_OnHitEnd(Hit hit){
        Debug.Log("end hit on beat: " + hit.BeatEnd);
        //gameObject.SetActive(false);
        

    }

    
}