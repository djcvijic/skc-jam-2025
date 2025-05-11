using UnityEngine;

namespace PlayerPrefVariables
{
    public class PlayerPrefString
    {
        private readonly string _key;
        private readonly string _defaultValue;

        public PlayerPrefString(string key, string defaultValue = null)
        {
            _key = key;
            _defaultValue = defaultValue;
        }

        public string Get()
        {
            return PlayerPrefs.GetString(_key, _defaultValue);
        }

        public void Set(string value)
        {
            PlayerPrefs.SetString(_key, value);
        }

        public void Delete()
        {
            PlayerPrefs.DeleteKey(_key);
        }
    }
}