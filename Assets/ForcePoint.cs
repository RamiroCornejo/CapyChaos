using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePoint : MonoBehaviour
{
    [SerializeField] Rigidbody[] rigs;
    GameObject[] aux_gos;
    Vector3[] positions;
    Transform pos_to_force;
    [SerializeField] float force = 10;
    float timer;
    [SerializeField] float time_to_disapear = 1;
    bool animate_disapear;

    private void Start()
    {
        rigs = GetComponentsInChildren<Rigidbody>();

        positions = new Vector3[rigs.Length];
        aux_gos = new GameObject[rigs.Length];
        for (int i = 0; i < rigs.Length; i++)
        {
            positions[i] = rigs[i].transform.position;
            rigs[i].isKinematic = true;
            aux_gos[i] = rigs[i].gameObject;
        }

        SetActiveGOs(false);
    }

    public void Execute()
    {
        SetActiveGOs(true);

        for (int i = 0; i < rigs.Length; i++)
        {
            rigs[i].transform.position = positions[i];
            rigs[i].isKinematic = false;

            var dir = rigs[i].transform.position - pos_to_force.position;
            dir.Normalize();

            rigs[i].AddForce(dir * force, ForceMode.Impulse);
        }
    }

    private void Reset()
    {
        for (int i = 0; i < rigs.Length; i++)
        {
            rigs[i].transform.position = positions[i];
            rigs[i].isKinematic = true;
            rigs[i].velocity = Vector3.zero;
            rigs[i].gameObject.SetActive(false);
        }

        SetActiveGOs(false);
    }

    private void Update()
    {
        if (animate_disapear)
        {
            if (timer < time_to_disapear) timer = timer + 1 * Time.deltaTime;
            else
            {
                timer = 0;
                animate_disapear = false;
                Reset();
            }
        }
    }

    void SetActiveGOs(bool value_gos)
    {
        for (int i = 0; i < aux_gos.Length; i++) aux_gos[i].SetActive(value_gos);
    }
}
