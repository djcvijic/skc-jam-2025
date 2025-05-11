using UnityEngine;

namespace PlayerPrefVariables
{
    public class PlayerPrefInt
    {
        private readonly string _key;
        private readonly int _defaultValue;

        public PlayerPrefInt(string key, int defaultValue = 0)
        {
            _key = key;
            _defaultValue = defaultValue;
        }

        public int Get()
        {
            return PlayerPrefs.GetInt(_key, _defaultValue);
        }

        public void Set(int value)
        {
            PlayerPrefs.SetInt(_key, value);
        }

        public void Delete()
        {
            PlayerPrefs.DeleteKey(_key);
        }
    }
}