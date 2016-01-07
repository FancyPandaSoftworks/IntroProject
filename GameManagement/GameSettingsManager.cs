using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class GameSettingsManager
{
    protected Dictionary<string, string> stringSettings;

    /// <summary>
    /// Manages the settings
    /// </summary>
    public GameSettingsManager()
    {
        stringSettings = new Dictionary<string, string>();
    }

    /// <summary>
    /// Set the value of a certain setting
    /// </summary>
    /// <param name="key">The setting which value should be changed</param>
    /// <param name="value">The value in which the setting needs to be changed</param>
    public void SetValue(string key, string value)
    {
        stringSettings[key] = value;
    }
    
    /// <summary>
    /// Get the value of the requested setting
    /// </summary>
    /// <param name="key">The setting which value is requested</param>
    /// <returns>Returns the value of the setting or an empty string</returns>
    public string GetValue(string key)
    {
        if (stringSettings.ContainsKey(key))
            return stringSettings[key];
        else
            return "";
    }
}
