using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public float currentHealth = 100;
    public float maxHealth = 100;
    public static float currentStamina = 100;
    public static float maxStamina = 100;
    public static float currentMana = 100;
    public static float maxMana = 100;

    [SerializeField]
    private GameObject deathScreen;
    [SerializeField]
    private PlayerMovement player;
    private Camera fpsCamera;

    public GameObject currentCheckpoint;

    [SerializeField]
    private float respawnDelay = 0;
    private bool coroutineIsRunning = false;

    [SerializeField]
    private AudioSource deathSound;
    [SerializeField]
    private AudioSource respawnSound;

    public bool playerIsAlive = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        fpsCamera = player.GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            if (playerIsAlive)
            {
                deathSound.Play();
            }
            playerIsAlive = false;            
            deathScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            fpsCamera.gameObject.SetActive(false);
        }
    }

    public void RespawnPlayer()
    {
        if (!coroutineIsRunning)
        {
            StartCoroutine("RespawnPlayerCo");
        }
    }

    public IEnumerator RespawnPlayerCo()
    {
        coroutineIsRunning = true;

        yield return new WaitForSeconds(respawnDelay);

        currentHealth = maxHealth;
        currentMana = maxMana;
        currentStamina = maxStamina;

        deathScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;

        player.transform.position = new Vector3(currentCheckpoint.transform.position.x, 
                                                currentCheckpoint.transform.position.y + 5, 
                                                currentCheckpoint.transform.position.z);
        fpsCamera.gameObject.SetActive(true);
        coroutineIsRunning = false;
        respawnSound.Play();
        playerIsAlive = true;
    }
}
