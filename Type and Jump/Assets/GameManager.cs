using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class GameManager : MonoBehaviour
{

    public GameObject helpPanel;
    public GameObject winPanel;
    public TextMeshProUGUI commandCounterText;
    public Transform spawnPoint;
    public GameObject playerPrefab;
    private PlayerController pc;

    public static int commandCounter;

    // Start is called before the first frame update
    void Start()
    {
        helpPanel.SetActive(true);
        winPanel.SetActive(false);
        pc = playerPrefab.GetComponent<PlayerController>();
        AudioManager.instance.Play("Theme");
        commandCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && helpPanel.activeInHierarchy)
        {
            YardimPaneliKapat();
        }
    }

    public void IncreaseCommandCounter()
    {
        commandCounter++;
        commandCounterText.text = "Given commands: " + commandCounter;
    }

    public int GetCommandCounter()
    {
        return commandCounter;
    }

    public void YardimPaneliAc()
    {
        helpPanel.SetActive(true);
    }

    public void YardimPaneliKapat()
    {
        helpPanel.SetActive(false);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void RespawnPlayer()
    {
        GameObject newPlayer = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
        FindObjectOfType<CinemachineVirtualCamera>().Follow = newPlayer.transform;
        pc.isAlive = true;
    }

    public void Die()
    {
        //reload scene
        Invoke("ReloadScene", 0.5f);
        AudioManager.instance.Play("Death");

    }

    public void FinishGame()
    {
        //finish screen
        winPanel.SetActive(true);

        //win sound
        AudioManager.instance.Stop("Theme");
        AudioManager.instance.Play("Win");
        Invoke("ReloadScene", 3f);
    }
}
