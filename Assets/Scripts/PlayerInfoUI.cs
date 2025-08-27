using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUI : MonoBehaviour
{
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI lapsCompleted;

    public void updateLaps(int lap)
    { 
        lapsCompleted.text = "Lap: " + lap + "/4";
    }
}
