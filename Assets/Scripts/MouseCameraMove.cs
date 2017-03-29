using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCameraMove : MonoBehaviour {

    public float MetersPerSecond = 10;
    public float RotateSensitivity = 30;

    string _xAxis = "Horizontal";
    string _yAxis = "Vertical";
    string _rotatekey = "Fire2";
    string _mouseX = "Mouse X";
    string _mouseY = "Mouse Y";

    public Camera MainCamera; 

    void Update () {
        Vector3 pos = transform.position;
        float x = pos.x + Input.GetAxis(_xAxis) * Time.deltaTime * MetersPerSecond;
        float y = 0; //TODO: Set to terrain height with raycast
        float z = pos.z + Input.GetAxis(_yAxis) * Time.deltaTime * MetersPerSecond;
        transform.position = new Vector3(x, y, z);

        if (Input.GetAxis(_rotatekey) >= 1)
        {
            Transform cameraTransform = MainCamera.transform;
            float camPitch = -Input.GetAxis(_mouseY) * RotateSensitivity;
            float baseYaw = Input.GetAxis(_mouseX) * RotateSensitivity;
            Vector3 camEuler = cameraTransform.localEulerAngles;
            cameraTransform.Rotate(cameraTransform.right, camPitch * Time.deltaTime);
            Vector3 newCamEuler = camEuler + new Vector3(camPitch * Time.deltaTime, 0, 0);
            cameraTransform.localEulerAngles = newCamEuler; //Camera gets pitch
            transform.localEulerAngles += new Vector3(0, baseYaw * Time.deltaTime, 0); //Base gets yaw
        }
        
        
	}
}
