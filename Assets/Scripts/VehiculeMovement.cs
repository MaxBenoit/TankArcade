using UnityEngine;
using System.Collections;

public class VehiculeMovement : MonoBehaviour {

    public int PlayerNumber = 1;              // Used to identify which tank belongs to which player.  This is set by this tank's manager.
    public float Speed = 12f;                 // How fast the tank moves forward and back.
    public float TurnSpeed = 180f;            // How fast the tank turns in degrees per second.
    public AudioSource MovementAudio;         // Reference to the audio source used to play engine sounds. NB: different to the shooting audio source.
    public AudioClip EngineIdling;            // Audio to play when the tank isn't moving.
    public AudioClip EngineDriving;           // Audio to play when the tank is moving.
    public float PitchRange = 0.2f;           // The amount by which the pitch of the engine noises can vary.


    string MovementAxisName;          // The name of the input axis for moving forward and back.
    string TurnAxisName;              // The name of the input axis for turning.
    new Rigidbody rigidbody;              // Reference used to move the tank.
    float MovementInputValue;         // The current value of the movement input.
    float TurnInputValue;             // The current value of the turn input.
    float OriginalPitch = 0f;         // The pitch of the audio source at the start of the scene.
    bool IsGrounded;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }


    private void OnEnable()
    {
        // When the tank is turned on, make sure it's not kinematic.
        rigidbody.isKinematic = false;

        // Also reset the input values.
        MovementInputValue = 0f;
        TurnInputValue = 0f;
    }


    private void OnDisable()
    {
        // When the tank is turned off, set it to kinematic so it stops moving.
        rigidbody.isKinematic = true;
    }


    private void Start()
    {
        // The axes names are based on player number.
        MovementAxisName = "Vertical";
        TurnAxisName = "Horizontal";

        // Store the original pitch of the audio source.
        //m_OriginalPitch = m_MovementAudio.pitch;
    }


    private void Update()
    {
        // Store the value of both input axes.
        MovementInputValue = Input.GetAxis(MovementAxisName);
        TurnInputValue = Input.GetAxis(TurnAxisName);

        //if (IsGrounded)
        //{
        //    rigidbody.rotation.x = 0;
        //    rigidbody.rotation.z = 0;
        //}
        //EngineAudio ();
    }


    private void EngineAudio()
    {
        // If there is no input (the tank is stationary)...
        if (Mathf.Abs(MovementInputValue) < 0.1f && Mathf.Abs(TurnInputValue) < 0.1f)
        {
            // ... and if the audio source is currently playing the driving clip...
            if (MovementAudio.clip == EngineDriving)
            {
                // ... change the clip to idling and play it.
                MovementAudio.clip = EngineIdling;
                MovementAudio.pitch = Random.Range(OriginalPitch - PitchRange, OriginalPitch + PitchRange);
                MovementAudio.Play();
            }
        }
        else
        {
            // Otherwise if the tank is moving and if the idling clip is currently playing...
            if (MovementAudio.clip == EngineIdling)
            {
                // ... change the clip to driving and play.
                MovementAudio.clip = EngineDriving;
                MovementAudio.pitch = Random.Range(OriginalPitch - PitchRange, OriginalPitch + PitchRange);
                MovementAudio.Play();
            }
        }
    }


    private void FixedUpdate()
    {
        // Adjust the rigidbodies position and orientation in FixedUpdate.
        Move();
        Turn();
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            Jump();
        }
    }

    void Move()
    {
        // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
        Vector3 movement = transform.forward * MovementInputValue * Speed * Time.deltaTime;

        // Apply this movement to the rigidbody's position.
        rigidbody.MovePosition(rigidbody.position + movement);
    }


    void Turn()
    {
        // Determine the number of degrees to be turned based on the input, speed and time between frames.
        float turn = TurnInputValue * TurnSpeed * Time.deltaTime;

        // Make this into a rotation in the y axis.
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        // Apply this rotation to the rigidbody's rotation.

        rigidbody.MoveRotation(rigidbody.rotation * turnRotation);
    }

    void Jump()
    {
        rigidbody.velocity = new Vector3(0, 10f, 0);
    }

    void OnCollisionEnter()
    {
        IsGrounded = true;
    }
    void OnCollisionExit()
    {
        IsGrounded = false;
    }
}
