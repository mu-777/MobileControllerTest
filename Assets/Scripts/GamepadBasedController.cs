using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamepadBasedController : MonoBehaviour {

    public float VelScale = 1f;
    public float AngVelScale = 1f;

    public float WheelRadius = 0.5f;
    public float BaselineLength = 0.8f;

    public GameObject TargetRobot;
    public Rigidbody WheelLeftRigidbody;
    public Rigidbody WheelRightRigidbody;

    void Start() {

    }

    void Update() {
        var VelInput = Input.GetAxis("Vertical") * VelScale;
        var AngVelInput = Input.GetAxis("Horizontal") * AngVelScale;

        if (Mathf.Abs(VelInput) < 0.01 * VelScale && Mathf.Abs(AngVelInput) < 0.01 * AngVelScale) {
            VelInput = AngVelInput = 0.0f;
        } else {
            print(string.Format($"v: {VelInput}, w: {AngVelInput}"));
        }

        var wheelAngVelL = -(BaselineLength / (4f * WheelRadius)) * (-VelInput * 2f / BaselineLength + AngVelInput);
        var wheelAngVelR = -(BaselineLength / (4f * WheelRadius)) * (-VelInput * 2f / BaselineLength - AngVelInput);

        var forwardVector = Vector3.Cross(TargetRobot.transform.right, Vector3.up);

        WheelLeftRigidbody.velocity = wheelAngVelL * WheelRadius * forwardVector;
        WheelRightRigidbody.velocity = wheelAngVelR * WheelRadius * forwardVector;

        print(string.Format($"l: {wheelAngVelL}, r: {wheelAngVelR}"));
    }
}
