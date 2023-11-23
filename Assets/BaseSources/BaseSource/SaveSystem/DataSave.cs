using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

public static class DataSave
{
    public static void SaveList<T>(PrefType prefType, List<T> list)
    {
        var str = string.Join(",", list);
        LocalPrefs.SetString(prefType.ToString(), str);
        UserPrefs.Save();
    }

    public static List<T> GetList<T>(PrefType prefType, T defaultValue)
    {
        var loadedString = LocalPrefs.GetString(prefType.ToString(), "");
        var strList = loadedString.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();

        if (defaultValue is float)
        {
            var list = new List<float>();
            for (int i = 0; i < strList.Count; i++)
            {
                var isFloat = float.TryParse(strList[i], out float result);
                if (isFloat)
                    list.Add(result);
            }

            return ChangeTypeList<T>(list);
        }

        if (defaultValue is string)
        {
            return ChangeTypeList<T>(strList);
        }

        if (defaultValue is int)
        {
            var list = new List<int>();
            for (int i = 0; i < strList.Count; i++)
            {
                var isInt = int.TryParse(strList[i], out int result);
                if (isInt)
                    list.Add(result);
            }

            return ChangeTypeList<T>(list);
        }

        if (defaultValue is double)
        {
            var list = new List<double>();
            for (int i = 0; i < strList.Count; i++)
            {
                var isDouble = double.TryParse(strList[i], out double result);
                if (isDouble)
                    list.Add(result);
            }

            return ChangeTypeList<T>(list);
        }

        if (defaultValue is long)
        {
            var list = new List<long>();
            for (int i = 0; i < strList.Count; i++)
            {
                var isLong = long.TryParse(strList[i], out long result);
                if (isLong)
                    list.Add(result);
            }

            return ChangeTypeList<T>(list);
        }

        return default;
    }

    public static List<T> GetList<T>(PrefType prefType) where T : struct
    {
        var loadedString = LocalPrefs.GetString(prefType.ToString(), "");
        var strList = loadedString.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();

        var enumList = new List<T>();

        strList.ForEach(x =>
        {
            var result = Enumeration<T>.GetEnum(x);
            if (result.HasValue)
            {
                enumList.Add(result.Value);
            }
        });
        return enumList;
    }

    public static void SaveJson<T>(PrefType prefType, T val) where T : class
    {
        var str = JsonConvert.SerializeObject(val);
        LocalPrefs.SetString(prefType.ToString(), str);
        UserPrefs.Save();
    }

    public static void SaveJson<T>(PrefType prefType, List<T> list)
    {
        var str = JsonConvert.SerializeObject(list);
        LocalPrefs.SetString(prefType.ToString(), str);
        UserPrefs.Save();
    }

    public static T GetJson<T>(PrefType prefType) where T : class, new()
    {
        var str = LocalPrefs.GetString(prefType.ToString(), "");
        if (string.IsNullOrEmpty(str) || str.Equals(""))
        {
            return new T();
        }

        var obj = JsonConvert.DeserializeObject<T>(str);
        return obj;
    }

    public static List<T> GetJsonList<T>(PrefType prefType)
    {
        var str = LocalPrefs.GetString(prefType.ToString(), "");
        if (string.IsNullOrEmpty(str) || str.Equals(""))
        {
            return new List<T>();
        }

        var obj = JsonConvert.DeserializeObject<List<T>>(str);
        return obj;
    }

    public static void SaveEnum<T>(PrefType prefType, T _enum) where T : struct
    {
        var str = _enum.ToString();
        LocalPrefs.SetString(prefType.ToString(), str);
        UserPrefs.Save();
    }

    public static T GetEnum<T>(PrefType prefType, T defaultValue) where T : struct
    {
        var str = LocalPrefs.GetString(prefType.ToString(), "").Trim();
        var result = Enumeration<T>.GetEnum(str);
        if (result.HasValue)
            return result.Value;
        return defaultValue;
    }

    public static List<T> ChangeTypeList<T>(object val)
    {
        return (List<T>)Convert.ChangeType(val, typeof(List<T>));
    }

    public static T ChangeType<T>(object val)
    {
        return (T)Convert.ChangeType(val, typeof(T));
    }
}