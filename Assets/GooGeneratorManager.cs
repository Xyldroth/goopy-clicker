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
    public long gooCount = 0;

    [Header("UI Text")]
    public TMP_Text gooCountText;
    public TMP_Text totalGenText;

    public TMP_Text gpcCountText;

    [Header("Can Upgrade Note")]
    public GameObject canUpdateMilkingNote;
    public GameObject canUpdateLabNote;
    public GameObject canUpdateConversionNote;
    public GameObject canUpdateGPCNote;

    [Header("Milking Room Upgrade Settings")]
    public bool canUpgradeMilking = false;

    public long milking_Level = 0;
    public TMP_Text milking_LevelText;

    public long milking_genCount = 0;
    public float milking_genRate = 0f;

    public TMP_Text milking_genRateText;
    public float milking_nextGenerationTime = 1f;

    private Dictionary<long, Tuple<long, long>> milkingRequirements = new Dictionary<long, Tuple<long, long>>()
    {
        { 1, new Tuple<long, long>(10, 1) },
        { 2, new Tuple<long, long>(20, 2) },
        { 3, new Tuple<long, long>(30, 3) },
        { 4, new Tuple<long, long>(40, 4) },
        { 5, new Tuple<long, long>(50, 5) },
        { 6, new Tuple<long, long>(60, 6) },
        { 7, new Tuple<long, long>(70, 7) },
        { 8, new Tuple<long, long>(80, 8) },
        { 9, new Tuple<long, long>(90, 9) },
        { 10, new Tuple<long, long>(100, 10) },
        { 11, new Tuple<long, long>(110, 11) },
        { 12, new Tuple<long, long>(120, 12) },
        { 13, new Tuple<long, long>(130, 13) },
        { 14, new Tuple<long, long>(140, 14) },
        { 15, new Tuple<long, long>(150, 15) },
        { 16, new Tuple<long, long>(160, 16) },
        { 17, new Tuple<long, long>(170, 17) },
        { 18, new Tuple<long, long>(180, 18) },
        { 19, new Tuple<long, long>(190, 19) },
        { 20, new Tuple<long, long>(200, 20) },
        { 21, new Tuple<long, long>(210, 21) },
        { 22, new Tuple<long, long>(220, 22) },
        { 23, new Tuple<long, long>(230, 23) },
        { 24, new Tuple<long, long>(240, 24) },
        { 25, new Tuple<long, long>(250, 25) },
        { 26, new Tuple<long, long>(260, 26) },
        { 27, new Tuple<long, long>(270, 27) },
        { 28, new Tuple<long, long>(280, 28) },
        { 29, new Tuple<long, long>(290, 29) },
        { 30, new Tuple<long, long>(300, 30) },
        { 31, new Tuple<long, long>(310, 31) },
        { 32, new Tuple<long, long>(320, 32) },
        { 33, new Tuple<long, long>(330, 33) },
        { 34, new Tuple<long, long>(340, 34) },
        { 35, new Tuple<long, long>(350, 35) },
        { 36, new Tuple<long, long>(360, 36) },
        { 37, new Tuple<long, long>(370, 37) },
        { 38, new Tuple<long, long>(380, 38) },
        { 39, new Tuple<long, long>(390, 39) },
        { 40, new Tuple<long, long>(400, 40) },
        { 41, new Tuple<long, long>(410, 41) },
        { 42, new Tuple<long, long>(420, 42) },
        { 43, new Tuple<long, long>(430, 43) },
        { 44, new Tuple<long, long>(440, 44) },
        { 45, new Tuple<long, long>(450, 45) },
        { 46, new Tuple<long, long>(460, 46) },
        { 47, new Tuple<long, long>(470, 47) },
        { 48, new Tuple<long, long>(480, 48) },
        { 49, new Tuple<long, long>(490, 49) },
        { 50, new Tuple<long, long>(500, 50) }
    };

    public TMP_Text milking_RequirementText;
    
    [Header("Lab Room Upgrade Settings")]
    public bool canUpgradeLab = false;

    public long lab_Level = 0;
    public TMP_Text lab_LevelText;

    public long lab_genCount = 0;
    public float lab_genRate = 0f;

    public TMP_Text lab_genRateText;
    public float lab_nextGenerationTime = 1f;

    public TMP_Text lab_RequirementText;

    private Dictionary<long, Tuple<long, long>> labRequirements = new Dictionary<long, Tuple<long, long>>()
    {
        { 1, new Tuple<long, long>(50, 5) },
        { 2, new Tuple<long, long>(100, 10) },
        { 3, new Tuple<long, long>(150, 15) },
        { 4, new Tuple<long, long>(200, 20) },
        { 5, new Tuple<long, long>(250, 25) },
        { 6, new Tuple<long, long>(300, 30) },
        { 7, new Tuple<long, long>(350, 35) },
        { 8, new Tuple<long, long>(400, 40) },
        { 9, new Tuple<long, long>(450, 45) },
        { 10, new Tuple<long, long>(500, 50) },
        { 11, new Tuple<long, long>(550, 55) },
        { 12, new Tuple<long, long>(600, 60) },
        { 13, new Tuple<long, long>(650, 65) },
        { 14, new Tuple<long, long>(700, 70) },
        { 15, new Tuple<long, long>(750, 75) },
        { 16, new Tuple<long, long>(800, 80) },
        { 17, new Tuple<long, long>(850, 85) },
        { 18, new Tuple<long, long>(900, 90) },
        { 19, new Tuple<long, long>(950, 95) },
        { 20, new Tuple<long, long>(1000, 100) },
        { 21, new Tuple<long, long>(1050, 105) },
        { 22, new Tuple<long, long>(1100, 110) },
        { 23, new Tuple<long, long>(1150, 115) },
        { 24, new Tuple<long, long>(1200, 120) },
        { 25, new Tuple<long, long>(1250, 125) },
        { 26, new Tuple<long, long>(1300, 130) },
        { 27, new Tuple<long, long>(1350, 135) },
        { 28, new Tuple<long, long>(1400, 140) },
        { 29, new Tuple<long, long>(1450, 145) },
        { 30, new Tuple<long, long>(1500, 150) },
        { 31, new Tuple<long, long>(1550, 155) },
        { 32, new Tuple<long, long>(1600, 160) },
        { 33, new Tuple<long, long>(1650, 165) },
        { 34, new Tuple<long, long>(1700, 170) },
        { 35, new Tuple<long, long>(1750, 175) },
        { 36, new Tuple<long, long>(1800, 180) },
        { 37, new Tuple<long, long>(1850, 185) },
        { 38, new Tuple<long, long>(1900, 190) },
        { 39, new Tuple<long, long>(1950, 195) },
        { 40, new Tuple<long, long>(2000, 200) },
        { 41, new Tuple<long, long>(2050, 205) },
        { 42, new Tuple<long, long>(2100, 210) },
        { 43, new Tuple<long, long>(2150, 215) },
        { 44, new Tuple<long, long>(2200, 220) },
        { 45, new Tuple<long, long>(2250, 225) },
        { 46, new Tuple<long, long>(2300, 230) },
        { 47, new Tuple<long, long>(2350, 235) },
        { 48, new Tuple<long, long>(2400, 240) },
        { 49, new Tuple<long, long>(2450, 245) },
        { 50, new Tuple<long, long>(2500, 250) }
    };

    [Header("Goo-Per-Click Upgrade Settings")]
    public bool canUpgradeGPC = false;
    public long gpcLevel = 0;
    public long gpcCount = 1;

    public TMP_Text gpc_RequirementText;

    private Dictionary<long, Tuple<long, long>> gpcRequirements = new Dictionary<long, Tuple<long, long>>()
    {
        { 1, new Tuple<long, long>(10, 2) },
        { 2, new Tuple<long, long>(25, 3) },
        { 3, new Tuple<long, long>(50, 4) },
        { 4, new Tuple<long, long>(100, 5) },
        { 5, new Tuple<long, long>(500, 10) },
        { 6, new Tuple<long, long>(1000, 20) },
        { 7, new Tuple<long, long>(2000, 50) },
        { 8, new Tuple<long, long>(5000, 100) },
        { 9, new Tuple<long, long>(10000, 200) },
        { 10, new Tuple<long, long>(50000, 500) },
        { 11, new Tuple<long, long>(100000, 1000) },
    };

    [Header("Drone Conversion Settings")]
    public bool canConvert = false;
    public long droneCount = 1;
    public float conversionChance = 50f;
    public long conversion_Requirement = 100000;
    public TMP_Text conversion_droneCountText;
    public TMP_Text conversion_RequirementText;

    private string jsonDataPath;


    private void Awake()
    {
        // Get the path for the JSON data file
        jsonDataPath = Path.Combine(Application.persistentDataPath, "SaveData.json");
        LoadData();
    }

    private void LoadData()
    {
        if (File.Exists(jsonDataPath))
        {
            // Read the JSON data from the file
            string jsonData = File.ReadAllText(jsonDataPath);

            // Convert the JSON data back to the serializable object
            SerializableRequirementsData sd = JsonUtility.FromJson<SerializableRequirementsData>(jsonData);

            // Update the variables with the loaded data
            milking_Level = sd.milking_Level;
            milking_genCount = sd.milking_genCount;
            milking_genRate = sd.milking_genRate;

            lab_Level = sd.lab_Level;
            lab_genCount = sd.lab_genCount;
            lab_genRate = sd.lab_genRate;

            gooCount = sd.gooCount;

            gpcLevel = sd.gpcLevel;
            gpcCount = sd.gpcCount;

            droneCount = sd.droneCount;

            Debug.Log("Loaded Data!");
        }
        else
        {
            Debug.LogWarning("Requirements data file not found. Using default values.");
            SerializableRequirementsData sd = new SerializableRequirementsData();
            SaveData();
        }
    }

    private void SaveData()
    {
        // Create a serializable object to hold the data
        SerializableRequirementsData sd = new SerializableRequirementsData();
        sd.milking_Level = milking_Level;
        sd.milking_genCount = milking_genCount;
        sd.milking_genRate = milking_genRate;

        sd.lab_Level = lab_Level;
        sd.lab_genCount = lab_genCount;
        sd.lab_genRate = lab_genRate;

        sd.gooCount = gooCount;

        sd.gpcLevel = gpcLevel;
        sd.gpcCount = gpcCount;

        sd.droneCount = droneCount;

        // Convert the object to JSON format
        string jsonData = JsonUtility.ToJson(sd, true);

        // Save the JSON data to a file
        File.WriteAllText(jsonDataPath, jsonData);

        Debug.Log("Saved Data!");
    }

    [Serializable]
    private class SerializableRequirementsData
    {
        public long gooCount;

        public long milking_Level;
        public long milking_genCount;
        public float milking_genRate;
        public long lab_Level;
        public long lab_genCount;
        public float lab_genRate;

        public long gpcLevel;
        public long gpcCount;

        public long droneCount;
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
        milking_genRateText.text = (milking_genCount * droneCount) + "/s";
        lab_genRateText.text = (lab_genCount * droneCount) + "/s";
        conversion_droneCountText.text = droneCount.ToString();
        conversion_RequirementText.text = conversion_Requirement.ToString();

        long totalGenCount = (milking_genCount + lab_genCount) * droneCount;
        totalGenText.text = totalGenCount + "/s";

        long milking_nextLevel = milking_Level + 1;
        Tuple<long, long> milking_requirementTuple;
        string milking_requirementText = "";
        if (milkingRequirements.TryGetValue(milking_nextLevel, out milking_requirementTuple))
        {
            milking_requirementText = milking_requirementTuple.Item1.ToString();
        }
        else
        {
            milking_requirementText = "Max level reached";
        }
        milking_RequirementText.text = milking_requirementText;

        long lab_nextLevel = lab_Level + 1;
        Tuple<long, long> lab_requirementTuple;
        string lab_requirementText = "";
        if (labRequirements.TryGetValue(lab_nextLevel, out lab_requirementTuple))
        {
            lab_requirementText = lab_requirementTuple.Item1.ToString();
        }
        else
        {
            lab_requirementText = "Max level reached";
        }
        lab_RequirementText.text = lab_requirementText;

        long gpc_nextLevel = gpcLevel + 1;
        Tuple<long, long> gpc_requirementTuple;
        string gpc_requirementText = "";
        if (gpcRequirements.TryGetValue(gpc_nextLevel, out gpc_requirementTuple))
        {
            gpc_requirementText = gpc_requirementTuple.Item1.ToString();
        }
        else
        {
            gpc_requirementText = "Max level reached";
        }
        gpc_RequirementText.text = gpc_requirementText;

        milking_LevelText.text = "Lv. " + milking_Level;
        lab_LevelText.text = "Lv. " + lab_Level;

        canUpdateMilkingNote.SetActive(canUpgradeMilking);
        canUpdateLabNote.SetActive(canUpgradeLab);
        canUpdateGPCNote.SetActive(canUpgradeGPC);
        canUpdateConversionNote.SetActive(canConvert);

        if (milking_genRate != 0 && Time.time > milking_nextGenerationTime)
        {
            milking_nextGenerationTime = Time.time + milking_genRate;
            gooCount += milking_genCount * droneCount;
        }

        if (lab_genRate != 0 && Time.time > lab_nextGenerationTime)
        {
            lab_nextGenerationTime = Time.time + lab_genRate;
            gooCount += lab_genCount * droneCount;
        }

        if (gooCount >= milkingRequirements.GetValueOrDefault(milking_Level + 1, new Tuple<long, long>(long.MaxValue, long.MaxValue)).Item1)
        {
            canUpgradeMilking = true;
        }
        else
        {
            canUpgradeMilking = false;
        }


        if (gooCount >= labRequirements.GetValueOrDefault(lab_Level + 1, new Tuple<long, long>(long.MaxValue, long.MaxValue)).Item1)
        {
            canUpgradeLab = true;
        }
        else
        {
            canUpgradeLab = false;
        }

        if (gooCount >= gpcRequirements.GetValueOrDefault(gpcLevel + 1, new Tuple<long, long>(long.MaxValue, long.MaxValue)).Item1)
        {
            canUpgradeGPC = true;
        }
        else
        {
            canUpgradeGPC = false;
        }

        if(gooCount >= conversion_Requirement)
        {
            canConvert = true;
        }
        else
        {
            canConvert = false;
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
            long nextLevel = milking_Level + 1;
            if (gooCount >= milkingRequirements.GetValueOrDefault(nextLevel, new Tuple<long, long>(long.MaxValue, long.MaxValue)).Item1)
            {
                gooCount -= milkingRequirements[nextLevel].Item1;
                milking_genRate = 1f;
                milking_genCount = milkingRequirements[nextLevel].Item2;
                milking_Level = nextLevel;
                Debug.Log($"Generating Goo at the rate of: {milking_genCount * droneCount}/s");
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
            long nextLevel = lab_Level + 1;
            if (gooCount >= labRequirements.GetValueOrDefault(nextLevel, new Tuple<long, long>(long.MaxValue, long.MaxValue)).Item1)
            {
                gooCount -= labRequirements[nextLevel].Item1;
                lab_genRate = 1f;
                lab_genCount = labRequirements[nextLevel].Item2;
                lab_Level = nextLevel;
                Debug.Log($"Generating Goo at the rate of: {lab_genCount * droneCount}/s");
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
            long nextLevel = gpcLevel + 1;
            if (gooCount >= gpcRequirements.GetValueOrDefault(nextLevel, new Tuple<long, long>(long.MaxValue, long.MaxValue)).Item1)
            {
                gooCount -= gpcRequirements[nextLevel].Item1;
                canUpgradeGPC = false;
                gpcCount = gpcRequirements[nextLevel].Item2;
                gpcLevel = nextLevel;
                Debug.Log($"Generating {gpcCount} Goo Per Click");
            }
        }
        else
        {
            Debug.Log("Cannot unlock Goo Per Click yet!");
        }
    }

    public void OnConvertClicked()
    {
        if (canConvert)
        {
            if (gooCount >= conversion_Requirement)
            {
                gooCount -= conversion_Requirement;

                // Generate a random number between 0 and 1
                float randomValue = UnityEngine.Random.value;
                float conversionChance_Percentage = conversionChance / 100f;

                if (randomValue < conversionChance_Percentage)
                {
                    // Conversion failed
                    Debug.Log($"Drone conversion failed!\n {randomValue} < {conversionChance_Percentage}");
                }
                else
                {
                    droneCount += 1;
                    Debug.Log($"Converted new drone. Total drone count: {droneCount}\n {randomValue} >= {conversionChance_Percentage}");
                }
            }
        }
        else
        {
            Debug.Log("Cannot convert yet!");
        }
    }

    public void OnSaveClicked()
    {
        SaveData();
    }
}