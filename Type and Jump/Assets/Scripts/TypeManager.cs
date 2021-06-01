using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using EZCameraShake;
using TMPro;

public class TypeManager : MonoBehaviour
{
    public TextMeshProUGUI yazi;
    public GameObject yardimPaneli;
    private PlayerController pc;
    public float speed = 5f;

    private void Start()
    {
        yazi.text = "";
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        /* foreach (char letter in Input.inputString)
         {
            // Debug.Log("Basilan tus: " + letter);
             yazi.text += letter.ToString();
         }*/
        if (!pc.GetIsAlive())
            return;
        foreach (char c in Input.inputString)
        {
            AudioManager.instance.Play("Typing");
            if (yardimPaneli.activeInHierarchy)
            {
                yardimPaneli.SetActive(false);
            }
            if (c == '\b') // has backspace/delete been pressed?
            {
                if (yazi.text.Length != 0)
                {
                    yazi.text = yazi.text.Substring(0, yazi.text.Length - 1);
                }
            }
            else if ((c == '\n') || (c == '\r')) // enter/return
            {
                AudioManager.instance.Play("Enter");
                string word = yazi.text;
                switch (word)
                {
                    case "JUMP":
                        pc.Jump();
                        break;
                    case "TURN":
                        pc.Turn();
                        break;
                    case "RIGHT":
                        pc.GoRight();
                        break;
                    case "LEFT":
                        pc.GoLeft();
                        break;
                    case "FIRE":
                        pc.Fire();
                        break;
                    case "HELP":
                        pc.Help();
                        break;
                    case "STOP":
                        pc.Stop();
                        break;
                    case "JUMP RIGHT":
                        pc.JumpRight();
                        break;
                    case "JUMP LEFT":
                        pc.JumpLeft();
                        break;
                    case "RESTART":
                        FindObjectOfType<GameManager>().ReloadScene();
                        break;
                    default:
                        AudioManager.instance.Play("Wrong");
                        CameraShaker.Instance.ShakeOnce(4f, 4f, 0.1f, 1f);
                        break;
                }
                FindObjectOfType<GameManager>().IncreaseCommandCounter();
                yazi.text = "";
            }
            else
            {
                yazi.text += c;
                yazi.text = yazi.text.ToUpper();
            }
        }

    }

    
}
