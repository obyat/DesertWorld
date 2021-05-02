using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Transform pivot;
    public Vector3 offset;

    public bool useOffsetValues;

    public float rotateSpeed;
    public float maxViewAngle;
    public float minViewAngle;

    public bool inverseY;

    public bool enabledInput;
    public float startTimer;
    // Start is called before the first frame update
    void Start()
    {
        if(!useOffsetValues)
        {
            offset = target.position - transform.position;
        }
        pivot.transform.position = target.transform.position;
        pivot.transform.parent = null;

        Cursor.lockState = CursorLockMode.Locked;

        enabledInput = false;
        startTimer = 0.1f;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(enabledInput)
        {
            // Get the X position of the mouse & rotate the target
            float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
            pivot.Rotate(0, horizontal, 0);

            // Get the Y postion of the mouse & rotate the pivot
            float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
            // pivot.Rotate(vertical, 0, 0);
            if(inverseY) {
                pivot.Rotate(vertical, 0, 0);
            }
            else {
                pivot.Rotate(-vertical, 0, 0);
            }
            // Limit Up/down camera rotation
            if(pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f) 
            {
                pivot.rotation = Quaternion.Euler(maxViewAngle, 0, 0);
            }
            if(pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 360f + minViewAngle) 
            {
                pivot.rotation = Quaternion.Euler(360f + minViewAngle, 0, 0);
            }
            // Move the camera based on the current rotation of the target & original offset.
            float desiredYAngle = pivot.eulerAngles.y;
            float desiredXAngle = pivot.eulerAngles.x;

            Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
            transform.position = target.position - (rotation * offset);

            if(transform.position.y < target.transform.position.y) {
                transform.position = new Vector3(transform.position.x, target.position.y - 0.5f, transform.position.z);
            }

            transform.LookAt(target);
        } 
    }
    void Update()
    {
        if (startTimer > 0){
            startTimer -= Time.deltaTime;
        } else {
            enabledInput = true;
        }
    }
    public void setEnableInput(bool b)
    {
        enabledInput = b;
    }
}
