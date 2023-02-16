using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using Gyroscope = UnityEngine.InputSystem.Gyroscope;

public class Sensors : MonoBehaviour
{
    [SerializeField]
    Transform[] touchVisualizers;

    [SerializeField]
    Transform gravityBall;

    [SerializeField]
    Transform gyroCube;

    [SerializeField]
    MeshRenderer sphereRenderer, LinearSensor;
 
    private void Start()
    {
        InputSystem.EnableDevice(GravitySensor.current);
        InputSystem.EnableDevice(Gyroscope.current);
        InputSystem.EnableDevice(LinearAccelerationSensor.current);
        InputSystem.EnableDevice(Accelerometer.current);
        InputSystem.EnableDevice(ProximitySensor.current);
        InputSystem.EnableDevice(LightSensor.current);
    }

    private void Update()
    {
        var proxyOffset = LinearAccelerationSensor.current.acceleration.ReadValue();
        LinearSensor.transform.position = proxyPos + proxyOffset;




        gyroCube.Rotate(Gyroscope.current.angularVelocity.ReadValue(), Space.World);

        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                var touch = Input.GetTouch(i);
                var worldPos = Camera.main.ScreenToWorldPoint(touch.position);
                touchVisualizers[touch.fingerId].position = worldPos + Vector3.forward;
            }
        }
        else
        {
            foreach (var item in touchVisualizers)
            {
                item.position = Vector3.one * 1000;
            }
        }

        if(GravitySensor.current != null)
        {
            var gravity = GravitySensor.current.gravity.ReadValue();
            gravity.z *= -1;
            gravityBall.position = gravity;
            gravityBall.localScale = Vector3.one * (1 + (0.5f * GravitySensor.current.gravity.ReadValue().z));

        }

        Foo();
    }



    bool throwStarted;

    Vector3 throwValue;
    private bool obstructed;

    Vector2 touchPos;

    private Vector3 proxyPos;



        void Foo(){ 

        if (GravitySensor.current == null || ProximitySensor.current == null || LinearAccelerationSensor.current == null)
            return;
        var gravity = GravitySensor.current.gravity.ReadValue();
        var up = new Vector3(-gravity.x, -gravity.y, 0);


        transform.up = up;

        if(gravity.x > 0)
        {
            sphereRenderer.material.color = Color.green;
            throwStarted = true;
        }
        else
        {
            sphereRenderer.material.color = Color.red;
            if(throwStarted)
            {
                throwValue = Gyroscope.current.angularVelocity.ReadValue();
                Debug.Log("Ang vel - " + Gyroscope.current.angularVelocity.ReadValue());
                Debug.Log("Magniture - " + Gyroscope.current.angularVelocity.ReadValue().magnitude);
                throwStarted = false;
            }
        }

        

        if (ProximitySensor.current.distance.ReadValue() > 0)
            obstructed = true;
        else
            obstructed = false;

        LinearSensor.material.color = obstructed ? Color.white : Color.green;






    }

    void FrontCamera()
    {
        return;
        Application.RequestUserAuthorization(UserAuthorization.WebCam);
        string frontCamName = null;
        var webCamDevices = WebCamTexture.devices;
        foreach (var camDevice in webCamDevices)
        {
            if (camDevice.isFrontFacing)
            {
                frontCamName = camDevice.name;
                break;
            }
        }

        WebCamTexture webcamTexture = new WebCamTexture(frontCamName);

        LinearSensor.material.mainTexture = webcamTexture;
        webcamTexture.Play();
    }

    private void OnGUI()
    {
        int YPosition = 0;
        GUI.Label(new Rect(100, (YPosition++) * 25, 300, 100), "Gravity: " + GravitySensor.current?.gravity.ReadValue() + "");
        GUI.Label(new Rect(100, (YPosition++) * 25, 300, 100), "Linear Acceleration: " + LinearAccelerationSensor.current?.acceleration.ReadValue() + "");
        GUI.Label(new Rect(100, (YPosition++) * 25, 300, 100), "Acceleration: " + Accelerometer.current?.acceleration.ReadValue() + "");
        GUI.Label(new Rect(100, (YPosition++) * 25, 300, 100), "Gyroscope: " + Gyroscope.current?.angularVelocity.ReadValue() + "");
        GUI.Label(new Rect(100, (YPosition++) * 25, 300, 100), "Light Sensor: " + LightSensor.current?.lightLevel.ReadValue() + " lux");
    }

}
