using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instanciate;
    private void Awake() => instanciate = this;
    HashSet<CharacterSpawner> spawners = new HashSet<CharacterSpawner>();
    public void SubscribeSpawner(CharacterSpawner spawner) => spawners.Add(spawner);
    public void BeginAllSpawners()
    {
        foreach (var s in spawners) s.BeginSpawn();
    }

    HashSet<Character> chars = new HashSet<Character>();
    public void SubscribeCharacter(Character character)
    {
        chars.Add(character);
    }
    public void UnSubscribeCharacter(Character character)
    {
        chars.Remove(character);
    }

    public static HashSet<Character> Characters => instanciate.chars;
}
