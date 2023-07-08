using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using TMPro;

public class CheckCollisions : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI CoinText;
    public PlayerController playerController;

    Vector3 PlayerStartPos;

    public GameObject speedBoosterIcon;
    private InGameRanking ig;

    private Animator playerAnim;
    public GameObject player;


    private void Start()
    {
        playerAnim = GetComponentInChildren<Animator>();

        PlayerStartPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        speedBoosterIcon.SetActive(false);
        ig = FindObjectOfType<InGameRanking>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            AddCoin();
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("End"))
        {
            PlayerFinished();
            if (score > 10)
            {
                //Debug.Log("You Win!");
                playerAnim.SetBool("Win", true);
            }
            else
            {
                //Debug.Log("You Lose..");
                playerAnim.SetBool("Lose", true);
            }
        }
        else if (other.CompareTag("Speedboost"))
        {
            playerController.runningSpeed = playerController.runningSpeed + 3f;
            speedBoosterIcon.SetActive(true);
            StartCoroutine(SlowAfterAWhileCoroutine());
        }
    }

    void PlayerFinished() {
        playerController.runningSpeed = 0f;
        transform.Rotate(transform.rotation.x, 180, transform.rotation.z, Space.Self);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            //Debug.Log("Touched Obstacle!..");
            // Added new codes
            transform.position = PlayerStartPos;
        }
    }

    public void AddCoin()
    {
        score++;
        CoinText.text = "Score: " + score.ToString();
    }

    private IEnumerator SlowAfterAWhileCoroutine()
    {
        yield return new WaitForSeconds(2.0f);
        playerController.runningSpeed = playerController.runningSpeed - 3f;
        speedBoosterIcon.SetActive(false);
    }

}
