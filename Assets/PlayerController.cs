using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {
    [Header("General")]
    [Tooltip("In MS^-1")] [SerializeField] float ControlSpeed = 4f;
    [Tooltip("In M")] [SerializeField] float xRange = 5f;
    [Tooltip("In M")] [SerializeField] float yRange = 3f;
    [SerializeField] GameObject[] guns;

    [Header("Screen-Position based")]
    [SerializeField] float PositionPitchFactor = -5f;
    [SerializeField] float ControlPitchFactor = -20f;

    [Header("Control-Throw based")]
    [SerializeField] float PositionYawFactor = 5f;
    [SerializeField] float ControlRollFactor = -20f;

    float xThrow, yThrow;
    bool isControlEnabled = true;


    // Use this for initialization
    void Start () {
		
	}
    // Update is called once per frame
    void Update ()
    {
        if (isControlEnabled == true)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }
    }

    private void ProcessRotation()
    {
        float pitch = transform.localPosition.y * PositionPitchFactor + yThrow*ControlPitchFactor  ;
        float yaw = transform.localPosition.x  * PositionYawFactor ;
        float roll = 0f;

        transform.localRotation = Quaternion.Euler(pitch,yaw,roll);
    }

    void OnPlayerDeath() //called by string reference
    {
        isControlEnabled = false;
    }

    private void ProcessTranslation()
    {
         xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
         yThrow = CrossPlatformInputManager.GetAxis("Vertical");


        float xOffSet = xThrow * ControlSpeed * Time.deltaTime;
        float yOffSet = yThrow * ControlSpeed * Time.deltaTime;

        float RawXPos = transform.localPosition.x + xOffSet;
        float ClampedXPos = Mathf.Clamp(RawXPos, -xRange, xRange);

        float RawYPos = transform.localPosition.y + yOffSet;
        float ClampedYPos = Mathf.Clamp(RawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(ClampedXPos, ClampedYPos, transform.localPosition.z);
    }

    void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            SetGunsActive(true);
        }
        else
        {
            SetGunsActive(false);
        }
    }

    private void SetGunsActive(bool isActive)
    {
        foreach(GameObject gun in guns)
        {
            gun.SetActive(isActive);
        }
    }
  }

