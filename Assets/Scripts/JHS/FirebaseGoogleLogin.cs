using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
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

    private void Awake()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("Firebase Initialize Failed");
                return;
            }

            FirebaseApp app = FirebaseApp.DefaultInstance;
            auth = FirebaseAuth.DefaultInstance;

            configuration = new GoogleSignInConfiguration { WebClientId = googleWebAPI, RequestEmail = true, RequestIdToken = true };
        });
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
        if (task.IsFaulted)
        {
            Debug.LogError("Fault1");
        }
        else if(task.IsCanceled)
        {
            Debug.LogError("Login Cancel");
        }
        else
        {
            Firebase.Auth.Credential credential = Firebase.Auth.GoogleAuthProvider.GetCredential(task.Result.IdToken, null);

            auth.SignInWithCredentialAsync(credential).ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("SignInWithCredentialAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
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
        UnityWebRequest www = new UnityWebRequest(imageUri);
        yield return www;

        Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        userProfileImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
    }
}