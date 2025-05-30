using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerLevel : MonoBehaviour
{
    [Header(" Settings ")]
    private int requiredXp;
    private int currentXp;
    private int level;
    private int levelsEarnedThisWave;

    [Header(" Visuals ")]
    [SerializeField] private Slider xpBar;
    [SerializeField] private TextMeshProUGUI levelText;

    private void Awake()
    {
        Essence.onCollected += EssenceCollectedCallBack;
    }

    private void OnDestroy()
    {
        Essence.onCollected -= EssenceCollectedCallBack;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateRequiredXp();
        UpdateVisuals();
    }

    private void UpdateRequiredXp() => requiredXp = (level + 1) * 5;
    private void UpdateVisuals()
    {
        xpBar.value = (float)currentXp / requiredXp;
        levelText.text = "LVL " + (level + 1);
    }

    private void EssenceCollectedCallBack(Essence essence)
    {
        currentXp++;

        if(currentXp >= requiredXp)
            LevelUp();

        UpdateVisuals();
    }

    private void LevelUp()
    {
        level++;
        levelsEarnedThisWave++;
        currentXp = 0;

        UpdateRequiredXp();
    }

    public bool HasLeveledUp()
    {
        if(levelsEarnedThisWave > 0)
        {
            levelsEarnedThisWave--;
            return true;
        }

        return false;
    }
}
