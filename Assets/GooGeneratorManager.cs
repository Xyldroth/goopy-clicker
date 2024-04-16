using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GooGeneratorManager : MonoBehaviour
{
    public Button button;
    public int gooCount;

    [Header("UI Text")]
    public TMP_Text gooCountText;
    public TMP_Text autoGenerateRateCountText;
    public TMP_Text gpcCountText;

    [Header("Can Upgrade Note")]
    public GameObject canUpdateAutoGenNote;
    public GameObject canUpdateGPCNote;

    [Header("Auto Generation Upgrade Settings")]
    public bool canUpgradeGeneration = false;

    public int generationLevel = 0;
    public int autoGenerationCount = 0;
    public float autoGenerateRate = 1f;
    public float nextGenerationTime = 0f;

    [Header("Goo-Per-Click Upgrade Settings")]
    public bool canUpgradeGPC = false;
    public int gpcLevel = 0;
    public int gpcCount = 1;

    // Update is called once per frame
    void Update()
    {
        gooCountText.text = gooCount.ToString();
        gpcCountText.text = "+" + gpcCount;
        autoGenerateRateCountText.text = autoGenerationCount + "/s";
        canUpdateAutoGenNote.SetActive(canUpgradeGeneration);
        canUpdateGPCNote.SetActive(canUpgradeGPC);

        if (autoGenerateRate != 0 && Time.time > nextGenerationTime)
        {
            nextGenerationTime = Time.time + autoGenerateRate;
            gooCount += autoGenerationCount;
        }

        if (gooCount >= 20 && generationLevel == 0)
        {
            canUpgradeGeneration = true;
        }
        else if (gooCount >= 50 && generationLevel == 1)
        {
            canUpgradeGeneration = true;
        }
        else if (gooCount >= 500 && generationLevel == 2)
        {
            canUpgradeGeneration = true;
        }
        else
        {
            canUpgradeGeneration = false;
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

    public void OnAutoGenButtonClicked()
    {
        if(canUpgradeGeneration)
        {
            if (gooCount >= 20 && generationLevel == 0)
            {
                gooCount -= 20;
                canUpgradeGeneration = false;
                autoGenerateRate = 1f;
                autoGenerationCount = 1;
                generationLevel = 1;
                Debug.Log($"Generating Goo at the rate of: {autoGenerationCount}/s");
            }
            else if (gooCount >= 50 && generationLevel == 1)
            {
                gooCount -= 50;
                canUpgradeGeneration = false;
                autoGenerationCount = 5;
                generationLevel = 2;
                Debug.Log($"Generating Goo at the rate of: {autoGenerationCount}/s");
            }
            else if (gooCount >= 500 && generationLevel == 2)
            {
                gooCount -= 500;
                canUpgradeGeneration = false;
                autoGenerationCount = 10;
                generationLevel = 3;
                Debug.Log($"Generating Goo at the rate of: {autoGenerationCount}/s");
            }
        }
        else
        {
            Debug.Log("Cannot unlock Auto Generation yet!");
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
}
