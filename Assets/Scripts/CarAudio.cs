using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAudio : MonoBehaviour
{
    private float minPitch = 0.1f;
    private float maxPitch = 3.5f;
    private float pitchFromCar;
    AudioSource audioSource; 

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = minPitch;
    }

    // Update is called once per frame
    void Update()
    {
      pitchFromCar = WheelController.vol.CarSpeed;
      if(pitchFromCar < minPitch){
      audioSource.pitch = minPitch;
    }
    else if(pitchFromCar > maxPitch){
      audioSource.pitch = maxPitch;
    }
    else{
      audioSource.pitch = pitchFromCar;
    }
    }
}
