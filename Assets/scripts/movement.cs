using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    
    [SerializeField] float mainThrust =100f;
    [SerializeField] float rotationThrust =1f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainengineparticles;
    [SerializeField] ParticleSystem leftthrustparticles;
    [SerializeField] ParticleSystem rightthrustparticles;
    
    Rigidbody rb;
    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>();
       audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if(!audioSource.isPlaying)
            {
              audioSource.PlayOneShot(mainEngine);
            }
            if (!mainengineparticles.isPlaying)     
            {     
              mainengineparticles.Play();
            }
        }
        else
        {
            audioSource.Stop();
            mainengineparticles.Stop();
        }
       
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            applyrotation(rotationThrust);
            if (!rightthrustparticles.isPlaying)     
            {     
              rightthrustparticles.Play();
            }
        }
        else if(Input.GetKey(KeyCode.D))
        {
            applyrotation(-rotationThrust);
            if (!leftthrustparticles.isPlaying)     
            {     
              leftthrustparticles.Play();
            }

        }
        else
        {
            rightthrustparticles.Stop();
            leftthrustparticles.Stop();
        }
    

    
    }

    void applyrotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }

}
