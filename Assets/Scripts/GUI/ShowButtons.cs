using System.Collections.Generic;
using UnityEngine;

public class ShowButtons : MonoBehaviour
{
    [SerializeField] private List<GameObject> menuButtons;

    public void ShowMenuButtons()
    {
        menuButtons.ForEach(x => x.SetActive(true));
    }
}
