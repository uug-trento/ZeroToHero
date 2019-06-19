using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public string passw;
    public TextMeshProUGUI passwordVisualizer;
    public Animator[] laserAnimators;

    float time = 0;
    public TextMeshProUGUI timeText;
    bool isPwdCorrect;
    string insertedPassw;

    // Start is called before the first frame update
    void Start()
    {
        timeText.text = "Time: 0";
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeText.text = "Time: " + time.ToString("F2");
    }

    public void InsertPwd(string number)
    {
        insertedPassw += number;
        passwordVisualizer.text = insertedPassw;
    }
    public void CheckPwd()
    {
        if (passw == insertedPassw)
        {
            passwordVisualizer.text = "Correct!";
            DeactivateLaser();
        }
        else
        {
            passwordVisualizer.text = "Wrong!";
            insertedPassw = "";
        }
    }

    void DeactivateLaser()
    {
        foreach (Animator laserAnimator in laserAnimators)
        {
            laserAnimator.SetBool("isActive", false);
        }
    }
}
