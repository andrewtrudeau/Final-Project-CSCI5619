using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maluable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addScale(Vector3 scale) {
        gameObject.transform.localScale += scale;
    }

    public void setRotation(Quaternion rotation) {
        gameObject.transform.rotation = rotation;
    }
}
