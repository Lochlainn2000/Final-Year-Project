using UnityEngine;
using System.Collections.Generic;

public class PlayerPrefsManager : MonoBehaviour
{
    // Function to save a list of strings to PlayerPrefs
    public static void SaveStringList(List<string> list, string key)
    {
        PlayerPrefs.SetInt(key + "_Count", list.Count); // Save the count of the list

        for (int i = 0; i < list.Count; i++)
        {
            PlayerPrefs.SetString(key + "_" + i, list[i]); // Save each string in the list
        }

        PlayerPrefs.Save(); // Save changes to PlayerPrefs
    }

    // Function to load a list of strings from PlayerPrefs
    public static List<string> LoadStringList(string key)
    {
        List<string> list = new List<string>();
        print(1);
        int count = PlayerPrefs.GetInt(key + "_Count", 0); // Get the count of the list from PlayerPrefs
        print(2);

        for (int i = 0; i < count; i++)
        {
            string value = PlayerPrefs.GetString(key + "_" + i); // Load each string from PlayerPrefs
            list.Add(value);
        }

        print(3);
        return list;
    }
}
