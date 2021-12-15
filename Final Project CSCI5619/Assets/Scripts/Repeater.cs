using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repeater : LaserPointer
{
    public Material onMaterial;
    public Material offMaterial;
    private bool hit;


    // Start is called before the first frame update
    void Start()
    {
        laserPointer.enabled = false;
        lineRendererMaterial = laserPointer.material;
 
        laserPointer.SetPosition(1, new Vector3(0, 0, 100));
    }

    // Update is called once per frame
    void Update()
    {
        Renderer rend = GetComponent<Renderer>();
        if(hit){
            rend.material = onMaterial;
            laserPointer.enabled = true;
        } else {
            rend.material = offMaterial;
            laserPointer.enabled = false;
        }
        base.Update();
    }
    
    public void setHit(bool newHit){
        this.hit = newHit;
    }
}
