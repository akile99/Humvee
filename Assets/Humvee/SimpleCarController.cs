using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCarController : MonoBehaviour {



	public void GetInput()
	{
		// m_horizontalInput = Input.GetAxis("Horizontal");
		// m_verticalInput = Input.GetAxis("Vertical");
	}

	private void Steer()
	{
		m_steeringAngle = maxSteerAngle * m_horizontalInput;
		frontDriverW.steerAngle = m_steeringAngle;
		frontPassengerW.steerAngle = m_steeringAngle;
	}

	private void Accelerate()
	{
		frontDriverW.motorTorque = m_verticalInput * motorForce;
		frontPassengerW.motorTorque = m_verticalInput * motorForce;
		rearDriverW.motorTorque = m_verticalInput * motorForce;
		rearPassengerW.motorTorque = m_verticalInput * motorForce;
	}

	private void Brake(float brake)
	{
		frontDriverW.brakeTorque = brake;
		frontPassengerW.brakeTorque = brake;
		rearDriverW.brakeTorque = brake;
		rearPassengerW.brakeTorque = brake;
	}

	private void UpdateWheelPoses()
	{
		UpdateWheelPose(frontDriverW, frontDriverT);
		UpdateWheelPose(frontPassengerW, frontPassengerT);
		UpdateWheelPose(rearDriverW, rearDriverT);
		UpdateWheelPose(rearPassengerW, rearPassengerT);
	}

	private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
	{
		Vector3 _pos = _transform.position;
		Quaternion _quat = _transform.rotation;

		_collider.GetWorldPose(out _pos, out _quat);

		_transform.position = _pos;
		_transform.rotation = _quat;
	}

	private void FixedUpdate()
	{
		// GetInput();
		
		
		// Brake();
		UpdateWheelPoses();
		if (Input.GetKeyDown("space"))
		{
			Brake(brakeForce);
		}
		else if (Input.GetAxis("Horizontal") != 0)
		{
			m_horizontalInput = Input.GetAxis("Horizontal");
			Steer();
			Debug.Log("horizonal");
		}
		else if (Input.GetAxis("Vertical") != 0)
		{
			Brake(0);
			m_verticalInput = Input.GetAxis("Vertical");
			Accelerate();
		}
	}

	private float m_horizontalInput;
	private float m_verticalInput;
	private float m_steeringAngle;
	// private float verticalInput = Input.GetAxis("Vertical");
	// private float m_brakeInput;

	public WheelCollider frontDriverW, frontPassengerW;
	public WheelCollider rearDriverW, rearPassengerW;
	public Transform frontDriverT, frontPassengerT;
	public Transform rearDriverT, rearPassengerT;
	public float maxSteerAngle = 30;
	public float motorForce = 50;
	public float brakeForce = 5000;
}