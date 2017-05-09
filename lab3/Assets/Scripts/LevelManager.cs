using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    public static LevelManager Instance { get { return instance; } }

    private float startTime;
    public float silverTime;
    public float goldTime;

    public Transform respawnPoint;
    private GameObject player;

    public GameObject pauseMenu;
    private void Update()
    {
        if (player.transform.position.y < -10)
        {
            Death();
        }
    }
    public void Death()
    {
        player.transform.position = respawnPoint.position;
        Rigidbody body = player.GetComponent<Rigidbody>();
        body.velocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;
    }
    private void Start()
    {
        instance = this;
        pauseMenu.SetActive(false);
        startTime = Time.time;
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = respawnPoint.position;
    }
    public void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }
    public void ToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Victory()
    {
        float duration = Time.time - startTime;
        if (duration < goldTime)
        {
            GameManager.Instance.currency += 50;
        }
        else
        if (duration < silverTime)
        {
            GameManager.Instance.currency += 25;
        }
        else
        {
            GameManager.Instance.currency += 10;
        }
        GameManager.Instance.Save();

        LevelData level = new LevelData(SceneManager.GetActiveScene().name);
        string saveString = (level.BestTime > duration || level.BestTime == 0.0f) ? duration.ToString() : level.BestTime.ToString();
        saveString += '&';
        saveString += silverTime.ToString();
        saveString += '&';
        saveString += goldTime.ToString();
        PlayerPrefs.SetString(SceneManager.GetActiveScene().name, saveString);

        SceneManager.LoadScene("MainMenu");
    }
}
