using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MoreMountains.Tools;

public class LoadLevel : MonoBehaviour
{
    public string levelName;

    public void loadLevel() {
        MMSceneLoadingManager.LoadScene(levelName);
    }
}
