////////////////////////////////////////////////////////////
// File: CameraController.cs
// Author: Jack Peedle
// Date Created: 02/11/21
// Last Edited By: Jack Peedle
// Date Last Edited: 13/12/21
// Brief: Camera controller to limit the player to a isometric view and boundaries of the map
//////////////////////////////////////////////////////////// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    // camera move speed
    public float cameraMoveSpeed = 22f;

    // border thickness
    public float borderThickness = 10f;

    // vector 2 for the screen limit
    public Vector2 screenLimit;

    // min X int
    public int minX;

    // max X int
    public int maxX;

    // min Z int
    public int minZ;

    // max Z int
    public int maxZ;

    // min Y int
    public int minY;

    // max Y int
    public int maxY;

    // On start
    private void Start() {

        // min X value
        minX = -250;

        // max X value
        maxX = 180;

        // min Z value
        minZ = -230;

        // max Z value
        maxZ = 200;

        // min Y value
        minY = -20;

        // max Y value
        maxY = 20;

    }

    // Update
    public void Update() {

        // vector 3 position = transform.position
        Vector3 pos = transform.position;

        // if the "W" key is pressed
        if (Input.GetKey("w")) {

            // position Z += the camera move speed multiplied by time.deltaTime
            pos.z += cameraMoveSpeed * Time.deltaTime;

        }

        // if the "S" key is pressed
        if (Input.GetKey("s")) {

            // position Z -= the camera move speed multiplied by time.deltaTime
            pos.z -= cameraMoveSpeed * Time.deltaTime;

        }

        // if the "D" key is pressed
        if (Input.GetKey("d")) {

            // position X += the camera move speed multiplied by time.deltaTime
            pos.x += cameraMoveSpeed * Time.deltaTime;

        }

        // if the "A" key is pressed
        if (Input.GetKey("a")) {

            // position X -= the camera move speed multiplied by time.deltaTime
            pos.x -= cameraMoveSpeed * Time.deltaTime;

        }

        // if the "Q" key is pressed
        if (Input.GetKey("q")){

            // position Y -= the camera move speed multiplied by time.deltaTime
            pos.y -= cameraMoveSpeed * Time.deltaTime;

        }

        // if the "E" key is pressed
        if (Input.GetKey("e")) {

            // position Y += the camera move speed multiplied by time.deltaTime
            pos.y += cameraMoveSpeed * Time.deltaTime;

        }

        // clamp the x position 
        pos.x = Mathf.Clamp(pos.x, minX, maxX);

        // clamp the z position
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);

        //
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        // transform.position is = to pos
        transform.position = pos;

    }

}
