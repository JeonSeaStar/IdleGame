using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Google;
using TMPro;

public class FirebaseManager : MonoBehaviour
{
    private FirebaseAuth auth;

    private readonly string googleWebAPI = "357130692486-c6rsslcpof5ubrfsjqesgtvib5nl60dr.apps.googleusercontent.com";
    private GoogleSignInConfiguration configuration;
    private bool isSignin = false;

    public TextMeshProUGUI noticeText;
    public TextMeshProUGUI loginText;
    public Button signinBtn;

    private void Awake()
    {
        // Firebase 초기화
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                noticeText.text = "Firebase Initialize Failed";
                return;
            }

            noticeText.text = "Firebase Initialize Complete";
            FirebaseApp app = FirebaseApp.DefaultInstance;
            auth = FirebaseAuth.DefaultInstance;

            // 구글 SDK를 활용한 로그인 기능 등록
            configuration = new GoogleSignInConfiguration { WebClientId = googleWebAPI, RequestEmail = true, RequestIdToken = true };
        });
    }

    public void OnSignIn()
    {
        // 로그인, 로그아웃 구분
        if (!isSignin)
        {
            GoogleSignIn.Configuration = configuration;
            GoogleSignIn.Configuration.UseGameSignIn = false;
            GoogleSignIn.Configuration.RequestIdToken = true;
            GoogleSignIn.DefaultInstance.SignIn().ContinueWith(SignInWithGoogle);
        }
        else
        {
            GoogleSignIn.DefaultInstance.SignOut();
            loginText.text = "GOOGLE LOGIN";
        }

        isSignin = !isSignin;
    }

    private void SignInWithGoogle(Task<GoogleSignInUser> task)
    {
        // 구글 로그인
        Credential credential = GoogleAuthProvider.GetCredential(task.Result.IdToken, null);
        auth.SignInWithCredentialAsync(credential).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                noticeText.text = "Google Sign-In Failed";
                return;
            }

            noticeText.text = "Google Sign-In Successful!";
            loginText.text = "LOGOUT";
        });
    }
}