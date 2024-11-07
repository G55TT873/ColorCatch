using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float PlayerSpeed = 5;
    public float jumpForce = 5;
    private float movementX;
    private float NewSpeed;
    private int count;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI Windone;

    public AudioSource Pickup;
    public AudioSource BPickup;
    public AudioSource TPickup;
    public AudioSource SPickup;
    public AudioSource DPickup;
    private Rigidbody rb;
    private bool isGrounded = true;
    public Transform cameraTransform;
    public Timer gameTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        NewSpeed = PlayerSpeed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();

            Pickup.Play();
            FindObjectOfType<Spawner>().SpawnItem();
        }
        if (other.gameObject.CompareTag("-PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count - 1;
            SetCountText();
            BPickup.Play();
        }
        if (other.gameObject.CompareTag("+PickUp"))
        {
            other.gameObject.SetActive(false);
            IncreaseSpeed(5f);
            SPickup.Play();
        }
        if (other.gameObject.CompareTag("PickUp++"))
        {
            other.gameObject.SetActive(false);
            gameTimer.FreezeTimer(10f);
            TPickup.Play();
        }
        if (other.gameObject.CompareTag("PickUp+++"))
        {
            other.gameObject.SetActive(false);
            DoublePoints();
            DPickup.Play();
        }
    }

    public void DoublePoints()
    {
        count *= 2;
        SetCountText();
    }

    public void IncreaseSpeed(float duration)
    {
        StartCoroutine(SpeedBoostCoroutine(duration));
    }

    private IEnumerator SpeedBoostCoroutine(float duration)
    {
        NewSpeed = PlayerSpeed * 2;

        yield return new WaitForSeconds(duration);

        NewSpeed = PlayerSpeed;
    }

    void Update()
    {
        movementX = Input.GetAxis("Horizontal");
        float movementY = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 movement = camForward * Input.GetAxis("Vertical") + camRight * movementX;

        rb.AddForce(movement * NewSpeed);
    }

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        Windone.text = "Your Score: " + count.ToString();
    }

    public int GetScore()
    {
        return count;
    }
}
