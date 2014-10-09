//wheel objects, not meshes
public var FRWheel: GameObject;
public var FLWheel: GameObject;
public var RRWheel: GameObject;
public var RLWheel: GameObject;

//wheel meshes
public var FRMesh: GameObject;
public var FLMesh: GameObject;
public var RRMesh: GameObject;
public var RLMesh: GameObject;

//Variables for adjusting vehicle handling
public var TorqueFront: boolean;		//front-wheel drive?
public var TorqueRear: boolean;			//rear-wheel drive?
public var BrakeFront: boolean;			//brakes on front wheels?
public var BrakeRear: boolean;			//brakes on rear?
public var BrakePower: float;			//Brake force
public var TorquePower: float;			//Engine power
public var MaxWheelAngle: float;		//Maximum angle wheels can turn

public var AntiRoll: float;
public var MaxSpeed: float;
public var VehicleCOM: Vector3;			//Center of mass, set origin lower for more stability

private var wheelangle: float;

function Start () {
	this.rigidbody.centerOfMass = VehicleCOM;
}

function FixedUpdate () {
	//declare colliders//
	var FR: WheelCollider = FRWheel.GetComponent(WheelCollider);
	var FL: WheelCollider = FLWheel.GetComponent(WheelCollider);
	var RR: WheelCollider = RRWheel.GetComponent(WheelCollider);
	var RL: WheelCollider = RLWheel.GetComponent(WheelCollider);
	//----//
	//get speed//
	speed = this.rigidbody.velocity.sqrMagnitude;
	//----//
	//Inputs//
	var torque: float;
	var brake: float;
	torque = -Input.GetAxis("Vertical") * TorquePower * Time.deltaTime;
	wheelangle = Input.GetAxis("Horizontal") * MaxWheelAngle;
	if (Input.GetKey("space")) {
		brake = rigidbody.mass * BrakePower;
	}
	else {
		brake = 0.0;
	}
	//----//
	//Steering//
	FR.steerAngle = wheelangle;
	FL.steerAngle = wheelangle;
	//----//
	//movement//
	if (brake > 0.0) {
		if (BrakeFront) {
			FR.brakeTorque = brake;
			FL.brakeTorque = brake;
		}
		if (BrakeRear) {
			RR.brakeTorque = brake;
			RL.brakeTorque = brake;
		}
		if (TorqueFront) {
			FR.motorTorque = 0.0;
			FL.motorTorque = 0.0;
		}
		if (TorqueRear) {
			RR.motorTorque = 0.0;
			RL.motorTorque = 0.0;
		}
	}
	else {
		FR.brakeTorque = 0.0;
		FL.brakeTorque = 0.0;
		RR.brakeTorque = 0.0;
		RL.brakeTorque = 0.0;
		if (speed < MaxSpeed && torque < 0.0) {
			if (TorqueFront) {
				FR.motorTorque = torque;
				FL.motorTorque = torque;
			}
			if (TorqueRear) {
				RR.motorTorque = torque;
				RL.motorTorque = torque;
			}
		}
		else if (speed < MaxSpeed/4 && torque > 0.0) {
			if (TorqueFront) {
				FR.motorTorque = torque;
				FL.motorTorque = torque;
			}
			if (TorqueRear) {
				RR.motorTorque = torque;
				RL.motorTorque = torque;
			}
		}
		else {
			FR.motorTorque = 0.0;
			FL.motorTorque = 0.0;
			RR.motorTorque = 0.0;
			RL.motorTorque = 0.0;			
		}
	}
	//----//
	
	//ANTI-ROLL//
	var hit: WheelHit;
	var FRDistance: float = 1.0;
	var FLDistance: float = 1.0;
	var RRDistance: float = 1.0;
	var RLDistance: float = 1.0;
	if (FR.GetGroundHit(hit)) {
		FRDistance = (-FR.transform.InverseTransformPoint(hit.point).y - FR.radius) / FR.suspensionDistance;
	}
	if (FL.GetGroundHit(hit)) {
		FLDistance = (-FL.transform.InverseTransformPoint(hit.point).y - FL.radius) / FL.suspensionDistance;
	}
	if (RR.GetGroundHit(hit)) {
		FRDistance = (-RR.transform.InverseTransformPoint(hit.point).y - RR.radius) / RR.suspensionDistance;
	}
	if (RL.GetGroundHit(hit)) {
		FLDistance = (-RL.transform.InverseTransformPoint(hit.point).y - RL.radius) / RL.suspensionDistance;
	}
	//--//
	var AntiRollF = (FLDistance - FRDistance) * AntiRoll;
	var AntiRollR = (RLDistance - RRDistance) * AntiRoll;
	//--//
	if (FR.GetGroundHit(hit)) {
		rigidbody.AddForceAtPosition(FR.transform.up * AntiRollF, FR.transform.position);
	}
	if (FL.GetGroundHit(hit)) {
		rigidbody.AddForceAtPosition(FL.transform.up * -AntiRollF, FL.transform.position);
	}
	if (RR.GetGroundHit(hit)) {
		rigidbody.AddForceAtPosition(RR.transform.up * AntiRollR, RR.transform.position);
	}
	if (RL.GetGroundHit(hit)) {
		rigidbody.AddForceAtPosition(RL.transform.up * -AntiRollR, RL.transform.position);
	}
	//----//
}

function Update() {
	//Mesh transform//
	FRMesh.transform.localRotation.eulerAngles.y = wheelangle;
	FLMesh.transform.localRotation.eulerAngles.y = wheelangle;
	//RRMesh.transform.localRotation.eulerAngles.y = -wheelangle;
	//RLMesh.transform.localRotation.eulerAngles.y = -wheelangle;
	//----//
}