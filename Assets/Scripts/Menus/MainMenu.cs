using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject levelSelector = null;
    [SerializeField] Transform levelSelectorParent = null;
    [SerializeField] Transform parentOfParents = null;
    [SerializeField] LevelUI levelPrefab = null;
    [SerializeField] LevelUI levelQuestion = null;
    [SerializeField] Transform pagePrefab = null;
    [SerializeField] Transform comingSoon = null;
    [SerializeField] GameObject nextPageButton = null;
    [SerializeField] GameObject prevPageButton = null;

    [SerializeField] int numberPerPage = 8;

    [SerializeField] GameObject mainHUD = null;
    [SerializeField] SpawnerCredits spawn = null;
    [SerializeField] AudioMixer mixer = null;
    [SerializeField] Animator birdAnim = null;
    [SerializeField] GameObject langPanel;

    List<Transform> pages = new List<Transform>();

    Transform currentParent;

    LevelsSaveData saveData;
    Settings settings;

    int currentPage;

    private void Start()
    {
        currentParent = levelSelectorParent;
        pages.Add(currentParent);

        if (BinarySerialization.IsFileExist(SaveDataUtilities.LevelSaveDataName))
        {
            saveData = BinarySerialization.Deserialize<LevelsSaveData>(SaveDataUtilities.LevelSaveDataName);
            saveData = SaveDataUtilities.RefreshSaveData(saveData);
        }
        else
        {
            saveData = SaveDataUtilities.CreateSaveData();
            BinarySerialization.Serialize(SaveDataUtilities.LevelSaveDataName, saveData);
        }

        if (BinarySerialization.IsFileExist(SaveDataUtilities.SettingsName))
        {
            settings = BinarySerialization.Deserialize<Settings>(SaveDataUtilities.SettingsName);
        }
        else
        {
            settings = new Settings();
            BinarySerialization.Serialize(SaveDataUtilities.SettingsName, settings);
        }

        isMute = settings.mute;

        if (isMute)
        {
            isMute = true;
            birdAnim.Play("MenuBirdMute");
            mixer.SetFloat("Volume", -80);
        }
    }
    bool alreadyOpen;
    public void OpenLevelSelector()
    {
        levelSelector.SetActive(true);
        SoundFX.Play_ui_Transition_03();

        if (!alreadyOpen)
        {
            int levelsCount = SceneManager.sceneCountInBuildSettings - 1;

            bool playable = true;

            for (int i = 0; i < levelsCount; i++)
            {
                if (i != 0 && i % numberPerPage == 0)
                {
                    currentParent = Instantiate(pagePrefab, parentOfParents);
                    pages.Add(currentParent);
                }
                var level = Instantiate(levelPrefab, currentParent);
                if (playable)
                {
                    level.SetUI(i + 1, saveData.levelsCompleted[i], saveData.capibaraClear[i], saveData.collectableClear[i], saveData.timeClear[i], saveData.crownObtain[i]);
                    playable = saveData.levelsCompleted[i];
                }
            }

            int diff = levelsCount % numberPerPage;
            if (diff != 0)
            {
                for (int i = 0; i < numberPerPage - diff; i++)
                {
                    Instantiate(levelQuestion, currentParent);
                }
            }

            pages.Add(Instantiate(comingSoon, parentOfParents));
            alreadyOpen = true;
        }

        pages[0].gameObject.SetActive(true);

        if (pages.Count > 1)
        { 
            nextPageButton.SetActive(true);
            for (int i = 1; i < pages.Count; i++)
            {
                pages[i].gameObject.SetActive(false);
            }
        }

        StartCoroutine(CoroutineUtility.ScaleThings(4, levelSelector.transform, Vector3.zero, Vector3.one));
    }

    public void LanguagePanel()
    {
        if (backing) return;
        SoundFX.Play_ui_Transition_02();
        langPanel.SetActive(true);
        //StartCoroutine(CreditsCoroutine(() => spawn.On(), Vector3.one, Vector3.zero));
    }
    public void BackFromLangPanel()
    {
        SoundFX.Play_ui_Transition_03();
        langPanel.SetActive(false);
    }

    public void Credits()
    {
        if (backing) return;
        SoundFX.Play_ui_Transition_02();
        backing = true;
        StartCoroutine(CreditsCoroutine(()=>spawn.On(), Vector3.one, Vector3.zero));
    }

    bool backing;
    IEnumerator CreditsCoroutine(System.Action End, Vector3 from, Vector3 to)
    {
        yield return CoroutineUtility.ScaleThings(5, mainHUD.transform, from, to);
        End();
        backing = false;
    }

    public void BackCredits()
    {
        SoundFX.Play_ui_Transition_03();
        if (backing) return;
        backing = true;
        StartCoroutine(CreditsCoroutine(() => spawn.Off(), Vector3.zero, Vector3.one));
    }

    public void Back()
    {
        if (levelSelector.activeSelf) StartCoroutine(ExitLevel());
        SoundFX.Play_ui_Transition_02();
    }

    IEnumerator ExitLevel()
    {
        yield return CoroutineUtility.ScaleThings(2, levelSelector.transform, Vector3.one, Vector3.zero);
        nextPageButton.SetActive(false);
        prevPageButton.SetActive(false);
        currentPage = 0;
        levelSelector.SetActive(false);
    }

    public void ChangePage(int dir)
    {
        pages[currentPage].gameObject.SetActive(false);
        currentPage += dir;
        pages[currentPage].gameObject.SetActive(true);

        if (currentPage == 0) prevPageButton.SetActive(false);
        else prevPageButton.SetActive(true);

        if(currentPage >= pages.Count - 1) nextPageButton.SetActive(false);
        else nextPageButton.SetActive(true);
    }

    bool isMute;

    public void MuteSwitch()
    {
        if (isMute)
        {
            mixer.SetFloat("Volume", 0);
            birdAnim.Play("MenuBird");
        }
        else
        {
            mixer.SetFloat("Volume", -80);
            birdAnim.Play("MenuBirdMute");
        }
        isMute = !isMute;
        settings.mute = isMute;
        BinarySerialization.Serialize(SaveDataUtilities.SettingsName, settings);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
