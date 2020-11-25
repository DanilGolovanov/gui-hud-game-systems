using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customisation : MonoBehaviour
{
    [SerializeField]
    private Player player;

    [SerializeField]
    private PlayerProfession[] playerProfessions;

    [SerializeField]
    private string textureLocation = "Character/";

    static string[] names = { "Skin", "Hair", "Mouth", "Eyes", "Clothes", "Armour" };

    public List<Texture2D>[] partsTexture = new List<Texture2D>[names.Length];
    private int[] currentPartsTextureIndex = new int[names.Length];

    public Renderer characterRenderer;

    public Vector2 scrollPosition = Vector2.zero;

    private void Start()
    {
        for (int i = 0; i < names.Length; i++)
        {
            int count = 0;

            Texture2D tempTexture;
            partsTexture[i] = new List<Texture2D>();
            do
            {
                tempTexture = (Texture2D)Resources.Load(textureLocation + names[i] + "_" + count);
                if (tempTexture != null)
                {
                    partsTexture[i].Add(tempTexture);
                }
                count++;
            } while (tempTexture != null);
        }

        if (player == null)
        {
            Debug.LogError("player in Customisation is null");
        }

        if (playerProfessions != null && playerProfessions.Length > 0)
        {
            player.Profession = playerProfessions[0];
        }
    }

    void SetTexture(string type, int direction)
    {
        int partIndex = 0;

        switch (type)
        {
            case "Skin":
                partIndex = 0;
                break;
            case "Hair":
                partIndex = 1;
                break;
            case "Mouth":
                partIndex = 2;
                break;
            case "Eyes":
                partIndex = 3;
                break;
            case "Clothes":
                partIndex = 4;
                break;
            case "Armour":
                partIndex = 5;
                break;
        }

        int max = partsTexture[partIndex].Count;

        int currentTexture = currentPartsTextureIndex[partIndex];
        currentTexture += direction;
        if (currentTexture < 0)
        {
            currentTexture = max - 1;
        }
        else if (currentTexture > max - 1)
        {
            currentTexture = 0;
        }
        currentPartsTextureIndex[partIndex] = currentTexture;

        Material[] materials = characterRenderer.materials;
        materials[partIndex].mainTexture = partsTexture[partIndex][currentTexture];
        characterRenderer.materials = materials;
    }

    private void OnGUI()
    {
        CustomiseOnGUI();
        StatsOnGUI();
        ProfessionsOnGUI();
    }

    private void ProfessionsOnGUI()
    {
        int currentHeight = 0;

        GUI.Box(new Rect(Screen.width - 170, 230, 155, 80), "Professions");

        scrollPosition = GUI.BeginScrollView(new Rect(Screen.width - 170, 250, 155, 50), scrollPosition, new Rect(0, 0, 100, 30 * playerProfessions.Length));

        for (int i = 0; i < playerProfessions.Length; i++)
        {
            if (GUI.Button(new Rect(20, currentHeight + i * 30, 100, 20), playerProfessions[i].professionName))
            {
                player.Profession = playerProfessions[i];
            }
        }
        
        GUI.EndScrollView();

        GUI.Box(new Rect(Screen.width - 170, Screen.height - 90, 155, 80), "Display");
        GUI.Label(new Rect(Screen.width - 140, Screen.height - 90 + 30, 100, 20), player.Profession.professionName);
        GUI.Label(new Rect(Screen.width - 140, Screen.height - 90 + 45, 100, 20), player.Profession.abilityName);
        GUI.Label(new Rect(Screen.width - 140, Screen.height - 90 + 60, 100, 20), player.Profession.abilityDescription);
    }

    private void StatsOnGUI()
    {
        float currentHeight = 40;
        GUI.Box(new Rect(Screen.width - 170, 10, 155, 210), "Stats: " + player.playerStats.baseStatPoints);
        for (int i = 0; i < player.playerStats.baseStats.Length; i++)
        {
            BaseStats stat = player.playerStats.baseStats[i];

            if (GUI.Button(new Rect(Screen.width - 165, currentHeight + i * 30, 20, 20), "-"))
            {
                player.playerStats.SetStats(i, -1);
            }

            GUI.Label(new Rect(Screen.width - 140, currentHeight + i * 30, 100, 20), stat.baseStatName + ": " + stat.finalStat);

            if (GUI.Button(new Rect(Screen.width - 40, currentHeight + i * 30, 20, 20), "+"))
            {
                player.playerStats.SetStats(i, 1);
            }
        }
    }

    private void CustomiseOnGUI()
    {
        GUI.Box(new Rect(10, 10, 120, 210), "Visuals");
        int currentHeight = 40;

        for (int i = 0; i < names.Length; i++)
        {
            if (GUI.Button(new Rect(20, currentHeight + i * 30, 20, 20), "<"))
            {
                SetTexture(names[i], -1);
            }

            GUI.Label(new Rect(45, currentHeight + i * 30, 60, 20), names[i]);

            if (GUI.Button(new Rect(100, currentHeight + i * 30, 20, 20), ">"))
            {
                SetTexture(names[i], 1);

            }
        }
    }
}

