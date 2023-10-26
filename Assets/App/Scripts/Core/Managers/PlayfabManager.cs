using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using CodeForAll.APP.Core;

namespace CodeForAll.APP.Managers
{
    public class PlayfabManager : Singleton<PlayfabManager>
    {
        public Action<string> onError;
        public Action<string> onPasswordReset;
        public Action<LoginResult> onLoginSuccess;
        public Action<Dictionary<string,UserDataRecord>> onLoadData;
        public Action<string> onRegisterSuccess;

        //[SerializeField] public PlayerData playerData;


        private string email;
        private string pass;


        private void Start()
        {           
           
        }
        

        public void LogInAnonymousAccount() { 
         var request = new LoginWithCustomIDRequest{
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
            };

            PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
        }

       
        public void Register(string email,string userName,string password)
        {
            var request = new RegisterPlayFabUserRequest
            {

                Email = email,
                Username = userName,
                DisplayName = userName,
                Password = password                

            };
            this.email = email;
            this.pass = password;

            PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
        }
        public void Login(string email, string password)
        {
            if (email.Contains("@"))
            {
                var request = new LoginWithEmailAddressRequest
                {
                    Email = email,
                    Password = password,
                    InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
                    {
                        GetPlayerProfile = true
                    }

                };

                this.email = email;
                this.pass = password;

                PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
            }
        }

        public void ResetPassword(string email) {
            var request = new SendAccountRecoveryEmailRequest
            {
                Email = email,
                TitleId = "C2642"

            };

            PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnError);
        }
        
        public void SaveUserData() {
            //TODO Guarda todos los datos aqui porfa
            //var request = new UpdateUserDataRequest
            //{
            //    Data = new Dictionary<string, string> {
            //        { "CharacterStats" ,JsonUtility.ToJson(playerData) }
            //    }
            //};

            //PlayFabClientAPI.UpdateUserData(request,OnDataSend,OnError);
        }

        private void OnDataSend(UpdateUserDataResult obj)
        {
            Debug.Log("Data sended");
        }

        public void GetUserData() {
            PlayFabClientAPI.GetUserData(new GetUserDataRequest(),OnPlayerDataReceived,OnError);
        }

        private void OnPlayerDataReceived(GetUserDataResult result)
        {
            Debug.Log(result.Data.Count);
            onLoadData?.Invoke(result.Data);
        }

        private void OnPasswordReset(SendAccountRecoveryEmailResult result)
        {
            //TODO para recovery del password
            onPasswordReset?.Invoke("Password reset mail send");
        }

        private void OnLoginSuccess(LoginResult result)
        {
            Debug.Log("Loggueando");
            //TODO cuando se loguee bien has algo que aun nose
            PlayerPrefs.SetString("UserMail", email);
            PlayerPrefs.SetString("UserPassword", pass);               
            onLoginSuccess?.Invoke(result);
            GetUserData();


        }

        private void OnRegisterSuccess(RegisterPlayFabUserResult result)
        {
            Login(email, pass);
            onRegisterSuccess?.Invoke("Registered  Success");
        }

        private void OnError(PlayFabError error)
        {
            onError?.Invoke(error.GenerateErrorReport());
            Debug.Log(error.ErrorMessage);
        }

        private void OnSuccess(LoginResult result)
        {
            Debug.Log("Successful login/account create!");

        }


        //TODO aun falta : https://www.youtube.com/watch?v=QS_sl7jNyVc&t=0s&ab_channel=CocoCode
    }
}
