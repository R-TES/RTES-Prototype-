using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomPlayerInitialSelection : MonoBehaviour
{
    // Start is called before the first frame update
    TMP_Dropdown dropdown;
    void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        List<TMP_Dropdown.OptionData> menuOptions = dropdown.GetComponent<TMP_Dropdown>().options;
        int random_index = Random.Range(0, menuOptions.Count);
        dropdown.value = random_index;
    }


}
