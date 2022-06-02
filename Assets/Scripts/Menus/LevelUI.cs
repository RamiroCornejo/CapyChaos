using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelUI : MonoBehaviour
{
    [SerializeField] GameObject blockedImg = null;
    [SerializeField] Image levelCompleteStar = null;
    [SerializeField] Image capibaraCompleteStar = null;
    [SerializeField] Image capibaraIcon = null;
    [SerializeField] Image collectCompleteStar = null;
    [SerializeField] Image collectIcon = null;
    [SerializeField] Image timeCompleteStar = null;
    [SerializeField] Image timeIcon = null;

    [SerializeField] Sprite starOK = null;
    [SerializeField] GameObject crown = null;

    [SerializeField] TextMeshProUGUI levelTxt = null;

    int levelToRepresent;

    public void SetUI(int level, bool levelComplete, bool capibaraComplete, bool collectableComplete, bool timeComplete, bool allComplete)
    {
        blockedImg.SetActive(false);
        levelToRepresent = level;
        levelTxt.text = levelToRepresent.ToString();
        GetComponent<Button>().interactable = true;


        if (levelComplete) levelCompleteStar.sprite = starOK;

        if (capibaraComplete)
        {
            capibaraCompleteStar.sprite = starOK;
            capibaraIcon.color = Color.white;
        }

        if (collectableComplete)
        {
            collectCompleteStar.sprite = starOK;
            collectIcon.color = Color.white;
        }

        if (timeComplete)
        {
            timeCompleteStar.sprite = starOK;
            timeIcon.color = Color.white;
        }

        if (allComplete)
        {
            crown.SetActive(true);
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(levelToRepresent);
    }
}
