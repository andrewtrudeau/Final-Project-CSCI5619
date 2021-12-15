using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GOGO : MonoBehaviour
{
    // This is the threshold for scaling the distance of the controller (anything below is linear, above is a capped exponential)
    public float d = 0.2F;
    public GameObject headset;
    public GameObject controller;
    public InputActionProperty primary;
    public InputActionProperty secondary;
    private bool on = true;

    // Start is called before the first frame update
    void Start()
    {
        primary.action.performed += ToggleOn;
        secondary.action.performed += ToggleOn;
    }

    private void OnDestroy()
    {
        primary.action.performed -= ToggleOn;
        secondary.action.performed -= ToggleOn;
    }

    // Update is called once per frame
    void Update()
    {
        if(on){
            float distance = Vector3.Distance(this.transform.position, headset.transform.position); 

            if(distance > d){

                controller.transform.localPosition = Vector3.forward.normalized * (Mathf.Pow((distance-d+1),8)-1);

            }else{
                controller.transform.localPosition = new Vector3(0,0,0);
            }
        }
    }

    public void ToggleOn(InputAction.CallbackContext context){
        this.on = !this.on;
        controller.transform.localPosition = new Vector3(0,0,0);
    }

}
