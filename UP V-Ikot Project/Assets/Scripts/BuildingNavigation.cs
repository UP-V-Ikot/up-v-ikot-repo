using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuildingNavigation : MonoBehaviour
{
    public void BackButtonClick() {
        SceneManager.LoadScene(1);
    }
}
