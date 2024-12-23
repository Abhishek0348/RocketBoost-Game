using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustForce = 1000f;
    [SerializeField] float rotationForce = 100f;
    [SerializeField] AudioClip MainEngineSound;
    [SerializeField] ParticleSystem MainEngineParticle;
    [SerializeField] ParticleSystem LeftEngineParticle;
    [SerializeField] ParticleSystem RightEngineParticle;
    AudioSource audioSource;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    private void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustForce * Time.fixedDeltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(MainEngineSound);
        }
        if (!MainEngineParticle.isPlaying)
        {
            MainEngineParticle.Play();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        MainEngineParticle.Stop();
    }

    private void ProcessRotation()
    {
        float RotationValue = rotation.ReadValue<float>();
        if(RotationValue < 0)
        {
            RotateRight();

        }
        else if (RotationValue > 0)
        {
            RotateLeft();
        }
        else
        {
            StopRotation();
        }
    }

    private void RotateRight()
    {
        ApplyRotation(rotationForce);
        if (!RightEngineParticle.isPlaying)
        {
            LeftEngineParticle.Stop();
            RightEngineParticle.Play();
        }
    }

    private void RotateLeft()
    {
        ApplyRotation(-rotationForce);
        if (!LeftEngineParticle.isPlaying)
        {
            RightEngineParticle.Stop();
            LeftEngineParticle.Play();
        }
    }

    private void StopRotation()
    {
        RightEngineParticle.Stop();
        LeftEngineParticle.Stop();
    }

    private void ApplyRotation(float RotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * RotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
}
