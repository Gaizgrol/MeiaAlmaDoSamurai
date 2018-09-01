using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GeneralGameController : MonoBehaviour {
    public float initialEnemyHealth;
    public float initialEnemyDamage;
    public float initialPlayerHealth;
    public float initialPlayerDamage;
    public float initialPlayerMoveSpeed;
    public float currentPlayerHealth;
    public float currentPlayerDamage;
    public float currentPlayerMoveSpeed;
    public int counter;

    public bool isFirstScene;

    public GameObject player;
    public GameObject boss;

    public GameObject start;

    public GameObject healthBar;

    private Button b;

    private void Awake()
    {

        isFirstScene = true;

        DontDestroyOnLoad(gameObject);
        currentPlayerDamage = initialPlayerDamage;
        currentPlayerHealth = initialPlayerHealth;
        currentPlayerMoveSpeed = initialPlayerMoveSpeed;
        counter = -1;

        b = start.GetComponent<Button>();

    }

    // Use this for initialization
    void Start () {

        b.onClick.AddListener(GoToGame);

    }

    void GoToGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        player = GameObject.Find("Player");

        if ( SceneManager.GetActiveScene().name != "continue" )
        {

            isFirstScene = false;

            print("entrou");
            player = GameObject.Find("Player");
            boss = GameObject.Find("Enemy");

            if (player != null && boss != null)
            {
                print("found player");
                healthBar = GameObject.Find("healthbar");
                print(currentPlayerHealth);
                player.GetComponent<PlayerMove>().starting_health = initialPlayerHealth;

                player.GetComponent<PlayerMove>().max_health = currentPlayerHealth;
                player.GetComponent<PlayerMove>().health = currentPlayerHealth;

                player.GetComponent<PlayerMove>().damage = currentPlayerDamage;
            }

        }
        if ((SceneManager.GetActiveScene().name == "SampleScene") || (SceneManager.GetActiveScene().name == "IceScene"))
        {
            counter++;
        }

    }

    // Update is called once per frame
    void Update () {
        if ((SceneManager.GetActiveScene().name == "SampleScene") || (SceneManager.GetActiveScene().name == "IceScene"))
        {
            if ((player.GetComponent<PlayerMove>().health<=0))
            {
                if (Input.GetKeyUp(KeyCode.KeypadEnter)) 
                {
                    SceneManager.LoadSceneAsync("menu");
                    Destroy(gameObject);
                }
            }
        }
	}
}
