using UnityEngine;

namespace PlayerPrefVariables
{
    public class PlayerPrefFloat
    {
        private readonly string _key;
        private readonly float _defaultValue;

        public PlayerPrefFloat(string key, float defaultValue = 0.0f)
        {
            _key = key;
            _defaultValue = defaultValue;
        }

        public float Get()
        {
            return PlayerPrefs.GetFloat(_key, _defaultValue);
        }

        public void Set(float value)
        {
            PlayerPrefs.SetFloat(_key, value);
        }

        public void Delete()
        {
            PlayerPrefs.DeleteKey(_key);
        }
    }
}