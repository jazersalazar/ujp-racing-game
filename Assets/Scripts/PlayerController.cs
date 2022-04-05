using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour {
    [SerializeField] GameObject centerOfMass;
    [SerializeField] TextMeshProUGUI speedometerText;
    [SerializeField] TextMeshProUGUI rpmText;
    [SerializeField] List<WheelCollider> allWheels;
    [SerializeField] float _horsePower = 0.0f;
    [SerializeField] float _turnSpeed = 30f;
    [SerializeField] float speed;
    [SerializeField] float rpm;
    [SerializeField] int wheelsOnGround;

    private Rigidbody playerRb;
    private float _horizontal;
    private float _vertical;

    private void Start() {
        playerRb = GetComponent<Rigidbody>();
        playerRb.centerOfMass = centerOfMass.transform.position;
    }

    // Update is called once per frame
    private void FixedUpdate() {
        // Get the player input
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

        if (IsOnGround()) {
            // Move the vehicle forward
            playerRb.AddRelativeForce(Vector3.forward * _vertical * _horsePower);
            // Turn the vehicle
            transform.Rotate(Vector3.up, _turnSpeed * _horizontal * Time.deltaTime);

            // print speed
            speed = Mathf.Round(playerRb.velocity.magnitude * 2.237f); // 3.6 for kph
            speedometerText.SetText("Speed:" + speed + "mph");

            // print RPM
            rpm = Mathf.Round((speed % 30) * 40);
            rpmText.SetText("RPM:" + rpm);
        }
    }

    private bool IsOnGround() {
        wheelsOnGround = 0;

        foreach (WheelCollider wheel in allWheels) {
            if (wheel.isGrounded) {
                wheelsOnGround++;
            }
        }

        if (wheelsOnGround == 4) {
            return true;
        } else {
            return false;
        }
    }
}
