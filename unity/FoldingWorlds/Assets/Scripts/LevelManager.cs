using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> levelObjects;
    public int lvlIndex = 0;
    
    public void OnLevelFinished() {
        lvlIndex = (lvlIndex + 1) % levelObjects.Count; 
        for (int i = 0; i < levelObjects.Count; i++)
        {   
            var lo = levelObjects[i];
            lo.SetActive(i == lvlIndex);
        }
    }
}
