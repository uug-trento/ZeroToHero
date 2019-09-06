using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator anim;

    public void LoadLevel(int buildIndex)
    {
        anim.SetTrigger("ChangeScene");
        StartCoroutine(Wait(1.5f, buildIndex));
    }

    IEnumerator Wait(float time, int index)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(index);
    }
}
