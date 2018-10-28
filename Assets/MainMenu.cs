using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
    void SetButtonActive(int index)
    {
        GameObject buttonLocations = GameObject.Find("Canvas UI/MainMenu/items");
        for (int i = 0; i < buttonLocations.transform.childCount; i++)
        {
            //BaseEventData bed = new BaseEventData();

            Button btn = buttonLocations.transform.GetChild(i).GetComponent<Button>();
            Text btntxt = btn.gameObject.GetComponent<Text>();
            btntxt.fontSize = defaultFontSize;
            SetButtonNotActive(btn);
        }
        //GameObject buttonLocations = GameObject.Find("Canvas UI/MainMenu/items");
        Button currentButton = buttonLocations.transform.GetChild(activeButtonIndex).GetComponent<Button>();
        Text currentButtonText = currentButton.gameObject.GetComponent<Text>();
        currentButtonText.fontSize = activeFontSize;
        currentButton.OnSelect(null);


        /*
        ColorBlock cb = button.colors;
       
        currentButtonText.fontSize = activeFontSize;
        currentButtonText.color = activeFontColor;
        */
    }

    [SerializeField] private int defaultFontSize = 14; // size in px
    [SerializeField] private Color defaultFontColor = new Color(0f, 255f, 9f);
    void SetButtonNotActive(Button currentButton)
    {
        currentButton.OnDeselect(null);
        //currentButton.OnDeselect();
        /*
        ColorBlock cb = currentButton.colors;
        Text currentButtonText = currentButton.gameObject.GetComponent<Text>();
        currentButtonText.fontSize = defaultFontSize;
        cb.normalColor = defaultFontColor;
        */
    }

    void Awake()
    {
        GameObject buttonLocations = GameObject.Find("Canvas UI/MainMenu/items");
        totalChildren = buttonLocations.transform.childCount;
        for(int i = 0; i < buttonLocations.transform.childCount; i++)
        {
            Button currentButton = buttonLocations.transform.GetChild(i).GetComponent<Button>();
            SetButtonNotActive(currentButton);
        }
        activeButtonIndex = 0;
        SetButtonActive(activeButtonIndex);
    }

    private void OnMouseOver()
    {

    }

    // Use this for initialization
    void Start () {
       
        
        /*
        Debug.Log("Hello Ciera");
        Text text = GameObject.Find("resumebutton").GetComponent<Text>();
        text.text = "Yo T";
        */
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
            GameObject buttonLocations = GameObject.Find("Canvas UI/MainMenu/items");
            Button currentButton = buttonLocations.transform.GetChild(activeButtonIndex).GetComponent<Button>();
            currentButton.Select();
        }

        if (!canGoDown) return;
        Debug.Log("accepting events");
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
	}

    public void Respawn()
    {
        Transform ty =Util.FindPlayer();
        Health health = ty.GetComponent<Health>();
        health.Respawn();
        ResumeGame();
    }


}
