////////////////////////////////////////////////////////////
// File: CameraController.cs
// Author: Jack Peedle
// Date Created: 02/11/21
// Last Edited By: Jack Peedle
// Date Last Edited: 02/11/21
// Brief: 
//////////////////////////////////////////////////////////// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    // camera move speed
    public float cameraMoveSpeed = 25f;

    // border thickness
    public float borderThickness = 10f;

    //
    public Vector2 screenLimit;

    //
    //public float scrollSpeed = 2f;

    // Update
    public void Update() {

        // vector 3 position = transform.position
        Vector3 pos = transform.position;

        // if the "W" key is pressed or the mouse position Y is more than or = the screen height - border thickness
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - borderThickness) {

            // position Z += the camera move speed multiplied by time.deltaTime
            pos.z += cameraMoveSpeed * Time.deltaTime;

        }

        // if the "S" key is pressed or the mouse position Y is less than or = the border thickness
        if (Input.GetKey("s") || Input.mousePosition.y <= borderThickness) {

            // position Z -= the camera move speed multiplied by time.deltaTime
            pos.z -= cameraMoveSpeed * Time.deltaTime;

        }

        // if the "D" key is pressed or the mouse position X is more than or = the screen width - border thickness
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - borderThickness) {

            // position X += the camera move speed multiplied by time.deltaTime
            pos.x += cameraMoveSpeed * Time.deltaTime;

        }

        // if the "A" key is pressed or the mouse position X is more than or = the border thickness
        if (Input.GetKey("a") || Input.mousePosition.x <= borderThickness) {

            // position X -= the camera move speed multiplied by time.deltaTime
            pos.x -= cameraMoveSpeed * Time.deltaTime;

        }

        //
        //float scroll = Input.GetAxis("Mouse ScrollWheel");

        //
        //pos.y -= scrollSpeed * 10f * Time.deltaTime;

        //
        pos.x = Mathf.Clamp(pos.x, -screenLimit.x, screenLimit.x);

        //
        pos.z = Mathf.Clamp(pos.z, -screenLimit.y, screenLimit.y);

        // transform.position is = to pos
        transform.position = pos;

    }








    /*

    //
    public GameObject GameObjectToMove;

    [Header("X Values")]
    [SerializeField]
    // Minimum and maximum X value
    private float minXValue;
    [SerializeField]
    private float maxXValue;


    [Header("Y Values")]
    [SerializeField]
    // Minimum and maximum Y value
    private float minYValue;
    [SerializeField]
    private float maxYValue;


    [SerializeField]
    // Camera Speed
    private float cameraSpeed = 10;


    // Update
    public void Update() {

        // get the axis for the horizontal
        float horizontal = Input.GetAxisRaw("Horizontal");

        // get the axis for the vertical
        float vertical = Input.GetAxisRaw("Vertical");

        // vector 3 for direction, X, Y, Z, y = 0 because of the 3D space we don't want the camera movinbg up and down
        // Relative to the worlds space, we want it to move left and right
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // if it is moving in a direction
        if (direction.magnitude >= 0.1f) {


        }

    }

    */

}
