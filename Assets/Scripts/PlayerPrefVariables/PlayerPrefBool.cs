using UnityEngine;

namespace PlayerPrefVariables
{
    public class PlayerPrefBool
    {
        private readonly string _key;

        public PlayerPrefBool(string key)
        {
            _key = key;
        }

        public bool Get()
        {
            return PlayerPrefs.GetInt(_key) != 0;
        }

        public void Set(bool value)
        {
            PlayerPrefs.SetInt(_key, value ? 1 : 0);
        }

        public void Delete()
        {
            PlayerPrefs.DeleteKey(_key);
        }
    }
}