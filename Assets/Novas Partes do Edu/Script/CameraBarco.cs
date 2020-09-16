using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBarco : MonoBehaviour
{
    public Transform focusCamera;
    public Transform Ponto;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LookAt();
    }

    private void LookAt()
    {
        Vector3 positionCamera = new Vector3(focusCamera.position.x, transform.position.y, focusCamera.position.z);
        Quaternion newRotation = Quaternion.LookRotation(positionCamera - transform.position);
    }
}
