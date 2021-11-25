////////////////////////////////////////////////////////////
// File: CameraController.cs
// Author: Jack Peedle
// Date Created: 02/11/21
// Last Edited By: Jack Peedle
// Date Last Edited: 12/11/21
// Brief: Camera controller to limit the player to a isometric view and boundaries of the map
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

    // vector 2 for the screen limit
    public Vector2 screenLimit;

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

        // clamp the x position 
        pos.x = Mathf.Clamp(pos.x, -screenLimit.x, screenLimit.x);

        // clamp the z position
        pos.z = Mathf.Clamp(pos.z, -screenLimit.y, screenLimit.y);

        // transform.position is = to pos
        transform.position = pos;

    }

}
