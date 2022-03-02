using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
   [SerializeField] WheelCollider frontLeft;
   [SerializeField] WheelCollider frontRight;
   [SerializeField] WheelCollider rearLeft;
   [SerializeField] WheelCollider rearRight;
   [SerializeField] Transform frontLeftTransform;
   [SerializeField] Transform frontRightTransform;
   [SerializeField] Transform rearLeftTransform;
   [SerializeField] Transform rearRightTransform;

   private float Acceleration=2000f;
   private float Speed=100f;
   private float BreakingForce=6000f;
   private float CurrentAcceleration = 0f;
   private float CurrentBreakingForce = 0f;
   private float CurrentSteering = 0f;
   private float maxSteeringAngle = 30f;
   public static  WheelController vol;
   public  float CarSpeed ;
   Rigidbody rb;
   public void Start()
   {
      vol = this;
      rb = GetComponent<Rigidbody>();
   }

   private void FixedUpdate() {
       
       if(Input.GetAxis("Vertical") != 0){
           CurrentAcceleration = Input.GetAxis("Vertical") * Acceleration * Time.deltaTime * Speed;
           CarSpeed = (rb.velocity.magnitude*3.6f)/60;
       }
       else{
           CurrentAcceleration = 0f;
               CarSpeed-=((rb.velocity.magnitude*3.6f)/60)*Time.deltaTime;
               if(CarSpeed < 0){
                   CarSpeed = 0;
               }
               }

       if(Input.GetKey(KeyCode.Space)){
        CurrentBreakingForce = BreakingForce * Time.deltaTime * Speed;
        }

       else{ 
        CurrentBreakingForce = 0f;
        }

        frontRight.motorTorque = CurrentAcceleration;
        frontLeft.motorTorque = CurrentAcceleration;
        rearRight.motorTorque = CurrentAcceleration;
        rearLeft.motorTorque = CurrentAcceleration;
        frontRight.brakeTorque = CurrentBreakingForce;
        frontLeft.brakeTorque = CurrentBreakingForce;
        rearRight.brakeTorque = CurrentBreakingForce;
        rearLeft.brakeTorque = CurrentBreakingForce;
        CurrentSteering = maxSteeringAngle * Input.GetAxis("Horizontal");
        frontRight.steerAngle = CurrentSteering;
        frontLeft.steerAngle = CurrentSteering;

        UpdateWheel(frontLeft, frontLeftTransform);
        UpdateWheel(frontRight, frontRightTransform);
        UpdateWheel(rearLeft, rearLeftTransform);
        UpdateWheel(rearRight, rearRightTransform);
   }

   void UpdateWheel(WheelCollider col , Transform trans){

       Vector3 position;
       Quaternion rotation;
       col.GetWorldPose(out position, out rotation);


       trans.position = position;
       trans.rotation = rotation;
   }
}
