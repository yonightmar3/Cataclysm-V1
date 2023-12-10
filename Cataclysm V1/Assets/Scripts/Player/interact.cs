using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interact : MonoBehaviour
{
    public Camera mainCam;
    public GameObject pickUpText;
    public GameObject lookingAt;
    public GameObject bracelet;
    public GameObject book;

    public bool bookUnlocked;
    public bool bottleUnlocked;


    public GameObject bookIcon;
    public GameObject bottleIcon;


    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        pickUpText.SetActive(false);
        Vector3 cameraDirection = mainCam.transform.forward;
        if (Physics.Raycast(mainCam.transform.position, cameraDirection, out RaycastHit hitInfo, 2.5f))
        {
            lookingAt = hitInfo.transform.gameObject;

            if (hitInfo.transform.gameObject.name == "bracelet")
            {
                pickUpText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    lookingAt.SetActive(false);
                    bracelet.SetActive(true);
                    pickUpText.SetActive(false);
                }
            }
            //else pickUpText.SetActive(false);

            else if (hitInfo.transform.gameObject.name == "book")
            {
                pickUpText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    lookingAt.SetActive(false);
                    //book.SetActive(true);
                    bookUnlocked = true;
                    pickUpText.SetActive(false);
                    bookIcon.SetActive(true);
                }
            }

            else if (hitInfo.transform.gameObject.name == "bottle")
            {
                pickUpText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    lookingAt.SetActive(false);
                    //book.SetActive(true);
                    bottleUnlocked = true;
                    pickUpText.SetActive(false);
                    bottleIcon.SetActive(true);
                }
            }
            else pickUpText.SetActive(false);
        }
    }
}
