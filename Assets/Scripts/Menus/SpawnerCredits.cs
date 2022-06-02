using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerCredits : MonoBehaviour
{
    [SerializeField] float delayToSpawn = 2f;
    [SerializeField] CharCredits prefab = null;
    [SerializeField] float charDir = 1;
    bool spawn;


    [SerializeField] private Animator anim;


    float timer;

    CharCredits[] myChars;
    int currentCapibara;

    void Start()
    {
        timer = delayToSpawn;

        myChars = new CharCredits[7];

        for (int i = 0; i < myChars.Length; i++)
        {
            myChars[i] = Instantiate(prefab);
            myChars[i].gameObject.SetActive(false);
        }
    }

    bool oneshot;

    public void On()
    {
        timer = delayToSpawn;
        spawn = true;
    }

    public void Off()
    {
        spawn = false;
        oneshot = false;
        timer = 0;
        for (int i = 0; i < myChars.Length; i++)
        {
            myChars[i].gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (!spawn) return;
        timer += Time.deltaTime;

        if (timer >= delayToSpawn - 1)
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
            myChars[currentCapibara].Spawn();
            currentCapibara += 1;

            if (currentCapibara >= myChars.Length) currentCapibara = 0;
        }
    }
}
