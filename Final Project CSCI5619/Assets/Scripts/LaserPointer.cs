using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    protected Material lineRendererMaterial;
    public LineRenderer laserPointer;
    private Collider lastHit;

    // Start is called before the first frame update
    void Start()
    {
        laserPointer.enabled = true;
        lineRendererMaterial = laserPointer.material;
 
        laserPointer.SetPosition(1, new Vector3(0, 0, 100));

    }

    // Update is called once per frame
    protected void Update()
    {
        if (laserPointer.enabled)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                if(lastHit !=null && hit.collider != lastHit){
                    if (lastHit.GetComponent<Reciever>())
                        lastHit.GetComponent<Reciever>().setHit(false);
                    else if (lastHit.GetComponent<Repeater>())
                        lastHit.GetComponent<Repeater>().setHit(false);

                    lastHit = null;
                }

                laserPointer.SetPosition(1, new Vector3(0, 0, hit.distance));

               if (hit.collider.GetComponent<Reciever>())
                {
                    lastHit = hit.collider;
                    hit.collider.GetComponent<Reciever>().setHit(true);
                }
                else if (hit.collider.GetComponent<Repeater>())
                {
                    lastHit = hit.collider;
                    hit.collider.GetComponent<Repeater>().setHit(true);
                }
            }
            else
            {
                laserPointer.SetPosition(1, new Vector3(0, 0, 100));
                //laserPointer.material = lineRendererMaterial;
            }
        }
    }
}
