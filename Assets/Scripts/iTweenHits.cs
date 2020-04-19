using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iTweenHits : MonoBehaviour
{
    GameObject cameraHolder;

    public float cameraShakeX;
    public float cameraShakeY;

    public float shakeTime;

    // Start is called before the first frame update
    void Start()
    {
        cameraHolder = GameObject.FindGameObjectWithTag("PlayerCamera");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CustomImpactShake()
    {
        iTween.ShakePosition(cameraHolder, new Vector3(cameraShakeX, cameraShakeX, 0), shakeTime);
    }

    public void CameraLightImpact()
    {
        iTween.ShakePosition(cameraHolder, new Vector3(0.14f, 0.14f, 0), 0.10f);
    }

    public void CameraHeavyImpact()
    {
        iTween.ShakePosition(cameraHolder, new Vector3(0.3f, 0.3f, 0), 0.3f);
    }

}
