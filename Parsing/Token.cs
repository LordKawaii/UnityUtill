using UnityEngine;
using System.Collections;

public class Token {

    public readonly string name;
    public readonly string value;

    public Token(string tokenName, string tokenValue)
    {
        name = tokenName;
        value = tokenValue;
    }
}
