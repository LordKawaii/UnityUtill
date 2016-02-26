using System.IO;
using System.Collections.Generic;
using UnityEngine;


public class TextUtils
{
    public string TextFileToString(string filePath)
    {
        if (File.Exists(filePath))
        { 
            string output = "";
            string currentLine = "";

            using (StreamReader fStream = new StreamReader(filePath))
            { 
                while ((currentLine = fStream.ReadLine()) != null)
                {
                    output += currentLine + " ";
                }
        
                return output;
            }
        }
        Debug.LogError("File does not exist");
        return null;
    }

    public bool OverwriteListToFile(List<string> stringList, string filePath)
    {
        using (StreamWriter fWriter = new StreamWriter(filePath))
        { 
            foreach (string line in stringList)
            {
                fWriter.WriteLine(line);
            }
        }

        if (File.Exists(filePath))
            return true;
        else
            return false;
    }

    public bool CreateEmptyFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            File.Create(filePath);
            return true;
        }
        Debug.LogError(filePath + " Already exists");
        return false;
    }

}