using UnityEngine;
using UnityEngine.InputSystem;

using System.Collections;
using System.Collections.Generic;

public class PointGrabber : Grabber
{
    public LineRenderer laserPointer;
    public Material grabbablePointerMaterial;
    public Material selectedPointerMaterial;

    public InputActionProperty touchAction;
    public InputActionProperty grabAction;
    public InputActionProperty joystick;
    public InputActionProperty selectionAction;

    public GameObject spindle;

    public AudioSource selectAudio;
    public AudioSource deselectAudio;

    Material lineRendererMaterial;
    Transform grabPoint;
    Grabbable grabbedObject;
    Transform initialParent;
    Maluable maluable;

    public float deadZone = 0.25F;
    public float maxVelocity = 3.0F;

    // Start is called before the first frame update
    void Start()
    {
        laserPointer.enabled = false;
        lineRendererMaterial = laserPointer.material;

        grabPoint = new GameObject().transform;
        grabPoint.name = "Grab Point";
        grabPoint.parent = this.transform;
        grabbedObject = null;
        initialParent = null;

        grabAction.action.performed += Grab;
        grabAction.action.canceled += Release;

        touchAction.action.performed += TouchDown;
        touchAction.action.canceled += TouchUp;
        
        selectionAction.action.performed += Select;
        
        joystick.action.performed += MoveObject;

    }

    private void OnDestroy()
    {
        grabAction.action.performed -= Grab;
        grabAction.action.canceled -= Release;

        touchAction.action.performed -= TouchDown;
        touchAction.action.canceled -= TouchUp;

        selectionAction.action.performed -= Select;

        joystick.action.performed -= MoveObject;

    }

    // Update is called once per frame
    void Update()
    {
        if (laserPointer.enabled)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                laserPointer.SetPosition(1, new Vector3(0, 0, hit.distance));

                if (maluable != null && maluable.gameObject.GetComponent<Collider>() == hit.collider){
                    laserPointer.material = selectedPointerMaterial;
                } else if (hit.collider.GetComponent<Grabbable>()) {
                    laserPointer.material = grabbablePointerMaterial;
                } else {
                    laserPointer.material = lineRendererMaterial;
                }
            }
            else
            {
                laserPointer.SetPosition(1, new Vector3(0, 0, 100));
                laserPointer.material = lineRendererMaterial;
            }
        }
    }

    public override void Grab(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            if (hit.collider.GetComponent<Grabbable>())
            {
                grabPoint.localPosition = new Vector3(0, 0, hit.distance);

                if (hit.collider.GetComponent<Grabbable>().GetCurrentGrabber() != null)
                {
                    hit.collider.GetComponent<Grabbable>().GetCurrentGrabber().Release(new InputAction.CallbackContext());
                }

                grabbedObject = hit.collider.GetComponent<Grabbable>();
                grabbedObject.SetCurrentGrabber(this);

                if (grabbedObject.GetComponent<Rigidbody>())
                {
                    grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                    grabbedObject.GetComponent<Rigidbody>().useGravity = false;
                }

                initialParent = grabbedObject.transform.parent;
                grabbedObject.transform.parent = grabPoint;
            }
        }
    }

    public override void Release(InputAction.CallbackContext context)
    {
        if (grabbedObject)
        {
            if (grabbedObject.GetComponent<Rigidbody>())
            {
                grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
                grabbedObject.GetComponent<Rigidbody>().useGravity = true;
            }

            grabbedObject.transform.parent = initialParent;
            grabbedObject = null;
        }
    }

    // AHH
    void Select(InputAction.CallbackContext context)
    {
        if(grabbedObject == null) {
            deselectAudio.Play();
            maluable = null;
            spindle.GetComponent<Spindle>().ToggleOn(null);
            return;
        }
        if(grabbedObject.gameObject.GetComponent<Maluable>()){
            selectAudio.Play();
            maluable = grabbedObject.gameObject.GetComponent<Maluable>();
            spindle.GetComponent<Spindle>().ToggleOn(maluable);
        }
    }
    
    void TouchDown(InputAction.CallbackContext context)
    {
        laserPointer.enabled = true;
    }

    void TouchUp(InputAction.CallbackContext context)
    {
        laserPointer.enabled = false;
    }

    public void MoveObject(InputAction.CallbackContext context) {

        if(maxVelocity > 0){
            Vector2 inputAxes = context.action.ReadValue<Vector2>();  

            if(grabPoint != null)

            if(inputAxes.y >= deadZone || inputAxes.y <= -deadZone){
                float velocity = maxVelocity * inputAxes.y * Time.deltaTime;

                Vector3 dir = Vector3.forward;
                grabPoint.Translate(dir.normalized*velocity);

            }
        }
      
    }


}
