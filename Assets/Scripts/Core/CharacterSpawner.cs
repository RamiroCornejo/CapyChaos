using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] float delayToSpawn = 2f;
    [SerializeField] float delayToFirstSpawn = 1f;
    public Character prefab = null;
    [SerializeField, Range(1, 20)] int ammountToSpawn = 3;
    [SerializeField] float charDir = 1;

    [SerializeField] private Animator anim;
    [SerializeField] bool changeDir = false;
    public Collider myCol;

    public bool automatic = true;

    bool begin;

    float timer;

    Character[] myChars;
    int currentCapibara;

    bool anim_flicker = true;
    float timer_flicker;
    bool flip_flop = false;
    [SerializeField] SimpleShaderHightLights feedback_higtlights;

    void Start()
    {

        if (automatic)
        {
            begin = true;
            Main.instance.BeginGame();
        }
        else
        {
            begin = false;
            SpawnManager.instanciate.SubscribeSpawner(this);
        }

        timer = delayToSpawn - delayToFirstSpawn;

        myChars = new Character[ammountToSpawn];

        for (int i = 0; i < myChars.Length; i++)
        {
            myChars[i] = Instantiate(prefab);
            myChars[i].gameObject.SetActive(true);
            
            if (!myChars[i].alreadyAgregate) { Main.instance.AddCharacter(myChars[i]); myChars[i].alreadyAgregate = true; }
            myChars[i].gameObject.SetActive(false);
        }
    }

    bool oneshot = true;
    public void OnManualTouch()
    {
        SpawnManager.instanciate.BeginAllSpawners();
        anim.Play("Click");
        SoundFX.Play_LeavesTouch();
        Main.instance.BeginGame();
        myCol.enabled = false;
    }
    public void BeginSpawn() { begin = true; anim_flicker = false; feedback_higtlights.UE_Exit(); }

    void Update()
    {
        if (anim_flicker)
        {
            if (timer_flicker < 0.5f)
            {
                timer_flicker = timer_flicker + 1 * Time.deltaTime;
            }
            else
            {
                flip_flop = !flip_flop;
                timer_flicker = 0;
                if (flip_flop) feedback_higtlights.UE_Enter(false);
                else feedback_higtlights.UE_Exit();
            }
        }

        if (!begin) return;

        if (currentCapibara >= myChars.Length) return;
        timer += Time.deltaTime;
        
        if(timer >= delayToSpawn - 1)
        {
            anim.Play("Spawn");
            if (!oneshot)
            {
                oneshot = true;
                SoundFX.Play_capi_Leaves_Move();
            }
        }   
        
        if (timer >= delayToSpawn)
        {
            oneshot = false;
            timer = 0;
            myChars[currentCapibara].transform.position = transform.position;
            myChars[currentCapibara].transform.forward = new Vector3(myChars[currentCapibara].transform.forward.x * charDir, myChars[currentCapibara].transform.forward.y, myChars[currentCapibara].transform.forward.z);
            myChars[currentCapibara].gameObject.SetActive(true);
            SpawnManager.instanciate.SubscribeCharacter(myChars[currentCapibara]);
            currentCapibara += 1;

            if (changeDir) charDir = -charDir;
        }
    }

    public void KillAll()
    {
        for (int i = currentCapibara; i < myChars.Length; i++)
        {
            myChars[i].gameObject.SetActive(true);
            myChars[i].Kill();
        }

        currentCapibara = myChars.Length;

        timer = 0;
    }
}