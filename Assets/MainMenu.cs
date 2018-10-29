using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public static void BringUpMenu(MainMenu mainMenu)
    {
        mainMenu.gameObject.SetActive(true);
    }

    private Button[] buttons;
    int activeButtonIndex = 0;
    int totalChildren = 0;

    [SerializeField] private int activeFontSize = 20; // size in px
    [SerializeField] private Color activeFontColor = new Color(255f, 255f, 255f);
    [SerializeField] private int defaultFontSize = 14; // size in px
    [SerializeField] private Color defaultFontColor = new Color(0f, 255f, 9f);

    private Button[] _buttons;

    void SetButtonActive(int index)
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            Button currentButton = _buttons[i];
            Text btntxt = currentButton.gameObject.GetComponent<Text>();
            btntxt.fontSize = defaultFontSize;
        }
        //GameObject buttonLocations = GameObject.Find("Canvas UI/MainMenu/items");
        Button activeButton = _buttons[activeButtonIndex];
        Text currentButtonText = activeButton.gameObject.GetComponent<Text>();
        currentButtonText.fontSize = activeFontSize;
    }


    void Awake()
    {
        GameObject buttonLocations = GameObject.Find("Canvas UI/MainMenu/items");
        _buttons = buttonLocations.GetComponentsInChildren<Button>();
        totalChildren = buttonLocations.transform.childCount;
        for(int i = 0; i < _buttons.Length; i++)
        {
            Button currentButton = _buttons[i];
            Text btntxt = currentButton.gameObject.GetComponent<Text>();
            btntxt.fontSize = defaultFontSize;
        }
        activeButtonIndex = 0;
        SetButtonActive(activeButtonIndex);
    }

    private void OnMouseOver()
    {

    }

    // Use this for initialization
    void Start () {

	}

    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    public void ResumeGame()
    {
        gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    float currentTimer = 0f;
    float timeOut = .25f; // seconds
    bool canGoDown = true;
    // Update is called once per frame
    void Update () {
        currentTimer += Time.fixedDeltaTime;
        if (currentTimer > timeOut)
        {
            currentTimer = 0f;
            canGoDown = true;
        }

        if(Input.GetButtonDown("Jump"))
        {
            Button currentButton = _buttons[activeButtonIndex];
            Debug.LogWarning("Select Button " + currentButton.name);
            currentButton.onClick.Invoke();
        }

        if (!canGoDown) return;
        if(Input.GetAxis("Vertical") < -0.2)
        {
            currentTimer = 0f;
            canGoDown = false;
            if (activeButtonIndex == 0)
            {
                activeButtonIndex = totalChildren - 1;
            }
            else
            {
                activeButtonIndex--;
            }
            SetButtonActive(activeButtonIndex);
        }
        if(Input.GetAxis("Vertical") > .2)
        {
            currentTimer = 0f;
            canGoDown = false;
            if (activeButtonIndex == totalChildren - 1)
            {
                activeButtonIndex = 0;
            }
            else
            {
                activeButtonIndex++;
            }
            SetButtonActive(activeButtonIndex);
        }
        Debug.Log("menu button index " + activeButtonIndex);
    }

    public void Respawn()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name, LoadSceneMode.Single);
    }

}
