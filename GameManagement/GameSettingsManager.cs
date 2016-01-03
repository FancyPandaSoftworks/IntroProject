using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class GameSettingsManager
{
    protected Dictionary<string, string> stringSettings;

    public GameSettingsManager()
    {
        stringSettings = new Dictionary<string, string>();
    }

    public void SetValue(string key, string value)
    {
        stringSettings[key] = value;
    }
    //gives a string a value
    public string GetValue(string key)
    {
        if (stringSettings.ContainsKey(key))
            return stringSettings[key];
        else
            return "";
    }
}
