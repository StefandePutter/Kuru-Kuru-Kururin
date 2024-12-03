using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] private Button[] levels;
    private int levelsCleared;

    private void Start()
    {
        levelsCleared = SaveLoad.LoadLevel();
        
        // for levels cleared make buttons clickable
        for (int i = 0; i <= levelsCleared; i++)
        {
            // beating the last level levelsCleared will be 1 more than the array size so need to check
            if (i < levels.Length)
            {
                levels[i].interactable = true; 
            }
        }
    }
}
