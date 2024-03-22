using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System;
using UnityEngine.Networking;
using System.Collections;

public class TimeScripts : MonoBehaviour
{
    public static TimeScripts Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    struct TimeData
    {
        public string datetime;
    }

    const string API_URL = "http://worldtimeapi.org/api/ip";

    [HideInInspector] public bool IsTimeLodaed = false;

    private DateTime _currentDateTime = DateTime.Now;
    private DateTime _lastDayCheck = DateTime.Now;

    public static Action OnDayChanged;
    void Start()
    {
        StartCoroutine(GetRealDateTimeFromAPI());
    }

    public DateTime GetCurrentDateTime()
    {
        return _currentDateTime.AddSeconds(Time.realtimeSinceStartup);
    }

    IEnumerator GetRealDateTimeFromAPI()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(API_URL);

        yield return webRequest.SendWebRequest();

        if (webRequest.isNetworkError || webRequest.isHttpError)
        {
            Debug.Log("Error: " + webRequest.error);
        }
        else
        {
            TimeData timeData = JsonUtility.FromJson<TimeData>(webRequest.downloadHandler.text);

            _currentDateTime = ParseDateTime(timeData.datetime);
            IsTimeLodaed = true;

            Debug.Log("Success.");
        }
    }
    DateTime ParseDateTime(string datetime)
    {
        string date = Regex.Match(datetime, @"^\d{4}-\d{2}-\d{2}").Value;
        string time = Regex.Match(datetime, @"\d{2}:\d{2}:\d{2}").Value;

        return DateTime.Parse(string.Format("{0} {1}", date, time));
    }
    public bool HasDayChanged()
    {
        DateTime now = GetCurrentDateTime();

        if (now.Date != _lastDayCheck.Date)
        {
            _lastDayCheck = now;
            return true;
        }

        return false;
    }
}
