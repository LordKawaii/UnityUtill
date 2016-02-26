using UnityEngine;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public class ParsingEngine
{
    private static string stringPattern =
        @"(?<variable>[a-zA-Z_$][a-zA-Z0-9_$]*)|" +
        @"(?<float>[-+]?[0-9]*\.?[0-9]+)|";

    private Regex regPattern;
    private List<Token> tokenList;

    public ParsingEngine()
    {
            regPattern = new Regex(stringPattern);
            tokenList = new List<Token>();
    }

    public void ParseString(string input)
    {
        MatchCollection matches = regPattern.Matches(input);
        foreach (Match match in matches)
        {
            int i = 0;
            foreach (Group group in match.Groups)
            {
                string matchVal = group.Value;
                bool success = group.Success;
                //ignore index 0 (general)
                if (success && i > 0)
                {
                    string groupName = regPattern.GroupNameFromNumber(i);
                    tokenList.Add(new Token(groupName, matchVal));
                }
                i++;
            }//end foreach group
        }//end foreach match
    }//end ParseString

    public List<Token> getTokens()
    {
        return tokenList;
    }

    public List<Token> getVars()
    {
        if (tokenList.Count > 0)
        {
            List<Token> varList = new List<Token>();
            foreach (Token token in tokenList)
            {
                if (token.name == "variable")
                    varList.Add(token);
            }
            if (varList.Count > 0)
                return varList;
        }
        return null;
    }

    public List<Token> getFloats()
    {
        if (tokenList.Count > 0)
        {
            List<Token> floatList = new List<Token>();
            foreach (Token token in tokenList)
            {
                if (token.name == "float")
                    floatList.Add(token);
            }
            if (floatList.Count > 0)
                return floatList;
        }
        return null;
    }

    public void readList()//For Debugging***********************************
    {
        int i = 0;
        foreach (Token token in tokenList)
        { 
            Debug.Log("Token " + i + ": name " + token.name + " val " + token.value);
            i++;
        }
    }

}
