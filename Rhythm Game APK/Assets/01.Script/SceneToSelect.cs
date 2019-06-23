using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneToSelect : MonoBehaviour
{
    public void ReturnToSelect()
    {
        SceneManager.LoadScene("Select");
    }
}
