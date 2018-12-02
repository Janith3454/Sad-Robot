using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 6f;
    public float turningSpeed = 60f;
    public float turningSmoothing= 15f;

    private Vector3 movement;
    private Vector3 turning;
    private Animator anim;
    private Rigidbody playerRigidbody;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float lh = Input.GetAxisRaw("Horizontal");
        float lv = Input.GetAxisRaw("Vertical");


        Move ( lh, lv);

        Animating(lh, lv);

    }
    void Move(float lh, float lv)
    {
        movement.Set(lh, 0f, lv);

        movement = movement.normalized * speed * Time.deltaTime;

        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Rotating(float lh,float lv)
    {
        Vector3 targetDirection = new Vector3(lh, 0f, lv);

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

        Quaternion newRotation = Quaternion.Lerp(GetComponent<Rigidbody>().rotation, targetRotation, turningSmoothing * Time.deltaTime);

        GetComponent<Rigidbody>().MoveRotation(newRotation);

    }
    void Animating(float lh, float lv)
    {
        bool running = lh != 0f || lv != 0f;
        anim.SetBool("IsRunning", running); 
    }


}

