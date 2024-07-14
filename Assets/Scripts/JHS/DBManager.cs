using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using System;

public class DBManager : MonoBehaviour
{
    public string dbUrl = "https://idlegame-8259c-default-rtdb.firebaseio.com/";
    DatabaseReference reference;

    void Start()
    {
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new Uri(dbUrl);
    }

    void Update()
    {
        
    }
}