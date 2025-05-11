using System;
using Newtonsoft.Json;
using UnityEngine;

namespace PlayerPrefVariables
{
    public class PlayerPref<T>
    {
        private readonly string _key;
        private readonly T _defaultValue;

        public PlayerPref(string key, T defaultValue = default)
        {
            _key = key;
            _defaultValue = defaultValue;
        }

        public T Get()
        {
            if (!PlayerPrefs.HasKey(_key)) return _defaultValue;

            try
            {
                return JsonConvert.DeserializeObject<T>(PlayerPrefs.GetString(_key, ""))
                       ?? _defaultValue;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return _defaultValue;
            }
        }

        public void Set(T value)
        {
            PlayerPrefs.SetString(_key, JsonConvert.SerializeObject(value));
        }

        public void Delete()
        {
            PlayerPrefs.DeleteKey(_key);
            PlayerPrefs.Save();
        }

        public T GetOrSetIfMissing(T value)
        {
            if (PlayerPrefs.HasKey(_key)) return Get();

            Set(value);
            return value;
        }
    }
}