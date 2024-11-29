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

        for (int i = 0; i <= levelsCleared; i++)
        {
            if (i < levels.Length)
            {
                levels[i].interactable = true; 
            }
        }
    }
}
