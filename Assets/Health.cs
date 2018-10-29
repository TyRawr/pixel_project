using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    [SerializeField] private int m_CurrentHealth = 3; // For the player this corresponds to the number of hearts displayed
    [SerializeField] private int m_MaxHealth = 3;
    [SerializeField] private GameObject m_HeartPrefab_UI;

    // Invincibility and damage flicker
    [SerializeField] private List<SpriteRenderer> m_SpriteRenderer;
    [SerializeField] private float m_DamageFlickerTime = .3f;
    [SerializeField] private Color m_DamageTakenColor = Color.red;
    [SerializeField] private bool m_Invulnerable;
    [SerializeField] private float m_InvulnerableTime = 1f;
    [SerializeField] private MainMenu mainMenu;

    public Transform m_CurrentSpawnPoint;

    private Transform m_HeartsContainer_UI;
    private PlayerMovement m_PlayerMovement;

    void Awake()
    {
        m_HeartsContainer_UI = GameObject.Find("Canvas UI/Health Container/Hearts").transform;
        m_PlayerMovement = GetComponent<PlayerMovement>();
    }

    // Use this for initialization
    void Start () {
        this.UpdateHealthUI();
	}

    void ResetHealth()
    {
        this.m_CurrentHealth = this.m_MaxHealth;
        this.UpdateHealthUI();
    }
	
	// Update is called once per frame
	void Update () {

	}

    IEnumerator Invulnerable()
    {
        this.m_Invulnerable = true;
        Dictionary<SpriteRenderer, Color> spriteColorMap = new Dictionary<SpriteRenderer, Color>();
        foreach(SpriteRenderer sr in m_SpriteRenderer)
        {
            spriteColorMap.Add(sr, sr.color);
        }
        
        float time = 0f;
        bool flickerOn = true;
        while(time < m_InvulnerableTime)
        {
            if (flickerOn)
            {
                foreach(SpriteRenderer sr in m_SpriteRenderer)
                {
                    sr.color = m_DamageTakenColor;
                }
            } else
            {
                foreach (SpriteRenderer sr in m_SpriteRenderer)
                {
                    sr.color = spriteColorMap[sr];
                }
            }
            yield return new WaitForSeconds(m_DamageFlickerTime);
            flickerOn = !flickerOn;
            time += m_DamageFlickerTime;
        }
        foreach (SpriteRenderer sr in m_SpriteRenderer)
        {
            sr.color = spriteColorMap[sr];
        }
        this.m_Invulnerable = false;
    }

    public void TakeDamage(int damageTaken, GameObject objectThatDeltDamage)
    {
        if (this.m_Invulnerable) return;
       
        this.m_CurrentHealth -= damageTaken;
        if(this.m_CurrentHealth <= 0)
        {
            this.Die();
            this.StopCoroutine(Invulnerable());
            return;
        }

        //TODO make impact dynamic but also clamped
        if (objectThatDeltDamage.transform.position.x > transform.position.x)
        {
            m_PlayerMovement.AddToHorizontalMovement(-20f);
        } else
        {
            m_PlayerMovement.AddToHorizontalMovement(20f);
        }

        StartCoroutine(Invulnerable());
        this.UpdateHealthUI();
    }

    public void Die()
    {
        MainMenu.BringUpMenu(mainMenu);
        //this.Respawn();
    }

    public void Respawn()
    {
        if(this.m_CurrentSpawnPoint)
        {
            this.ResetHealth();
            this.transform.position = m_CurrentSpawnPoint.position;
            StartCoroutine(Invulnerable());
            UpdateHealthUI();
            //TODO spawn animation trigger
        }
    }

    private void UpdateHealthUI()
    {
        // for now just destroy all the hearts and remake them. meh
        foreach(Transform child in m_HeartsContainer_UI)
        {
            GameObject.Destroy(child.gameObject);
        }
        for(int i = 0; i < m_CurrentHealth; i ++)
        {
            GameObject heartUI = GameObject.Instantiate(m_HeartPrefab_UI);
            heartUI.transform.SetParent(m_HeartsContainer_UI);
            heartUI.transform.localScale = Vector3.one;
        }
    }
}
