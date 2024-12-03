using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DifficultyStats
{
    public enum DifficultyState
    {
        Easy,
        Medium,
        Hard
    }
    public DifficultyState difficulty;
    public int DiffucltyPoints;
}
public class Difficulty : MonoBehaviour
{
    public List<DifficultyStats> DS = new List<DifficultyStats>();
    public DifficultyStats.DifficultyState CurrentDifficutly;
    public int CurrentDifficultyPoints;

    public void SetEasy()
    {
        SetDifficulty(DS[0]);
    }
    public void SetMedium()
    {
        SetDifficulty(DS[1]);
    }
    public void SetHard()
    {
        SetDifficulty(DS[2]);
    }
    public void SetDifficulty(DifficultyStats DifData)
    {
        CurrentDifficutly = DifData.difficulty;
        CurrentDifficultyPoints = DifData.DiffucltyPoints;
    }
    
    public DifficultyStats.DifficultyState GetDifficulty()
    {
        return CurrentDifficutly;
    }
    public int GetDifficultyPoints()
    {
        return CurrentDifficultyPoints; 
    }



}
