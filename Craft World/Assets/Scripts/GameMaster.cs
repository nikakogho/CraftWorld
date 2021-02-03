using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameMaster : MonoBehaviour {
    Item[] items;
    public int ItemsCount { get; private set; }
    public Recipe[] Recipes { get; private set; }
    public string itemsPath = "Items";
    public string recipesPath = "Recipes";
    public static GameMaster instance;

    public GameObject pauseUI, winUI;

    int count = 0;

    enum GameState { Going, Paused, Over }
    GameState state;

    void Awake()
    {
        instance = this;
        items = Resources.LoadAll(itemsPath, typeof(Item)).Cast<Item>().ToArray();
        Recipes = Resources.LoadAll(recipesPath, typeof(Recipe)).Cast<Recipe>().ToArray();
        ItemsCount = items.Length;
    }

    void Pause(bool really)
    {
        state = really ? GameState.Paused : GameState.Going;
        pauseUI.SetActive(really);
    }

    void WinGame()
    {
        state = GameState.Over;
        winUI.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            switch (state)
            {
                case GameState.Going: Pause(true); break;
                case GameState.Paused: Pause(false); break;
            }
        }
    }

    public void ItemAdded()
    {
        count++;
        if (count == ItemsCount) WinGame();
    }

    #region Button Clicks

    public void Continue()
    {
        Pause(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        Application.Quit();
    }

    #endregion
}
