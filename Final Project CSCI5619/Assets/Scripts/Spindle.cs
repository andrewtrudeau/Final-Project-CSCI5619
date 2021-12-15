using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spindle : MonoBehaviour
{

    public GameObject leftController;
    public GameObject rightController;
    public GameObject cube;
    Grabbable grabbedObject;
    public InputActionProperty primary;
    public InputActionProperty secondary;
    private bool on = false;
    private bool grabbing = false;
    public InputActionProperty grabAction;
    public Material mutedRepeater;

    Maluable maluable; 
    GameObject ghost; 

    public AudioSource grabAudio;
    public AudioSource ungrabAudio;

    private Quaternion lastSpindleRotation;
    float dist;

    // Start is called before the first frame update
    void Start()
    {
        dist = 1F;
        lastSpindleRotation = Quaternion.LookRotation(leftController.transform.position - leftController.transform.position);
        //primary.action.performed += ToggleOn;
        //secondary.action.performed += ToggleOn;

        grabAction.action.performed += Grab;
        grabAction.action.canceled += GrabStop;
    }

    private void OnDestroy()
    {
        //primary.action.performed -= ToggleOn;
        //secondary.action.performed -= ToggleOn;
        grabAction.action.performed -= Grab;
        grabAction.action.canceled -= GrabStop;
    }

    // Update is called once per frame
    void Update()
    {
        LineRenderer lineRender = this.GetComponent<LineRenderer>();

        if(on){

            // Render line
            var points = new Vector3[2];
            points[0] = leftController.transform.position;
            points[1] = rightController.transform.position;
            lineRender.SetPositions(points);

            Vector3 C1toC2 = points[1] - points[0]; 
            Vector3 C2toC1 = points[0] - points[1]; 

            Quaternion spindleRotation = Quaternion.LookRotation(leftController.transform.position - rightController.transform.position);

            Quaternion rotationChange = spindleRotation * Quaternion.Inverse(lastSpindleRotation);

            // Cube and grabbed obj position
            cube.transform.position = leftController.transform.position + ((points[1]-points[0])/2) - new Vector3(0,0.01F,0);

            if(maluable != null){
                ghost.transform.position = leftController.transform.position + ((points[1]-points[0])/2) - new Vector3(0,0.01F,0);
                ghost.transform.localScale = maluable.gameObject.transform.localScale * 0.15f;
            }

            if(grabbing && maluable != null){
                //grabbedObject.transform.position = cube.transform.position;

                if(dist == 0)
                    dist = 0.000001F;

                // Chnage in distance and angle
                float distChange = Vector3.Distance(points[1], points[0])-dist;
                
                maluable.addScale(new Vector3(distChange,distChange,distChange));
                maluable.setRotation(rotationChange * grabbedObject.transform.rotation);

            }

            dist = Vector3.Distance(points[1], points[0]);
            lastSpindleRotation = spindleRotation;

        }else{
            lineRender.enabled = false;
            cube.GetComponent<MeshRenderer>().enabled = false;
        }

    }

    public void ToggleOn(Maluable maluable){
        this.maluable = maluable;
        if(maluable != null){
            ghost = GameObject.Instantiate(this.maluable.gameObject);
            ghost.transform.localScale *= 0.15f;
            ghost.GetComponent<Renderer>().material = mutedRepeater;
            if(ghost.GetComponent<CapsuleCollider>())
                ghost.GetComponent<CapsuleCollider>().enabled = false;
            this.on = true;
            LineRenderer lineRender = this.GetComponent<LineRenderer>();
            lineRender.enabled = true;
            cube.GetComponent<MeshRenderer>().enabled = true;
        }else {
            this.on = false;
            Destroy(ghost);
        }
    }

    void Grab(InputAction.CallbackContext context)
    {
        grabbing = true;
        grabAudio.Play();
    }

    void GrabStop(InputAction.CallbackContext context)
    {
        grabbing = false;
        ungrabAudio.Play();
    }

    public void setGrabbedObject(Grabbable obj){
        this.grabbedObject = obj;
    }
}
