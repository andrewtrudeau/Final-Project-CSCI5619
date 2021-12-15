using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reciever : MonoBehaviour
{
    public Material onMaterial;
    public Material offMaterial;
    private bool hit;

    // Start is called before the first frame update
    void Start()
    {
        hit = false;
    }

    // Update is called once per frame
    void Update()
    {
        Renderer rend = GetComponent<Renderer>();
        if(hit){
            rend.material = onMaterial;
        }else {
            rend.material = offMaterial;
        }
        
    }

    public void setHit(bool newHit){
        this.hit = newHit;
    }
}
