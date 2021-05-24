using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArrowBehaviour : MonoBehaviour
{

    GameObject conductor;
    // Start is called before the first frame update
    void Start()
    {
        conductor = GameObject.Find("Conductor");
        ConductorBehavior conductorBehavior = conductor.GetComponent<ConductorBehavior>();

        conductorBehavior.OnHitEnd += ConductorBehavior_OnHitEnd;
        conductorBehavior.OnHitStart += ConductorBehavior_OnHitStart;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {  
        //gameObject.transform.localScale += new Vector3(1, 1, 0);
    }
    
    private void ConductorBehavior_OnHitEnd(Hit hit){
        Debug.Log("end hit on beat: " + hit.BeatEnd);
    }

    private void ConductorBehavior_OnHitStart(Hit hit){
        Debug.Log("start hit on beat: " + hit.BeatStart);
    }
}