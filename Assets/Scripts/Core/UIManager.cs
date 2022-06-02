using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

    [Header("Win and Lose UI")]
    [SerializeField] GameObject loseScreen = null;
    [SerializeField] GameObject winScreen = null;

    [SerializeField] Image[] principalStars = new Image[0];
    [SerializeField] Image capibaraStar = null;
    [SerializeField] Image collectableStar = null;
    [SerializeField] Image timeStar = null;
    [SerializeField] Sprite collectableOK = null;
    [SerializeField] Sprite collectableNull = null;
    [SerializeField] Sprite capibaraOK = null;
    [SerializeField] Sprite starOK = null;

    [SerializeField] RectTransform capibaraParent = null;
    [SerializeField] RectTransform collectableParent = null;
    [SerializeField] Image capibaraPrefab = null;
    [SerializeField] Image collectablePrefab = null;
    [SerializeField] TextMeshProUGUI timerText = null;
    [SerializeField] Transform winText = null;
    [SerializeField] Transform crown = null;

    [SerializeField] float timeToLose = 3;
    [SerializeField] float speedToScaleStamp = 4;
    [SerializeField] float timeBetweenStamps = 0.2f;
    [SerializeField] float speedToScaleStar = 3;
    [SerializeField] float timeBetweenStarAndStamp = 0.3f;
    [SerializeField] float timeBetweenCategories = 0.3f;

    [Header("Gamplay UI")]
    [SerializeField] TextMeshProUGUI counterText = null;
    [SerializeField] Animator counterAnim = null;
    [SerializeField] ParticleSystem[] confeti = new ParticleSystem[0];
    [SerializeField] ParticleSystem[] festejo_chiquito = new ParticleSystem[0];

    [Header("Sounds")]
    [SerializeField] AudioClip[] goodSound = new AudioClip[0];
    [SerializeField] AudioClip[] badSound = new AudioClip[0];

    int starAmmount = 1;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < goodSound.Length; i++) AudioManager.instance.GetSoundPool(goodSound[i].name, AudioManager.SoundDimesion.TwoD, goodSound[i]);
        for (int i = 0; i < badSound.Length; i++) AudioManager.instance.GetSoundPool(badSound[i].name, AudioManager.SoundDimesion.TwoD, badSound[i]);
    }

    public void ShowLose()
    {
        StartCoroutine(LoseCoroutine());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && win)
        {
            InstaWin();
            StopAllCoroutines();
            win = false;
        }
    }

    public void ChangeCounter(int newCount)
    {
        counterText.text = newCount.ToString();

        if (newCount == 0)
        {
            FestejoChiquito();

            SoundFX.Play_gameWin();
        }
        counterAnim.Play("Slash");
    }

    public void SetCollectables(Sprite collOK, Sprite collNull)
    {
        collectableOK = collOK;
        collectableNull = collNull;
    }

    public void ChangeCounterReference(Animator anim, TextMeshProUGUI txt)
    {
        counterText = txt;
        counterAnim = anim;

        counterText.text = Main.instance.objectiveCharacter.ToString();
    }

    bool win;
    public void ShowWin(int maxCapibara, int currentCapibara, int maxCollectable, int currentCollectable, int objectiveSeconds, int currentSeconds)
    {
        win = true;
        winScreen.SetActive(true);
        int levelIndex = SceneManager.GetActiveScene().buildIndex - 1;

        int starObtain = 0;

        if (!Main.instance.saveData.levelsCompleted[levelIndex]) starObtain += 1;
        if (!Main.instance.saveData.collectableClear[levelIndex] && currentCapibara >= maxCapibara) starObtain += 1;
        if (!Main.instance.saveData.capibaraClear[levelIndex] && currentCollectable >= maxCollectable) starObtain += 1;
        if (!Main.instance.saveData.timeClear[levelIndex] && currentSeconds <= objectiveSeconds) starObtain += 1;

        Main.instance.AddStars(starObtain);


        _maxCapibara = maxCapibara;
        _currentCapibara = currentCapibara;
        _maxCollectable = maxCollectable;
        _currentCollectable = currentCollectable;
        _objectiveSeconds = objectiveSeconds;
        _currentSeconds = currentSeconds;

        capibaras = new Image[_maxCapibara];
        for (int i = 0; i < _maxCapibara; i++)
        {
            capibaras[i] = Instantiate(capibaraPrefab, capibaraParent);
            capibaras[i].transform.localScale = Vector3.zero;
        }

        collectables = new Image[_maxCollectable];
        for (int i = 0; i < _maxCollectable; i++)
        {
            collectables[i] = Instantiate(collectablePrefab, collectableParent);
            collectables[i].sprite = collectableNull;
            collectables[i].transform.localScale = Vector3.zero;
        }
        Main.instance.FadeMusic();

        StartCoroutine(WinCoroutine());
    }



    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextLevelIndex >= SceneManager.sceneCountInBuildSettings) nextLevelIndex = 0;
        SceneManager.LoadScene(nextLevelIndex);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    int _maxCapibara;
    int _currentCapibara;
    int _maxCollectable;
    int _currentCollectable;
    int _objectiveSeconds;
    int _currentSeconds;

    Image[] capibaras = new Image[0];
    Image[] collectables = new Image[0];

    IEnumerator WinCoroutine()
    {
        winText.localScale = Vector3.zero;
        yield return CoroutineUtility.ScaleThings(1, winScreen.transform, Vector3.zero, Vector3.one);

        int levelIndex = SceneManager.GetActiveScene().buildIndex - 1;
        Main.instance.saveData.levelsCompleted[levelIndex] = true;

        #region Capibara
        for (int i = 0; i < _maxCapibara; i++)
        {
            if (i < _currentCapibara)
            {
                capibaras[i].sprite = capibaraOK;

                int random = Random.Range(0, goodSound.Length);
                AudioManager.instance.PlaySound(goodSound[random].name);

                yield return CoroutineUtility.ScaleThings(speedToScaleStamp, capibaras[i].transform, Vector3.zero, Vector3.one);

                yield return new WaitForSeconds(timeBetweenStamps);
            }
            else
            {
                int random = Random.Range(0, badSound.Length);
                AudioManager.instance.PlaySound(badSound[random].name);
                capibaras[i].transform.localScale = Vector3.one;
                yield return new WaitForSeconds(timeBetweenStamps);
            }
        }

        yield return new WaitForSeconds(timeBetweenStarAndStamp);

        capibaraStar.gameObject.SetActive(true);

        if (_currentCapibara >= _maxCapibara)
        {
            int random = Random.Range(0, goodSound.Length);
            AudioManager.instance.PlaySound(goodSound[random].name);
            starAmmount += 1;
            capibaraStar.sprite = starOK;
            if (!Main.instance.saveData.capibaraClear[levelIndex]) yield return CoroutineUtility.ScaleThings(speedToScaleStar, capibaraStar.transform, Vector3.zero, Vector3.one * 1.5f);
            Main.instance.saveData.capibaraClear[levelIndex] = true;
        }
        else
        {
            int random = Random.Range(0, badSound.Length);
            AudioManager.instance.PlaySound(badSound[random].name);
        }
        #endregion

        yield return new WaitForSeconds(timeBetweenCategories);

        #region Collectable

        for (int i = 0; i < _maxCollectable; i++)
        {
            if (i < _currentCollectable)
            {
                collectables[i].sprite = collectableOK;

                int random = Random.Range(0, goodSound.Length);
                AudioManager.instance.PlaySound(goodSound[random].name);

                yield return CoroutineUtility.ScaleThings(speedToScaleStamp, collectables[i].transform, Vector3.zero, Vector3.one);

                yield return new WaitForSeconds(timeBetweenStamps);
            }
            else
            {
                int random = Random.Range(0, badSound.Length);
                AudioManager.instance.PlaySound(badSound[random].name);
                collectables[i].transform.localScale = Vector3.one;
                yield return new WaitForSeconds(timeBetweenStamps);
            }
        }

        yield return new WaitForSeconds(timeBetweenStarAndStamp);

        collectableStar.gameObject.SetActive(true);

        if (_currentCollectable >= _maxCollectable)
        {
            int random = Random.Range(0, goodSound.Length);
            AudioManager.instance.PlaySound(goodSound[random].name);
            starAmmount += 1;
            collectableStar.sprite = starOK;
            if (!Main.instance.saveData.collectableClear[levelIndex]) yield return CoroutineUtility.ScaleThings(speedToScaleStar, collectableStar.transform, Vector3.zero, Vector3.one * 1.5f);
            Main.instance.saveData.collectableClear[levelIndex] = true;
        }
        else
        {
            int random = Random.Range(0, badSound.Length);
            AudioManager.instance.PlaySound(badSound[random].name);
        }

        #endregion

        yield return new WaitForSeconds(timeBetweenCategories);

        #region Time

        int minutes = 0;
        int seconds = 0;
        int objMinutes = _objectiveSeconds / 60;
        int objSeconds = _objectiveSeconds - objMinutes * 60;
        string secondsOnString = objSeconds >= 10 ? objSeconds.ToString() : "0" + objSeconds;

        for (int i = 0; i < _currentSeconds; i++)
        {
            seconds += 1;
            if (seconds == 60)
            {
                minutes += 1;
                seconds = 0;
            }
            string secondsOnStringTwo = seconds >= 10 ? seconds.ToString() : "0" + seconds;

            timerText.text = minutes + ":" + secondsOnStringTwo + " / " + objMinutes + ":" + secondsOnString;

            yield return new WaitForSeconds(0.05f);
        }

        timeStar.gameObject.SetActive(true);

        if (_currentSeconds <= _objectiveSeconds)
        {
            int random = Random.Range(0, goodSound.Length);
            AudioManager.instance.PlaySound(goodSound[random].name);
            timerText.color = Color.green;
            starAmmount += 1;
            timeStar.sprite = starOK;
            if (!Main.instance.saveData.timeClear[levelIndex]) yield return CoroutineUtility.ScaleThings(speedToScaleStar, timeStar.transform, Vector3.zero, Vector3.one * 1.5f);
            Main.instance.saveData.timeClear[levelIndex] = true;
        }
        else
        {
            int random = Random.Range(0, badSound.Length);
            AudioManager.instance.PlaySound(badSound[random].name);
            timerText.color = Color.red;
        }

        #endregion

        yield return new WaitForSeconds(timeBetweenCategories);

        yield return CoroutineUtility.ScaleThings(speedToScaleStamp, winText, Vector3.zero, Vector3.one);
        yield return new WaitForSeconds(timeBetweenStamps);

        for (int i = 0; i < principalStars.Length; i++)
        {
            principalStars[i].gameObject.SetActive(true);
            if (i < starAmmount)
            {
                if (i + 1 == starAmmount) SoundFX.Play_Glitter_Long(); //si es la ultima estrella reproduzco el glitter con cola larga
                else SoundFX.Play_Glitter_Normal(); 

                principalStars[i].sprite = starOK;
                yield return CoroutineUtility.ScaleThings(speedToScaleStar, principalStars[i].transform, Vector3.zero, Vector3.one * 1.5f);
            }

            yield return new WaitForSeconds(timeBetweenStamps);
        }

        if (starAmmount >= principalStars.Length)
        {
            crown.gameObject.SetActive(true);
            Main.instance.saveData.crownObtain[levelIndex] = true;

            yield return CoroutineUtility.ScaleThings(3, crown, Vector3.one * 2, Vector3.one);

            SoundFX.Play_gameWin();

            for (int i = 0; i < confeti.Length; i++)
                confeti[i].Play();
        }

        win = false;

        Main.instance.Save();
    }

    IEnumerator LoseCoroutine()
    {
        Main.instance.FadeMusic();
        yield return new WaitForSeconds(timeToLose);
        SoundFX.Play_gameLose();

        loseScreen.SetActive(true);
        yield return CoroutineUtility.ScaleThings(2, loseScreen.transform, Vector3.zero, Vector3.one);
    }

    void InstaWin()
    {
        starAmmount = 1;
        winText.localScale = Vector3.one;
        winScreen.transform.localScale = Vector3.one;
        int levelIndex = SceneManager.GetActiveScene().buildIndex - 1;
        Main.instance.saveData.levelsCompleted[levelIndex] = true;
        for (int i = 0; i < _maxCapibara; i++)
        {
            if (i < _currentCapibara)
            {
                capibaras[i].sprite = capibaraOK;
            }
            capibaras[i].transform.localScale = Vector3.one;
        }

        capibaraStar.gameObject.SetActive(true);
        capibaraStar.transform.localScale = Vector3.one * 1.5f;

        if (_currentCapibara >= _maxCapibara)
        {
            starAmmount += 1;
            capibaraStar.sprite = starOK;
            Main.instance.saveData.capibaraClear[levelIndex] = true;
        }

        for (int i = 0; i < _maxCollectable; i++)
        {
            if (i < _currentCollectable)
            {
                collectables[i].sprite = collectableOK;
            }
            collectables[i].transform.localScale = Vector3.one;
        }

        collectableStar.gameObject.SetActive(true);
        collectableStar.transform.localScale = Vector3.one * 1.5f;

        if (_currentCollectable >= _maxCollectable)
        {
            starAmmount += 1;
            collectableStar.sprite = starOK;
            Main.instance.saveData.collectableClear[levelIndex] = true;
        }

        int minutes = _currentSeconds / 60;
        int seconds = _currentSeconds - minutes * 60;
        int objMinutes = _objectiveSeconds / 60;
        int objSeconds = _objectiveSeconds - objMinutes * 60;
        string secondsOnString = objSeconds >= 10 ? objSeconds.ToString() : "0" + objSeconds;
        string secondsOnStringTwo = seconds >= 10 ? seconds.ToString() : "0" + seconds;

        timeStar.gameObject.SetActive(true);
        timeStar.transform.localScale = Vector3.one * 1.5f;
        timerText.text = minutes + ":" + secondsOnStringTwo + " / " + objMinutes + ":" + secondsOnString;

        if (_currentSeconds <= _objectiveSeconds)
        {
            timerText.color = Color.green;
            starAmmount += 1;
            timeStar.sprite = starOK;
            Main.instance.saveData.timeClear[levelIndex] = true;
        }
        else
            timerText.color = Color.red;

        for (int i = 0; i < principalStars.Length; i++)
        {
            principalStars[i].transform.localScale = Vector3.one * 1.5f;
            principalStars[i].gameObject.SetActive(true);

            if (i < starAmmount)
            {
                principalStars[i].sprite = starOK;
            }
        }

        if (starAmmount >= principalStars.Length)
        {

            crown.gameObject.SetActive(true);
            Main.instance.saveData.crownObtain[levelIndex] = true;

            crown.localScale = Vector3.one;

            for (int i = 0; i < confeti.Length; i++)
                confeti[i].Play();
            SoundFX.Play_gameWin();
        }

        Main.instance.Save();
    }

    void FestejoChiquito()
    {
        for (int i = 0; i < festejo_chiquito.Length; i++)
            festejo_chiquito[i].Play();
    }

    public void ForceSuicide()
    {
        var capybara = FindObjectsOfType<Character>();
        for (int i = 0; i < capybara.Length; i++)
        {
            capybara[i].Kill();
        }

        var spawner = FindObjectsOfType<CharacterSpawner>();

        for (int i = 0; i < spawner.Length; i++)
        {
            spawner[i].KillAll();
        }
    }
}
