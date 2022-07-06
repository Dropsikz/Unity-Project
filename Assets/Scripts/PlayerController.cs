using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public bool isGameOver = false;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpSpeed;
    [SerializeField] float timeToStart = 5f;
    [SerializeField] string startText = "Tap To Start";

    private AudioManager audioManager;
   

    private bool isOnGround = false;
    private Rigidbody rb;
    private bool isStart = false;
    private AudioSource audioSource;
    private Animator animator;
    private Text infoText;
    private int pointsInt;
    private IEnumerator startGame;
    private Text pointText;
    private int highPoints;
    private HighScore highScore;
    void Start()
    {
        InitializeUIText();
        InitializeAudio();
        InitializePhysics();
        InitializeAnimation();
        InitializeHighScore();
    }

    

    void Update()
    {
        PlayerMovement();
        GameLaunchGuard();
    }

    private void PlayerMovement()
    {
        if (isStart)
        {
            MovePlayerRight();
            if (Input.GetKeyDown(KeyCode.Space) && isOnGround || Input.touchCount > 0 && isOnGround)
            {
                Jump();
            }
        }
    }

    private void GameLaunchGuard()
    {
        if (Input.touchCount > 0 && startGame == null || Input.GetKeyDown(KeyCode.Space) && startGame == null)
        {
            startGame = StartGame(timeToStart);
            StartCoroutine(startGame);
        }
    }

    private void Jump()
    {
        audioSource.PlayOneShot(audioManager.jumpSound);
        rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
        isOnGround = false;
    }
   
    private void MovePlayerRight()
    {
       
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.World);
       
    }

    IEnumerator StartGame(float timeToStart)
    {
        while (timeToStart >= 0){
            infoText.text = timeToStart.ToString();
            timeToStart--;
            audioSource.PlayOneShot(audioManager.startGameSound);
            yield return new WaitForSeconds(1f);
        }
        pointText.gameObject.SetActive(true);
        infoText.gameObject.SetActive(false);
            
            isStart = true;
    }
    IEnumerator GameOver()
    {
        highScore.NewHighRecord(pointText,infoText);
        isGameOver = true;
        audioSource.PlayOneShot(audioManager.gameoverSound);
        moveSpeed = 0f;
        rb.isKinematic = true;
        animator.SetTrigger("isDead");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(0);
    }
    IEnumerator DestroyPlatform(GameObject platform)
    {
        float height = 0f;
        while (height < 1f)
        {
            platform.transform.Translate(Vector3.down * height);
            height += 0.0005f;
            yield return new WaitForSeconds(0.1f);
        }

        Destroy(platform);
    }
    private void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Lava")
        {
            StartCoroutine(GameOver());
        }
        if (other.tag == "Platform")
        {
            StartCoroutine(DestroyPlatform(other.gameObject));
            other.isTrigger = false;
            pointsInt++;
            pointText.text = pointsInt.ToString();
            audioSource.PlayOneShot(audioManager.pointCollectSound);
        }
    }
    private void InitializeHighScore()
    {
        highScore = GetComponent<HighScore>();
        highPoints = highScore.GetHighScore();
        infoText.text = startText + " Your Record: " + highPoints.ToString();
    }

    private void InitializeAnimation()
    {
        animator = GetComponent<Animator>();
    }

    private void InitializePhysics()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void InitializeAudio()
    {
        audioSource = GetComponent<AudioSource>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void InitializeUIText()
    {
        infoText = GameObject.FindGameObjectWithTag("Info").GetComponent<Text>();
        pointText = GameObject.FindGameObjectWithTag("Points").GetComponent<Text>();
        pointText.gameObject.SetActive(false);
        infoText.gameObject.SetActive(true);
    }

}
