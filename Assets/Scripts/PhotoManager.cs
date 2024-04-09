using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Linq;

public class PhotoManager
{
    static byte[] LoadImagebytes, NewImageBytes;
    static string NewImageName;
    static Texture2D Image;

    public static Texture2D LoadPhoto(string filename)
    {
        LoadImagebytes = LoadBytes(filename);
        Texture2D texture = new Texture2D(10, 10);
        texture.LoadImage(LoadImagebytes);
        return texture;
    }
    public static Texture2D LoadPhoto()
    {
        LoadImagebytes = NewImageBytes;
        Texture2D texture = new Texture2D(10, 10);
        texture.LoadImage(LoadImagebytes);
        return texture;
    }

    public static void SetPhotoBytes(string name, byte[] b)
    {
        NewImageBytes = b;
        NewImageName = name;
    }
    public static void SavePhoto()
    {
        SaveByte(NewImageBytes, NewImageName);
    }
    public static void RemovePhoto(string key)
    {
        PlayerPrefs.DeleteKey(key);
    }

    static void SaveByte(byte[] byteArray, string key)
    {
        List<string> idList = new List<string>();
        XDocument doc = new XDocument();

        //Transforms each element of the byte [] (array) on an element of the XML
        doc.Add(new XElement("root", byteArray.Select(x => new XElement("item", x))));

        //Lets all string in a line
        string xmlString = AllStringToOneLine(doc.ToString());

        //------------------------------------------------------
        //Maximum number of "char" / letter within a row in the string
        int LineLengthMax = 200000;
        //Calculates the number of line breaks
        int NumberBreaklines = (xmlString.Length / LineLengthMax);

        //let's take the positions where we put the line break      
        for (int i = 1; i <= NumberBreaklines; i++)
        {
            //Calculate where the line break will be inserted
            int x = LineLengthMax * i;
            //We create line breaks in various positions of the string
            xmlString = xmlString.Insert(x, "\n");
        }
        //------------------------------------------------------

        //Transform each line in an element of the "List"
        idList = xmlString.Split(new[] { "\n" }, StringSplitOptions.None).ToList();

        //Saves all the elements of the List / Array
        for (int i = 0; i <= idList.ToArray().Length - 1; i++)
        {
            //Saves the information in bytes (number) in text / XML
            PlayerPrefs.SetString(key + i, idList[i]);
        }
        //Saves the number of elements of the List / Array
        PlayerPrefs.SetInt(key + "size", idList.ToArray().Length);

        Debug.Log("saved");
    }


    static byte[] LoadBytes(string key)
    {
        byte[] byteArray;
        string loadedStr = "";
        //reloads the number of lines
        int a = PlayerPrefs.GetInt(key + "size");
        for (int i = 0; i <= a - 1; i++)
        {
            //It carries all the information in bytes (number) in text / XML
            loadedStr += PlayerPrefs.GetString(key + i);
        }

        //Transforms the text into an XML document
        XDocument doc = XDocument.Parse(loadedStr);

        //=====================================================
        //Turns every element of the XML an element to the "var int array" 
        var array = doc.Descendants("item").Select(x => (int)x).ToArray();
        //=====================================================

        //Transforms each element of the "int array" an element to the "byte array" 
        byteArray = array.Select(x => (byte)x).ToArray();

        Debug.Log("Loaded");

        return byteArray;
    }



    static string AllStringToOneLine(string word)
    {
        string charRepalcedSpace = " ";
        // Remove excess spaces
        Regex rgx = new Regex("\\s+");
        // replace multiple spaces by a space
        return word = rgx.Replace(word, charRepalcedSpace);
    }

}