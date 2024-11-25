using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using System;

public class DBManager : MonoBehaviour
{
    public string dbUrl = "https://idlegame-8259c-default-rtdb.firebaseio.com/";

    void Start()
    {
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new Uri(dbUrl);
        WriteDB();
        ReadDB();
    }

    public void WriteDB()
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        //GPSdata data1 = new GPSdata("Lim", 27, 174.4f, 51.3f);
        //GPSdata data2 = new GPSdata("Jae", 28, 161.8f, 45.3f);
        //GPSdata data3 = new GPSdata("Young", 29, 182.1f, 73.6f);
        //string jsondata1 = JsonUtility.ToJson(data1);
        //string jsondata2 = JsonUtility.ToJson(data2);
        //string jsondata3 = JsonUtility.ToJson(data3);

        //reference.Child("User").Child("Man1").SetRawJsonValueAsync(jsondata1);
        //reference.Child("User").Child("Man2").SetRawJsonValueAsync(jsondata2);
        //reference.Child("User").Child("Man3").SetRawJsonValueAsync(jsondata3);
    }

    public void ReadDB()
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("User");
        reference.GetValueAsync().ContinueWith(task =>
        {
            if(task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                foreach(DataSnapshot data in snapshot.Children)
                {
                    IDictionary GPSdata = (IDictionary)data.Value;
                    //Debug.Log("이름: " + GPSdata["name"] + ", 나이" + GPSdata["age"] + ", 키" + GPSdata["height"] + ", 몸무게" + GPSdata["weight"]);
                }
            }
        });
    }
}

public class UserData
{
    public string email = "";
    public int userID;
    public string nickname;
    public int level;
    public int exp;
    
    public UserData()
    {

    }
}