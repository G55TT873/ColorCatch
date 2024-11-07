using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public GameObject finishText;
    public GameObject player;
    public float timer = 60f;
    private bool isFinished = false;
    private bool isFrozen = false;
    private float frozenTime = 0f;

    private Vector3 playerInitialPosition;
    public HighScoreManager highScoreManager;

    private void Start()
    {
        finishText.gameObject.SetActive(false);
        playerInitialPosition = player.transform.position;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (!isFinished && !isFrozen)
        {
            timer -= Time.deltaTime;
            timerText.text = "Timer: " + Mathf.Ceil(timer).ToString() + "s";

            if (timer <= 0)
            {
                FinishGame();
            }
        }
        else if (isFrozen)
        {
            frozenTime -= Time.deltaTime;
            if (frozenTime <= 0f)
            {
                isFrozen = false;
            }
        }
    }

    public void FreezeTimer(float duration)
    {
        isFrozen = true;
        frozenTime = duration;
    }

    private void FinishGame()
    {
        timer = 0;
        finishText.gameObject.SetActive(true);

        isFinished = true;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<PlayerController>().enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (highScoreManager != null)
        {
            int score = player.GetComponent<PlayerController>().GetScore();
            highScoreManager.SaveNewScore(score);
        }
        else
        {
            Debug.LogWarning("HighScoreManager is not assigned!");
        }
    }
}
