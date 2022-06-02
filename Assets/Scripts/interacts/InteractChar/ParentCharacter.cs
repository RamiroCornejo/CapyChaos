using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class ParentCharacter : TriggerReceptor
{
    public Transform parent;
    public UserInteractable rbParent;
    float timer;

    HashSet<Character> parented_chars = new HashSet<Character>();

    [SerializeField] Transform pos_a;
    [SerializeField] Transform pos_b;

    [SerializeField] Transform check_zone_a;
    [SerializeField] Transform check_zone_b;
    [SerializeField] Transform floor_line;

    public bool fix_vertical_platform;

    protected override void OnExecute(Character c)
    {
        if (!parented_chars.Contains(c))
        {
            parented_chars.Add(c);
            c.transform.SetParent(parent);
        }
    }
    protected override void OnExit(Character c)
    {
        if (parented_chars.Contains(c))
        {
            parented_chars.Remove(c);
            if (c.transform.parent == parent) c.transform.parent = null;
        }
    }

    protected override void OnStay(Character c) { }

    private void Update()
    {
        var parented = parented_chars.ToArray();

        if (parented.Length > 0)
        {
            HashSet<Character> to_remove = new HashSet<Character>();

            for (int i = 0; i < parented.Length; i++)
            {
                var p = parented[i];
                if (p == null) continue;

                if (rbParent.moving) p.GetComponent<Rigidbody>().velocity = Vector3.zero;

                float x = p.transform.position.x;
                float y = p.transform.position.y;
                float AX = pos_a.position.x;
                float AY = pos_a.position.y;
                float BX = pos_b.position.x;
                float BY = pos_b.position.y;

                if (y < BY &&
                    y > AY &&
                    x < AX && //es asi porque las coordenadas estan invertidas, error de un inicio, voltear cuando se arregle
                    x > BX) //es asi porque las coordenadas estan invertidas, error de un inicio, voltear cuando se arregle
                {
                    //bleh, lo dejo asi porque paja
                }
                else
                {
                    to_remove.Add(p);
                }
            }

            foreach (var r in to_remove)
            {
                parented_chars.Remove(r);
                r.transform.parent = null;
            }
        }

        UpdateCheckCapys();
    }

    
    public List<Character> insider = new List<Character>();

    void UpdateCheckCapys()
    {
        

        if (!fix_vertical_platform) return;

        var allchars = SpawnManager.Characters;

        foreach (var c in allchars)
        {
            if (c == null) return; 
            float x = c.transform.position.x;
            float y = c.transform.position.y;
            float AX = check_zone_a.position.x;
            float AY = check_zone_a.position.y;
            float BX = check_zone_b.position.x;
            float BY = check_zone_b.position.y;
            
            if (    /*y < BY &&
                    y > AY &&*/
                    x < AX && //es asi porque las coordenadas estan invertidas, error de un inicio, voltear cuando se arregle
                    x > BX) //es asi porque las coordenadas estan invertidas, error de un inicio, voltear cuando se arregle
            {

                Debug.Log("estoy dentro");

                if (!insider.Contains(c)) insider.Add(c);

                c.state_platform = Character.platform_state.undef;
                
                //dentro
                float floor = floor_line.position.y;

                if (y > floor)
                {
                    //sobre
                    if (c.state_platform == Character.platform_state.undef)
                    {
                        c.state_platform = Character.platform_state.up;
                    }
                    if (c.state_platform == Character.platform_state.down)
                    {
                        //yo estaba abajo y la plataforma me transpasó
                    }
                }
                else
                {
                   

                    //debajo
                    if (c.state_platform == Character.platform_state.undef)
                    {
                        c.state_platform = Character.platform_state.down;

                        if (c.transform.position.y < floor + 1)
                        {
                            c.transform.position = new Vector3(c.transform.position.x, floor, c.transform.position.z);
                            var rb = c.GetComponent<Rigidbody>();
                            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.y);
                        }
                    }
                    if (c.state_platform == Character.platform_state.up)
                    {
                        Debug.Log("DEBAJOO: lo transpasee");

                        c.transform.position = new Vector3(c.transform.position.x, floor, c.transform.position.z);
                        var rb = c.GetComponent<Rigidbody>();
                        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.y);
                    }
                }
            }
            else
            {
                if(insider.Contains(c)) insider.Remove(c);
                //fuera
            }
        }
    }
}
