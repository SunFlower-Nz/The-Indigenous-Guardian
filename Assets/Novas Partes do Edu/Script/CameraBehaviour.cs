using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    // The target we are following
    public Transform target;
    // The distance in the x-z plane to the target
    private float distance = 10.0f;
    public float maxDistance = 10.0f;
    public float minDistance = 4.0f;
    // the height we want the camera to be above the target
    private float height = 5.0f;
    public float minHeight = -1;
    public float maxHeight = 10.0f;
    // How musc we
    public float heightDamping = 5.0f;
    public float rotationDaping = 3.0f;

    public Vector3 sensibility;

    private void LateUpdate()
    {
        // Early out if we don't have a target
        if (!target)
            return;

        //Calculate new distance
        distance -= Input.GetAxis("Mouse ScrollWheel")*sensibility.z;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        //calculate new Height
        height -= Input.GetAxis("Mouse Y") * sensibility.y;
        height = Mathf.Clamp(height, minHeight, maxDistance);

        // Calculate the current rotation angles
        float wantedRotationAngle = target.eulerAngles.y;
        float wantedHeight = target.position.y + height;

        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        // Damp the rotation around the y-axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDaping * Time.deltaTime);

        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        // Convert the angle into a rotation
        Quaternion currentRotation = Quaternion.Euler(0, transform.eulerAngles.y + Input.GetAxis("Mouse X") * sensibility.x, 0);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        transform.position = target.position;
        transform.position -= currentRotation * Vector3.forward * distance;

        // set the height of the camera
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

        //Alvays look at the target
        transform.LookAt(target);
    }
}
