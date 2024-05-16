using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject hudPanel;
    public bool isHudShow = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleHud();
        }
    }

    public void ToggleHud()
    {
        isHudShow = !isHudShow;
        if (isHudShow)
        {
            hudPanel.SetActive(true);
        }
        else
        {
            hudPanel.SetActive(false);
        }
    }

}
