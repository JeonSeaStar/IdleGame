using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase;
using Firebase.Extensions;
using Firebase.Auth;
using Google;

public class FirebaseGoogleLogin : MonoBehaviour
{
    public string googleWebAPI = "357130692486-c6rsslcpof5ubrfsjqesgtvib5nl60dr.apps.googleusercontent.com";

    private GoogleSignInConfiguration configuration;

    Firebase.DependencyStatus dependencyStatus = Firebase.DependencyStatus.UnavailableOther;
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;

    public TextMeshProUGUI usernameText, userEmailText;
    public Image userProfileImage;
    public string imageUrl;
    public GameObject LoginScreen, ProfileScreen;
    public TextMeshProUGUI errorDebug;
    public TextMeshProUGUI boolText;

    private void Awake()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                errorDebug.text = "Firebase Initialize Failed";
                Debug.LogError("Firebase Initialize Failed");
                return;
            }

            errorDebug.text = "Firebase Initialize Complete";
            FirebaseApp app = FirebaseApp.DefaultInstance;
            auth = FirebaseAuth.DefaultInstance;

            configuration = new GoogleSignInConfiguration { WebClientId = googleWebAPI, RequestEmail = true, RequestIdToken = true };
        });

        //configuration = new GoogleSignInConfiguration
        //{
        //    WebClientId = googleWebAPI,
        //    RequestIdToken = true
        //};
    }

    private void Start()
    {
        //InitFirebase();
    }

    private void InitFirebase()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }

    public void GoogleSignInClick()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        GoogleSignIn.Configuration.RequestEmail = true;

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnGoogleAuthenticatedFinished);
    }

    private void OnGoogleAuthenticatedFinished(Task<GoogleSignInUser> task)
    {
        boolText.text = task.IsFaulted.ToString();
        if (task.IsFaulted)
        {
            errorDebug.text = "Fault1";
            Debug.LogError("Fault1");
        }
        else if(task.IsCanceled)
        {
            errorDebug.text = "Login Cancel";
            Debug.LogError("Login Cancel");
        }
        else
        {
            Firebase.Auth.Credential credential = Firebase.Auth.GoogleAuthProvider.GetCredential(task.Result.IdToken, null);

            auth.SignInWithCredentialAsync(credential).ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    errorDebug.text = "SignInWithCredentialAsync was canceled.";
                    Debug.LogError("SignInWithCredentialAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    errorDebug.text = "SignInWithCredentialAsync encountered an error: " + task.Exception;
                    Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
                    return;
                }
                user = auth.CurrentUser;

                usernameText.text = user.DisplayName;
                userEmailText.text = user.Email;

                LoginScreen.SetActive(false);
                ProfileScreen.SetActive(true);

                StartCoroutine(LoadImage(CheckImageUrl(user.PhotoUrl.ToString())));
            });
        }
    }

    private string CheckImageUrl(string url)
    {
        if (!string.IsNullOrEmpty(url))
            return url;

        return imageUrl;
    }

    private IEnumerator LoadImage(string imageUri)
    {
        WWW www = new WWW(imageUri);
        yield return www;

        userProfileImage.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
    }
}