using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HandleUI : MonoBehaviour
{
    public TextMeshProUGUI playerHP;
    public TextMeshProUGUI bossHP;
    public TextMeshProUGUI timer;

    public float startTime;

    [HideInInspector] public SceneBehaviour sceneBehaviour;
    PlayerController playerController;
    BossController bossController;

    private void Start()
    {
        playerHP = GameObject.Find("Player Number (TMP)").GetComponent<TextMeshProUGUI>();
        bossHP = GameObject.Find("Boss Number (TMP)").GetComponent<TextMeshProUGUI>();
        timer = GameObject.Find("Timer (TMP)_1").GetComponent<TextMeshProUGUI>();
        
        sceneBehaviour = GameObject.FindObjectOfType<SceneBehaviour>();
        playerController = GameObject.FindObjectOfType<PlayerController>();
        bossController = GameObject.FindObjectOfType<BossController>();

        playerHP.text = playerController.health.ToString();
        bossHP.text = bossController.health.ToString();

        int temp = (int)startTime;
        timer.text = temp.ToString();
    }

    private void Update()
    {
        startTime -= Time.deltaTime;
        int temp = (int)startTime;
        timer.text = temp.ToString();

        if (temp < 0)
        {
            sceneBehaviour.playerLost = true;
        }

        playerHP.text = playerController.health.ToString();
        bossHP.text = bossController.health.ToString();
    }
}
