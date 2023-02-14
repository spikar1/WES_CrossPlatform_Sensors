using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using Gyroscope = UnityEngine.InputSystem.Gyroscope;

public class Sensors : MonoBehaviour
{
    [SerializeField]
    MeshRenderer sphereRenderer, proxySensor;

    bool throwStarted;

    Vector3 throwValue;
    private bool obstructed;

    Vector2 touchPos;

    [SerializeField]
    Transform[] touchVisualizers;
    private Vector3 proxyPos;

    private IEnumerator Start()
    {

        yield return new WaitForSeconds(1);
        InputSystem.EnableDevice(GravitySensor.current);
        InputSystem.EnableDevice(Gyroscope.current);
        InputSystem.EnableDevice(LinearAccelerationSensor.current);
        InputSystem.EnableDevice(ProximitySensor.current);

        proxyPos = proxySensor.transform.position;

        FrontCamera();

    }
    private void Update()
    {
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
                throwStarted = false;
            }
        }

        if(Input.touchCount > 0)
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
                item.position = Vector3.positiveInfinity;
            }
        }
        

        if (ProximitySensor.current.distance.ReadValue() > 0)
            obstructed = true;
        else
            obstructed = false;

        proxySensor.material.color = obstructed ? Color.white : Color.green;
        proxySensor.transform.position = proxyPos + LinearAccelerationSensor.current.acceleration.ReadValue();


    }

    void FrontCamera()
    {
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

        proxySensor.material.mainTexture = webcamTexture;
        webcamTexture.Play();
    }
    private void OnGUI()
    {
        GUI.Label(new Rect(100, 100, 300, 100), GravitySensor.current.enabled + "");
        GUI.Label(new Rect(100, 125, 300, 100), GravitySensor.current.gravity.ReadValue() + "");
        GUI.Label(new Rect(100, 150, 300, 100), "ThrowVal: " + throwValue);
        GUI.Label(new Rect(100, 175, 300, 100), "ThrowMag: " + throwValue.magnitude);
    }
}
