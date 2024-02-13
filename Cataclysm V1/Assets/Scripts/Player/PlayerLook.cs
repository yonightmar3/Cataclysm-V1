using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerLook : MonoBehaviour
{

    [SerializeField] private Animator arm;
    [SerializeField] private GameObject lightOrb;
    //private bool lightOn = false;
    public static bool lightOn = false;
    public static bool orbDeployed = false;



    private Camera cam;
    [SerializeField] private Camera paganiaCam;
    [SerializeField] private Camera mainCam;

    public static bool pagania;

    private float xRotation = 0f;

    public GameObject menu;
    private bool isMenuActive = false;
    private GameObject menuCanvas;


    private float sensitivity;

    public float xSensitivity = 30f;
    public float ySensitivity = 30f;

    [SerializeField] private Slider sensitivitySlider;

    public RaycastHit HitInfoFar { get; private set; }
    public RaycastHit HitInfoClose { get; private set; }


    private void Start()
    {
        cam = mainCam;
        Cursor.visible = false;

        if (PlayerPrefs.HasKey("sensitivity"))
        {
            LoadSensitivity();
        }

        menuCanvas = GameObject.Find("Menu Canvas");
    }

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }

    /*    private void Update()
        {
            Vector3 cameraDirection = cam.transform.forward;
            //Physics.Raycast(cam.transform.position, cameraDirection, out RaycastHit hitInfo, 20f);
        }*/

    // Update is called once per frame
    void Update()
    {
        //F KEY TOGGLES THE LIGHT
        if (Input.GetKeyDown(KeyCode.F) && !deployLightOrb.animationStart)
        {
            //turn light off
            if (lightOn)
            {
                arm.SetTrigger("lightOff");
                lightOrb.SetActive(false);
            }
            //turn light on
            else
            {
                arm.SetTrigger("lightOn");
                //StartCoroutine(lightOut());
            }

            lightOn = !lightOn;
        }

        if (pagania)
        {
            cam = paganiaCam;
            Debug.Log("pagania cam");
        }
        else cam = mainCam;

        Vector3 cameraDirection = cam.transform.forward;

        // Declare a local RaycastHit variable
        RaycastHit hitFar;
        RaycastHit hitClose;


        // Perform the raycast and assign the result to the local variable
        if (Physics.Raycast(cam.transform.position, cameraDirection, out hitFar, 20f))
        {
            // Assign the local variable to the class property
            HitInfoFar = hitFar;
        }
        else
        {
            // If no hit, set the HitInfo property to null or some default value
            HitInfoFar = new RaycastHit(); // You might want to set a default value based on your requirements
        }

        if (Physics.Raycast(cam.transform.position, cameraDirection, out hitClose, 1.5f))
        {
            // Assign the local variable to the class property
            HitInfoClose = hitFar;
        }
        else
        {
            // If no hit, set the HitInfo property to null or some default value
            HitInfoClose = new RaycastHit(); // You might want to set a default value based on your requirements
        }
    }

    private void LoadSensitivity()
    {
        sensitivitySlider.value = PlayerPrefs.GetFloat("sensitivity");
        SetSensitivity();
    }

    public void SetSensitivity()
    {
        sensitivity = sensitivitySlider.value;
        PlayerPrefs.SetFloat("musicVolume", sensitivity);
    }

    IEnumerator lightOut()
    {
        yield return new WaitForSeconds(1.6f);
        lightOrb.SetActive(true);
    }


}
