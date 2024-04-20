using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GooGeneratorManager : MonoBehaviour
{
    public Button button;
    public int gooCount = 0;

    [Header("UI Text")]
    public TMP_Text gooCountText;
    public TMP_Text gpcCountText;

    [Header("Can Upgrade Note")]
    public GameObject canUpdateMilkingNote;
    public GameObject canUpdateLabNote;
    public GameObject canUpdateGPCNote;

    [Header("Milking Room Upgrade Settings")]
    public bool canUpgradeMilking = false;

    public int milking_Level = 0;
    public TMP_Text milking_LevelText;

    public int milking_genCount = 0;
    public float milking_genRate = 0f;

    public TMP_Text milking_genRateText;
    public float milking_nextGenerationTime = 1f;

    private Dictionary<int, Tuple<int, int>> milkingRequirements = new Dictionary<int, Tuple<int, int>>()
    {
        { 1, new Tuple<int, int>(10, 1) },
        { 2, new Tuple<int, int>(20, 2) },
        { 3, new Tuple<int, int>(30, 3) },
        { 4, new Tuple<int, int>(40, 4) },
        { 5, new Tuple<int, int>(50, 5) }
    };

    [Header("Lab Room Upgrade Settings")]
    public bool canUpgradeLab = false;

    public int lab_Level = 0;
    public TMP_Text lab_LevelText;

    public int lab_genCount = 0;
    public float lab_genRate = 0f;

    public TMP_Text lab_genRateText;
    public float lab_nextGenerationTime = 1f;

    private Dictionary<int, Tuple<int, int>> labRequirements = new Dictionary<int, Tuple<int, int>>()
    {
        { 1, new Tuple<int, int>(50, 5) },
        { 2, new Tuple<int, int>(100, 10) },
        { 3, new Tuple<int, int>(200, 20) },
        { 4, new Tuple<int, int>(400, 40) },
        { 5, new Tuple<int, int>(800, 80) }
    };

    [Header("Goo-Per-Click Upgrade Settings")]
    public bool canUpgradeGPC = false;
    public int gpcLevel = 0;
    public int gpcCount = 1;

    private string jsonDataPath;

    private void Awake()
    {
        // Get the path for the JSON data file
        jsonDataPath = Path.Combine(Application.persistentDataPath, "requirementsData.json");
        LoadData();
    }

    private void LoadData()
    {
        if (File.Exists(jsonDataPath))
        {
            // Read the JSON data from the file
            string jsonData = File.ReadAllText(jsonDataPath);

            // Convert the JSON data back to the serializable object
            SerializableRequirementsData requirementsData = JsonUtility.FromJson<SerializableRequirementsData>(jsonData);

            // Update the variables with the loaded data
            milking_Level = requirementsData.milking_Level;
            milking_genCount = requirementsData.milking_genCount;
            milking_genRate = requirementsData.milking_genRate;

            lab_Level = requirementsData.lab_Level;
            lab_genCount = requirementsData.lab_genCount;
            lab_genRate = requirementsData.lab_genRate;

            gooCount = requirementsData.gooCount;
        }
        else
        {
            Debug.LogWarning("Requirements data file not found. Using default values.");
            SerializableRequirementsData requirementsData = new SerializableRequirementsData();
            SaveData();
        }
    }

    private void SaveData()
    {
        // Create a serializable object to hold the data
        SerializableRequirementsData requirementsData = new SerializableRequirementsData();
        requirementsData.milking_Level = milking_Level;
        requirementsData.milking_genCount = milking_genCount;
        requirementsData.milking_genRate = milking_genRate;

        requirementsData.lab_Level = lab_Level;
        requirementsData.lab_genCount = lab_genCount;
        requirementsData.lab_genRate = lab_genRate;

        requirementsData.gooCount = gooCount;

        // Convert the object to JSON format
        string jsonData = JsonUtility.ToJson(requirementsData, true);

        // Save the JSON data to a file
        File.WriteAllText(jsonDataPath, jsonData);
    }

    [Serializable]
    private class SerializableRequirementsData
    {
        public int gooCount;

        public int milking_Level;
        public int milking_genCount;
        public float milking_genRate;
        public int lab_Level;
        public int lab_genCount;
        public float lab_genRate;
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    // Update is called once per frame
    void Update()
    {
        gooCountText.text = gooCount.ToString();
        gpcCountText.text = "+" + gpcCount;
        milking_genRateText.text = milking_genCount + "/s";
        lab_genRateText.text = lab_genCount + "/s";

        milking_LevelText.text = "Lv. " + milking_Level;
        lab_LevelText.text = "Lv. " + lab_Level;

        canUpdateMilkingNote.SetActive(canUpgradeMilking);
        canUpdateLabNote.SetActive(canUpgradeLab);
        canUpdateGPCNote.SetActive(canUpgradeGPC);

        if (milking_genRate != 0 && Time.time > milking_nextGenerationTime)
        {
            milking_nextGenerationTime = Time.time + milking_genRate;
            gooCount += milking_genCount;
        }

        if (lab_genRate != 0 && Time.time > lab_nextGenerationTime)
        {
            lab_nextGenerationTime = Time.time + lab_genRate;
            gooCount += lab_genCount;
        }

        if (gooCount >= milkingRequirements.GetValueOrDefault(milking_Level + 1, new Tuple<int, int>(int.MaxValue, int.MaxValue)).Item1)
        {
            canUpgradeMilking = true;
        }
        else
        {
            canUpgradeMilking = false;
        }


        if (gooCount >= labRequirements.GetValueOrDefault(lab_Level + 1, new Tuple<int, int>(int.MaxValue, int.MaxValue)).Item1)
        {
            canUpgradeLab = true;
        }
        else
        {
            canUpgradeLab = false;
        }

        if (gooCount >= 20 && gpcLevel == 0)
        {
            canUpgradeGPC = true;
        }
        else if (gooCount >= 50 && gpcLevel == 1)
        {
            canUpgradeGPC = true;
        }
        else if (gooCount >= 500 && gpcLevel == 2)
        {
            canUpgradeGPC = true;
        }
        else
        {
            canUpgradeGPC = false;
        }
    }

    public void OnButtonClicked()
    {
        gooCount += gpcCount;
    }

    public void OnMilkingRoomButtonClicked()
    {
        if (canUpgradeMilking)
        {
            int nextLevel = milking_Level + 1;
            if (gooCount >= milkingRequirements.GetValueOrDefault(nextLevel, new Tuple<int, int>(int.MaxValue, int.MaxValue)).Item1)
            {
                gooCount -= milkingRequirements[nextLevel].Item1;
                milking_genRate = 1f;
                milking_genCount = milkingRequirements[nextLevel].Item2;
                milking_Level = nextLevel;
                Debug.Log($"Generating Goo at the rate of: {milking_genCount}/s");
            }
        }
        else
        {
            Debug.Log("Cannot upgrade Milking Room yet!");
        }
    }

    public void OnLabRoomButtonClicked()
    {
        if (canUpgradeLab)
        {
            int nextLevel = lab_Level + 1;
            if (gooCount >= labRequirements.GetValueOrDefault(nextLevel, new Tuple<int, int>(int.MaxValue, int.MaxValue)).Item1)
            {
                gooCount -= labRequirements[nextLevel].Item1;
                lab_genRate = 1f;
                lab_genCount = labRequirements[nextLevel].Item2;
                lab_Level = nextLevel;
                Debug.Log($"Generating Goo at the rate of: {lab_genCount}/s");
            }
        }
        else
        {
            Debug.Log("Cannot upgrade Lab Room yet!");
        }
    }

    public void OnGPCClicked()
    {
        if (canUpgradeGPC)
        {
            if (gooCount >= 20 && gpcLevel == 0)
            {
                gooCount -= 20;
                canUpgradeGPC = false;
                gpcCount = 2;
                gpcLevel = 1;
                Debug.Log($"Generating {gpcCount} Goo Per Click");
            }
            else if (gooCount >= 50 && gpcLevel == 1)
            {
                gooCount -= 50;
                canUpgradeGPC = false;
                gpcCount = 5;
                gpcLevel = 2;
                Debug.Log($"Generating {gpcCount} Goo Per Click");
            }
            else if (gooCount >= 500 && gpcLevel == 2)
            {
                gooCount -= 500;
                canUpgradeGPC = false;
                gpcCount = 10;
                gpcLevel = 3;
                Debug.Log($"Generating {gpcCount} Goo Per Click");
            }
        }
        else
        {
            Debug.Log("Cannot unlock Goo Per Click yet!");
        }
    }

    public void OnSaveClicked()
    {
        SaveData();
    }
}