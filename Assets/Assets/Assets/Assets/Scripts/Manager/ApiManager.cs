using Cashbaazi.App.Common;
using Cashbaazi.App.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.AudioSettings;

namespace Cashbaazi.App.Helper
{
    public class ApiManager : Singleton<ApiManager>
    {
        static string app_version = "1.6";
        static string APP_KEY = "FDC9829C-F534-426F-9732-C56DACDFDE36";

        static string BASE_URL = "https://api.morcoin.in/Services/CustomerService.asmx/";
       // static string BASE_URL = "https://testing.morcoin.in/Services/CustomerService.asmx/ ";
        static string URL_LoginSignup = BASE_URL + "loginsignup";
        static string URL_OtpLogin = BASE_URL + "otplogin";
        static string URL_UpdateProfile = BASE_URL + "updateprofile";
        static string URL_GetUserDetails = BASE_URL + "getuserdetails";
        static string URL_MinusWallet = BASE_URL + "minouswallate";
        static string URL_AddWallet = BASE_URL + "addwallate";
        static string URL_ApplyReferalCode = BASE_URL + "ApplyReferalCode";
        static string URL_SaveTempPayment = "https://api.morcoin.in/Services/WalletService.asmx/SaveTempPayment";
        static string URL_OfferList = "https://api.morcoin.in/Services/OfferService.asmx/" + "OfferList";
        static string URL_UsedOfferList = "https://api.morcoin.in/Services/OfferService.asmx/" + "UserOfferList";
        static string URL_ApplyPromocode = "https://api.morcoin.in/OfferService.asmx/" + "ApplyPromocode";
        static string URL_TransactionHistory = "https://api.morcoin.in/Services/WalletService.asmx/" + "TransactionHistory";
        static string URL_PayOutRequest = "https://api.morcoin.in/Services/PaymentService.asmx/PayoutReqest";
        static string URL_UpdatePaytmNNo = "https://api.morcoin.in/Services/CustomerService.asmx/UpdatePaytmNo";
        static string URL_AppUpdate = "https://api.morcoin.in/Services/AppUpdate.asmx/CheckAppUpdate";
        static string URL_AmazonPayNo = "https://api.morcoin.in/Services/CustomerService.asmx/UpdateAmazonPayNo";
        static string URL_UpdateBankDetail = "https://api.morcoin.in/Services/CustomerService.asmx/UpdateBankDetail";
        static string URL_UpdateUpiDetails = "https://api.morcoin.in/Services/CustomerService.asmx/UpdateUPI";

        [Header("Fetched Data")]
        public Response_LoginSignup responce_loginsignup;
        public Responce_Userdata responce_userdata;
        public List<Responce_Offer> responce_offers;
        public List<Responce_Offer> responce_usedoffers;
        public Responce_SaveTempPayment tempPaymentData;
        public List<Responce_Transaction> responce_transactionHistory;
        public Responce_ApplyPromo responce_applypromo;
        public Responce_Payout responce_payoutRequest;
        public Response_App_Update response_App_Update;

        public string SessionToken
        {
            get { return PlayerPrefs.GetString("pToken", string.Empty); }
            set { PlayerPrefs.SetString("pToken", value); }
        }

        private void Start()
        {
            responce_loginsignup = new Response_LoginSignup();
            responce_userdata = new Responce_Userdata();
        }

        public void API_LoginSignup(string mobile, Action success = null, Action failure = null)
        {
            StartCoroutine(Call_LoginSignup(mobile, success, failure));
        }
        public void API_OtpLogin(string otp, string mobile, Action success = null, Action failure = null)
        {
            StartCoroutine(Call_OtpLogin(otp, mobile, success, failure));
        }
        public void API_UpdateProfile(string name, string email, int avtar, string gender,  Action success = null, Action failure = null)
        {
            StartCoroutine(Call_UpdateProfile(name, email, avtar, gender, success, failure));
        }
        public void API_GetUserDetails(Action success = null, Action failure = null)
        {
            StartCoroutine(Call_GetUserDetails(success, failure));
        }
        public void API_MinusWallet(string message, int amount, int amountType, Action success = null, Action failure = null)
        {
            StartCoroutine(Call_MinusWallet(message, amount, amountType, success, failure));
        }
        public void API_AddWallet(string message, float amount, Action success = null, Action failure = null)
        {
            StartCoroutine(Call_AddWallet(message, amount, success, failure));
        }
        public void API_ApplyRefralCode(string refralCode, Action success = null, Action failure = null)
        {
            StartCoroutine(Call_ApplyRefralCode(refralCode, success, failure));
        }
        public void API_SaveTempPayment(string amount, bool ispromo, Action success = null, Action failure = null)
        {
            string promoid = "";
           if (ispromo)
                promoid = responce_applypromo.CustomerOfferId.ToString();
            StartCoroutine(Call_SaveTempPayment(amount,promoid, success, failure));
        }
        public void API_GetOfferList(Action success = null, Action failure = null)
        {
            StartCoroutine(Call_GetOfferList(success, failure));
        }
        public void API_GetUsedOfferList(Action success = null, Action failure = null)
        {
            StartCoroutine(Call_GetUsedOfferList(success, failure));
        }
        public void API_AppyPromocode(string amount, string promocode, Action success = null, Action failure = null)
        {
            StartCoroutine(Call_ApplyPromocode(amount, promocode, success, failure));
        }
        public void API_GetTransactionHistory(Action success = null, Action failure = null)
        {
            StartCoroutine(Call_GetTransactionHistory(success, failure));
        }
        public void API_PayoutRequest(string amount, byte payoutModeKey, string Upi, Action success = null, Action failure = null)
        {
            StartCoroutine(Call_PayoutRequest(amount, payoutModeKey, Upi, success, failure));
        }

        public void API_PayoutRequestPaytm(string amount, Action success = null, Action failure = null)
        {
            StartCoroutine(Call_PayoutRequestPaytm(amount, success, failure));
        }

        public void Api_AppUpdate(Action success = null, Action failure = null)
        {
       //     StartCoroutine(Call_AppUpdate( success, failure));
        }

        public void API_PayoutRequestAmazonPay(string amount, byte payoutModeKey, Action success = null , Action failure = null)
        {
            StartCoroutine(Call_PayoutRequestAmazonPay(amount,payoutModeKey,success,failure));
        }
        public void API_UpdateAmazonPayNum(string amazonPayNum,  Action success = null, Action failure = null)
        {
            StartCoroutine(Call_UpdateAmazonPayNum(amazonPayNum, success, failure));
        }

        public void API_UpdateBankDetails(string name, string accountNum, string bankName, string bankBranchName,string ifscCode, Action success = null, Action failure = null)
        {
            StartCoroutine(Call_UpdateBankDetails(name, accountNum, bankName,bankBranchName,ifscCode, success, failure));
        }

        public void API_UpdateUpiDetails(string upi, Action success = null, Action failure = null)
        {
            StartCoroutine(Call_UpdateUpiDetails(upi,success,failure));
        }

        IEnumerator Call_LoginSignup(string mobile, Action success = null, Action failure = null)
        {
            Loading.instance.ShowLoading();

            WWWForm formdata = new WWWForm();
            formdata.AddField("mobile", mobile);

            using (UnityWebRequest request = UnityWebRequest.Post(URL_LoginSignup, formdata))
            {
                request.SetRequestHeader("AppKey", APP_KEY);
                yield return request.SendWebRequest();
                while (!request.isDone)
                    yield return null;

                Debug.Log(request.downloadHandler.text);

                Loading.instance.HideLoading();

                switch (request.result)
                {
                    case UnityWebRequest.Result.InProgress:
                        break;
                    case UnityWebRequest.Result.Success:
                        responce_loginsignup = JsonUtility.FromJson<Response_LoginSignup>(request.downloadHandler.text);
                        if (responce_loginsignup.msg == "success")
                        {
                            if (success != null)
                                success();
                        }
                        else
                        {
                            if (failure != null)
                                failure();
                        }
                        break;
                    case UnityWebRequest.Result.ConnectionError:
                        if (failure != null)
                            failure();
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        if (failure != null)
                            failure();
                        break;
                    case UnityWebRequest.Result.DataProcessingError:
                        if (failure != null)
                            failure();
                        break;
                    default:
                        break;
                }
            }
        }
        IEnumerator Call_OtpLogin(string otp, string mobile, Action success = null, Action failure = null)
        {
            Loading.instance.ShowLoading();

            WWWForm formdata = new WWWForm();
            formdata.AddField("otp", otp);
            formdata.AddField("mobile", mobile);

            using (UnityWebRequest request = UnityWebRequest.Post(URL_OtpLogin, formdata))
            {
                request.SetRequestHeader("AppKey", APP_KEY);
                yield return request.SendWebRequest();
                while (!request.isDone)
                    yield return null;

                Loading.instance.HideLoading();

                switch (request.result)
                {
                    case UnityWebRequest.Result.InProgress:
                        break;
                    case UnityWebRequest.Result.Success:
                        Responce_OtpLogin otpLoginData = JsonUtility.FromJson<Responce_OtpLogin>(request.downloadHandler.text);
                        if (otpLoginData.message == "success")
                        {
                            SessionToken = otpLoginData.data.userdata.token;
                            responce_userdata = otpLoginData.data.userdata;
                            if (success != null)
                                success();
                        }
                        else
                        {
                            if (failure != null)
                                failure();
                        }
                        break;
                    case UnityWebRequest.Result.ConnectionError:
                        if (failure != null)
                            failure();
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        if (failure != null)
                            failure();
                        break;
                    case UnityWebRequest.Result.DataProcessingError:
                        if (failure != null)
                            failure();
                        break;
                    default:
                        break;
                }
            }
        }
        IEnumerator Call_UpdateProfile(string name, string email, int avtar, string gender, Action success = null, Action failure = null)
        {
            Loading.instance.ShowLoading();

            WWWForm formdata = new WWWForm();
            formdata.AddField("name", name);
            formdata.AddField("email", email);
            formdata.AddField("mobile", responce_userdata.mobile);
            formdata.AddField("avtar", avtar);
            formdata.AddField("gender", gender);
            
            using (UnityWebRequest request = UnityWebRequest.Post(URL_UpdateProfile, formdata))
            {
                request.SetRequestHeader("token", SessionToken);

                yield return request.SendWebRequest();
                while (!request.isDone)
                    yield return null;

                Loading.instance.HideLoading();

                switch (request.result)
                {
                    case UnityWebRequest.Result.InProgress:
                        break;
                    case UnityWebRequest.Result.Success:
                        Responce_UpdateProfile updateprofileData = JsonUtility.FromJson<Responce_UpdateProfile>(request.downloadHandler.text);
                        if (updateprofileData.message == "success")
                        {
                            responce_userdata = updateprofileData.data.userdata;
                            if (success != null)
                                success();
                        }
                        else
                        {
                            if (failure != null)
                                failure();
                        }
                        break;
                    case UnityWebRequest.Result.ConnectionError:
                        if (failure != null)
                            failure();
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        if (failure != null)
                            failure();
                        break;
                    case UnityWebRequest.Result.DataProcessingError:
                        if (failure != null)
                            failure();
                        break;
                    default:
                        break;
                }
            }
        }
        IEnumerator Call_GetUserDetails(Action success = null, Action failure = null)
        {
            Loading.instance.ShowLoading();

            using (UnityWebRequest request = UnityWebRequest.PostWwwForm(URL_GetUserDetails, string.Empty))
            {
                request.SetRequestHeader("token", SessionToken);

                yield return request.SendWebRequest();
                while (!request.isDone)
                    yield return null;

                Loading.instance.HideLoading();

                switch (request.result)
                {
                    case UnityWebRequest.Result.InProgress:
                        break;
                    case UnityWebRequest.Result.Success:
                        Responce_GetUserDetails profileData = JsonUtility.FromJson<Responce_GetUserDetails>(request.downloadHandler.text);
                        if (profileData.message == "success")
                        {
                            responce_userdata = profileData.data.userdata;
                            if (success != null)
                                success();
                        }
                        else
                        {
                            if (failure != null)
                                failure();
                        }
                        break;
                    case UnityWebRequest.Result.ConnectionError:
                        if (failure != null)
                            failure();
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        if (failure != null)
                            failure();
                        break;
                    case UnityWebRequest.Result.DataProcessingError:
                        if (failure != null)
                            failure();
                        break;
                    default:
                        break;
                }
            }
        }
        IEnumerator Call_MinusWallet(string message, int amount, int amountType, Action success = null, Action failure = null)
        {
            Loading.instance.ShowLoading();

            WWWForm formdata = new WWWForm();
            formdata.AddField("message", message);
            formdata.AddField("Amount", amount);
            formdata.AddField("AmountType", amountType);

            using (UnityWebRequest request = UnityWebRequest.Post(URL_MinusWallet, formdata))
            {
                request.SetRequestHeader("AppKey", APP_KEY);
                request.SetRequestHeader("token", SessionToken);

                yield return request.SendWebRequest();
                while (!request.isDone)
                    yield return null;

                Loading.instance.HideLoading();

                switch (request.result)
                {
                    case UnityWebRequest.Result.InProgress:
                        break;
                    case UnityWebRequest.Result.Success:
                        Responce_AddMinusWallet walletUpdateData = JsonUtility.FromJson<Responce_AddMinusWallet>(request.downloadHandler.text);
                        if (walletUpdateData.message == "success")
                        {
                            responce_userdata.wallet = responce_userdata.wallet - amount;
                            if (success != null)
                                success();
                        }
                        else
                        {
                            if (failure != null)
                                failure();
                        }
                        break;
                    case UnityWebRequest.Result.ConnectionError:
                        if (failure != null)
                            failure();
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        if (failure != null)
                            failure();
                        break;
                    case UnityWebRequest.Result.DataProcessingError:
                        if (failure != null)
                            failure();
                        break;
                    default:
                        break;
                }
            }
        }
        IEnumerator Call_AddWallet(string message, float amount, Action success = null, Action failure = null)
        {
            Loading.instance.ShowLoading();

            WWWForm formdata = new WWWForm();
            formdata.AddField("message", message);
            formdata.AddField("Amount", amount.ToString());
            formdata.AddField("AmountType", 3);

            using (UnityWebRequest request = UnityWebRequest.Post(URL_AddWallet, formdata))
            {
                request.SetRequestHeader("AppKey", APP_KEY);
                request.SetRequestHeader("token", SessionToken);

                yield return request.SendWebRequest();
                while (!request.isDone)
                    yield return null;

                Loading.instance.HideLoading();

                switch (request.result)
                {
                    case UnityWebRequest.Result.InProgress:
                        break;
                    case UnityWebRequest.Result.Success:
                        Responce_AddMinusWallet walletUpdateData = JsonUtility.FromJson<Responce_AddMinusWallet>(request.downloadHandler.text);
                        if (walletUpdateData.message == "success")
                        {
                            responce_userdata.wallet = responce_userdata.wallet + amount;
                            if (success != null)
                                success();
                        }
                        else
                        {
                            if (failure != null)
                                failure();
                        }
                        break;
                    case UnityWebRequest.Result.ConnectionError:
                        if (failure != null)
                            failure();
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        if (failure != null)
                            failure();
                        break;
                    case UnityWebRequest.Result.DataProcessingError:
                        if (failure != null)
                            failure();
                        break;
                    default:
                        break;
                }
            }
        }
        IEnumerator Call_ApplyRefralCode(string refralCode, Action success = null, Action failure = null)
        {
            Loading.instance.ShowLoading();

            WWWForm formdata = new WWWForm();
            formdata.AddField("ReferalCode", refralCode);

            using (UnityWebRequest request = UnityWebRequest.Post(URL_ApplyReferalCode, formdata))
            {
                request.SetRequestHeader("token", SessionToken);
                request.SetRequestHeader("AppKey", APP_KEY);
                yield return request.SendWebRequest();
                while (!request.isDone)
                    yield return null;

                Loading.instance.HideLoading();

                switch (request.result)
                {
                    case UnityWebRequest.Result.InProgress:
                        break;
                    case UnityWebRequest.Result.Success:
                        Responce_Referral refralData = JsonUtility.FromJson<Responce_Referral>(request.downloadHandler.text);
                        if (refralData.Message == "success")
                        {
                            Toast.ShowToast(String.Format("Your referral from {0} applied successfully", refralData.ReferByUser));
                            if (success != null)
                                success();
                        }
                        else
                        {
                            Toast.ShowToast(refralData.Message);
                            if (failure != null)
                                failure();
                        }
                        break;
                    case UnityWebRequest.Result.ConnectionError:
                        if (failure != null)
                            failure();
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        if (failure != null)
                            failure();
                        break;
                    case UnityWebRequest.Result.DataProcessingError:
                        if (failure != null)
                            failure();
                        break;
                    default:
                        break;
                }
            }
        }
        IEnumerator Call_SaveTempPayment(string amount, string promoid, Action success = null, Action failure = null)
        {
            Loading.instance.ShowLoading();

            WWWForm formdata = new WWWForm();
            formdata.AddField("Amount", amount);
            formdata.AddField("Remarks", "Adding from Game");
            

            if (!string.IsNullOrEmpty(promoid))
                formdata.AddField("CustomerOfferId", promoid);

            using (UnityWebRequest request = UnityWebRequest.Post(URL_SaveTempPayment, formdata))
            {
                request.SetRequestHeader("token", SessionToken);

                yield return request.SendWebRequest();
                while (!request.isDone)
                    yield return null;

                Loading.instance.HideLoading();

                switch (request.result)
                {
                    case UnityWebRequest.Result.InProgress:
                        break;
                    case UnityWebRequest.Result.Success:
                        tempPaymentData = JsonUtility.FromJson<Responce_SaveTempPayment>(request.downloadHandler.text);
                        if (tempPaymentData.Message == "success")
                        {
                            responce_applypromo = null;
                            if (success != null)
                                success();
                            
                        }
                        else
                        {
                            Toast.ShowToast(tempPaymentData.Message);
                            if (failure != null)
                                failure();
                        }
                        break;
                    case UnityWebRequest.Result.ConnectionError:
                        if (failure != null)
                            failure();
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        if (failure != null)
                            failure();
                        break;
                    case UnityWebRequest.Result.DataProcessingError:
                        if (failure != null)
                            failure();
                        break;
                    default:
                        break;
                }
            }
        }
        IEnumerator Call_GetOfferList(Action success = null, Action failure = null)
        {
            Loading.instance.ShowLoading();

            using (UnityWebRequest request = UnityWebRequest.PostWwwForm(URL_OfferList, string.Empty))
            {
                request.SetRequestHeader("AppKey", APP_KEY);
                request.SetRequestHeader("TokenNo", SessionToken);

                yield return request.SendWebRequest();
                while (!request.isDone)
                    yield return null;

                Loading.instance.HideLoading();

                switch (request.result)
                {
                    case UnityWebRequest.Result.InProgress:
                        break;
                    case UnityWebRequest.Result.Success:
                        Responce_OfferList offerData = JsonUtility.FromJson<Responce_OfferList>(request.downloadHandler.text);
                        if (offerData.Message == "success")
                        {
                            responce_offers = offerData.OfferList;
                            if (success != null)
                                success();
                        }
                        else
                        {
                            if (failure != null)
                                failure();
                        }
                        break;
                    case UnityWebRequest.Result.ConnectionError:
                        if (failure != null)
                            failure();
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        if (failure != null)
                            failure();
                        break;
                    case UnityWebRequest.Result.DataProcessingError:
                        if (failure != null)
                            failure();
                        break;
                    default:
                        break;
                }
            }
        }
        IEnumerator Call_GetUsedOfferList(Action success = null, Action failure = null)
        {
            Loading.instance.ShowLoading();

            using (UnityWebRequest request = UnityWebRequest.PostWwwForm(URL_UsedOfferList, string.Empty))
            {
                request.SetRequestHeader("AppKey", APP_KEY);
                request.SetRequestHeader("TokenNo", SessionToken);

                yield return request.SendWebRequest();
                while (!request.isDone)
                    yield return null;

                Loading.instance.HideLoading();

                switch (request.result)
                {
                    case UnityWebRequest.Result.InProgress:
                        break;
                    case UnityWebRequest.Result.Success:
                        Responce_OfferList offerData = JsonUtility.FromJson<Responce_OfferList>(request.downloadHandler.text);
                        if (offerData.Message == "success")
                        {
                            responce_usedoffers = offerData.OfferList;
                            if (success != null)
                                success();
                        }
                        else
                        {
                            if (failure != null)
                                failure();
                        }
                        break;
                    case UnityWebRequest.Result.ConnectionError:
                        if (failure != null)
                            failure();
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        if (failure != null)
                            failure();
                        break;
                    case UnityWebRequest.Result.DataProcessingError:
                        if (failure != null)
                            failure();
                        break;
                    default:
                        break;
                }
            }
        }
        IEnumerator Call_ApplyPromocode(string amount, string promocode, Action success = null, Action failure = null)
        {
            Loading.instance.ShowLoading();

            WWWForm formdata = new WWWForm();
            formdata.AddField("Amount", amount);
            formdata.AddField("Promocode", promocode);

            using (UnityWebRequest request = UnityWebRequest.Post(URL_ApplyPromocode, formdata))
            {
                request.SetRequestHeader("AppKey", APP_KEY);
                request.SetRequestHeader("TokenNo", SessionToken);

                yield return request.SendWebRequest();
                while (!request.isDone)
                    yield return null;

                Loading.instance.HideLoading();

                switch (request.result)
                {
                    case UnityWebRequest.Result.InProgress:
                        break;
                    case UnityWebRequest.Result.Success:
                        Responce_ApplyPromo offerData = JsonUtility.FromJson<Responce_ApplyPromo>(request.downloadHandler.text);
                        if (offerData.Message == "success")
                        {
                            responce_applypromo = offerData;
                            if (success != null)
                                success();
                           
                        }
                        else
                        {
                            if (failure != null)
                                failure();
                        }
                        break;
                    case UnityWebRequest.Result.ConnectionError:
                        if (failure != null)
                            failure();
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        if (failure != null)
                            failure();
                        break;
                    case UnityWebRequest.Result.DataProcessingError:
                        if (failure != null)
                            failure();
                        break;
                    default:
                        break;
                }
            }
        }
        IEnumerator Call_GetTransactionHistory(Action success = null, Action failure = null)
        {
            Loading.instance.ShowLoading();

            using (UnityWebRequest request = UnityWebRequest.PostWwwForm(URL_TransactionHistory, string.Empty))
            {
                request.SetRequestHeader("AppKey", APP_KEY);
                request.SetRequestHeader("TokenNo", SessionToken);

                yield return request.SendWebRequest();
                while (!request.isDone)
                    yield return null;

                Loading.instance.HideLoading();

                switch (request.result)
                {
                    case UnityWebRequest.Result.InProgress:
                        break;
                    case UnityWebRequest.Result.Success:
                        Responce_TransactionHistory transactionData = JsonUtility.FromJson<Responce_TransactionHistory>(request.downloadHandler.text);
                        if (transactionData.Message == "success")
                        {
                            responce_transactionHistory = transactionData.TransactionHistory;
                            if (success != null)
                                success();
                        }
                        else
                        {
                            if (failure != null)
                                failure();
                        }
                        break;
                    case UnityWebRequest.Result.ConnectionError:
                        if (failure != null)
                            failure();
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        if (failure != null)
                            failure();
                        break;
                    case UnityWebRequest.Result.DataProcessingError:
                        if (failure != null)
                            failure();
                        break;
                    default:
                        break;
                }
            }
        }

        IEnumerator Call_PayoutRequest(string amount, byte payoutModeKey, string Upi, Action success = null, Action failure = null)
        {
            Loading.instance.ShowLoading();
            WWWForm formdata = new WWWForm();
            formdata.AddField("Amount", amount);
            formdata.AddField("Upi", Upi);
            formdata.AddField("payoutModeKey", payoutModeKey);

            using (UnityWebRequest request = UnityWebRequest.Post(URL_PayOutRequest, formdata))
            {
                request.SetRequestHeader("AppKey", APP_KEY);
                request.SetRequestHeader("TokenNo", SessionToken);

                yield return request.SendWebRequest();
                while (!request.isDone)
                    yield return null;

                try
                {
                    Loading.instance.HideLoading();

                    switch (request.result)
                    {
                        case UnityWebRequest.Result.InProgress:
                            break;
                        case UnityWebRequest.Result.Success:
                            Responce_Payout offerData = JsonUtility.FromJson<Responce_Payout>(request.downloadHandler.text);
                            if (offerData.Message == "success")
                            {
                                if (success != null)
                                    success();
                            }
                            else
                            {
                            Toast.ShowToast(offerData.Message);
                                if (failure != null)
                                    failure();
                            }
                            break;
                        case UnityWebRequest.Result.ConnectionError:
                            if (failure != null)
                                failure();
                            break;
                        case UnityWebRequest.Result.ProtocolError:
                            if (failure != null)
                                failure();
                            break;
                        case UnityWebRequest.Result.DataProcessingError:
                            if (failure != null)
                                failure();
                            break;
                        default:
                            break;
                    }

                }
                catch (Exception ex)
                {

                }
            }

        }

        IEnumerator Call_PayoutRequestPaytm(string amount, Action success = null, Action failure = null)
        {
            Loading.instance.ShowLoading();
            WWWForm formdata = new WWWForm();
            formdata.AddField("Amount", amount);
           

            using (UnityWebRequest request = UnityWebRequest.Post(URL_UpdatePaytmNNo, formdata))
            {
                request.SetRequestHeader("TokenNo", SessionToken);
                request.SetRequestHeader("TokenNo", SessionToken);

                yield return request.SendWebRequest();
                while (!request.isDone)
                    yield return null;

                try
                {
                    Loading.instance.HideLoading();

                    switch (request.result)
                    {
                        case UnityWebRequest.Result.InProgress:
                            break;
                        case UnityWebRequest.Result.Success:
                            Responce_GetUserDetails updateprofileData = JsonUtility.FromJson<Responce_GetUserDetails>(request.downloadHandler.text);
                            if (updateprofileData.message == "success")
                            {
                                if (success != null)
                                    success();
                            }
                            else
                            {
                                Toast.ShowToast(updateprofileData.message);
                                if (failure != null)
                                    failure();
                            }
                            break;
                        case UnityWebRequest.Result.ConnectionError:
                            if (failure != null)
                                failure();
                            break;
                        case UnityWebRequest.Result.ProtocolError:
                            if (failure != null)
                                failure();
                            break;
                        case UnityWebRequest.Result.DataProcessingError:
                            if (failure != null)
                                failure();
                            break;
                        default:
                            break;
                    }

                }
                catch (Exception ex)
                {

                }
            }

        }

       /* IEnumerator Call_AppUpdate(Action success = null, Action failure = null)
        {
            Loading.instance.ShowLoading();
            WWWForm formdata = new WWWForm();
            formdata.AddField("AppVersion", "1.7");

            using (UnityWebRequest request = UnityWebRequest.Post(URL_AppUpdate, formdata))
            {
                request.SetRequestHeader("AppKey", APP_KEY);
                yield return request.SendWebRequest();
                while (!request.isDone)
                    yield return null;

                try
                {
                    Loading.instance.HideLoading();

                    switch (request.result)
                    {
                        case UnityWebRequest.Result.InProgress:
                            break;
                        case UnityWebRequest.Result.Success:
                            response_App_Update = JsonUtility.FromJson<Response_App_Update>(request.downloadHandler.text);
                            if (response_App_Update.Message == "success")
                            {
                                if (success != null)
                                    success();
                            }
                            else
                            {
                               // Toast.ShowToast(response_App_Update.Message);
                                if (failure != null)
                                    failure();
                            }
                            break;
                        case UnityWebRequest.Result.ConnectionError:
                            if (failure != null)
                                failure();
                            break;
                        case UnityWebRequest.Result.ProtocolError:
                            if (failure != null)
                                failure();
                            break;
                        case UnityWebRequest.Result.DataProcessingError:
                            if (failure != null)
                                failure();
                            break;
                        default:
                            break;
                    }

                }
                catch (Exception ex)
                {

                }
            }

        }*/

        IEnumerator Call_PayoutRequestAmazonPay(string amount, byte payoutModeKey, Action success = null, Action failure = null)
        {
            Loading.instance.ShowLoading();
            WWWForm formdata = new WWWForm();
            formdata.AddField("Amount", amount);
            formdata.AddField("payoutModeKey", payoutModeKey);
           

            using (UnityWebRequest request = UnityWebRequest.Post(URL_PayOutRequest, formdata))
            {
                request.SetRequestHeader("AppKey", APP_KEY);
                request.SetRequestHeader("TokenNo", SessionToken);

                yield return request.SendWebRequest();
                while (!request.isDone)
                    yield return null;

                try
                {
                    Loading.instance.HideLoading();

                    switch (request.result)
                    {
                        case UnityWebRequest.Result.InProgress:
                            break;
                        case UnityWebRequest.Result.Success:
                            Responce_Payout offerData = JsonUtility.FromJson<Responce_Payout>(request.downloadHandler.text);
                            if (offerData.Message == "success")
                            {
                                if (success != null)
                                    success();
                            }
                            else
                            {
                                Toast.ShowToast(offerData.Message);
                                if (failure != null)
                                    failure();
                            }
                            break;
                        case UnityWebRequest.Result.ConnectionError:
                            if (failure != null)
                                failure();
                            break;
                        case UnityWebRequest.Result.ProtocolError:
                            if (failure != null)
                                failure();
                            break;
                        case UnityWebRequest.Result.DataProcessingError:
                            if (failure != null)
                                failure();
                            break;
                        default:
                            break;
                    }

                }
                catch (Exception ex)
                {

                }
            }

        }

        IEnumerator Call_UpdateAmazonPayNum(string amazonPayNum, Action success = null, Action failure = null)
        {
            Loading.instance.ShowLoading();

            WWWForm formdata = new WWWForm();
            formdata.AddField("AmazonPayNo", amazonPayNum);

            using (UnityWebRequest request = UnityWebRequest.Post(URL_AmazonPayNo, formdata))
            {
                request.SetRequestHeader("TokenNo", SessionToken);
                request.SetRequestHeader("AppKey", APP_KEY);
                yield return request.SendWebRequest();
                while (!request.isDone)
                    yield return null;

                Debug.Log(request.downloadHandler.text);

                Loading.instance.HideLoading();

                switch (request.result)
                {
                    case UnityWebRequest.Result.InProgress:
                        break;
                    case UnityWebRequest.Result.Success:
                        Responce_GetUserDetails updateprofileData = JsonUtility.FromJson<Responce_GetUserDetails>(request.downloadHandler.text);
                        if (updateprofileData.message == "success")
                        {
                            if (success != null)
                                success();
                        }
                        else
                        {
                            if (failure != null)
                                failure();
                        }
                        break;
                    case UnityWebRequest.Result.ConnectionError:
                        if (failure != null)
                            failure();
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        if (failure != null)
                            failure();
                        break;
                    case UnityWebRequest.Result.DataProcessingError:
                        if (failure != null)
                            failure();
                        break;
                    default:
                        break;
                }
            }
        }

        IEnumerator Call_UpdateBankDetails(string name,string accountNum,string bankName,string bankBranchName,string ifscCode, Action success = null, Action failure = null)
        {
            Loading.instance.ShowLoading();

            WWWForm formdata = new WWWForm();
            formdata.AddField("accountHolderName", name);
            formdata.AddField("bankAccountNo", accountNum);
            formdata.AddField("bankName", bankName);
            formdata.AddField("bankBranch", bankBranchName);
            formdata.AddField("ifsc", ifscCode);


            using (UnityWebRequest request = UnityWebRequest.Post(URL_UpdateBankDetail, formdata))
            {
                request.SetRequestHeader("AppKey", APP_KEY);
                request.SetRequestHeader("TokenNo", SessionToken);
                yield return request.SendWebRequest();
                while (!request.isDone)
                    yield return null;

                Debug.Log(request.downloadHandler.text);

                Loading.instance.HideLoading();

                switch (request.result)
                {
                    case UnityWebRequest.Result.InProgress:
                        break;
                    case UnityWebRequest.Result.Success:
                        Responce_GetUserDetails updateprofileData = JsonUtility.FromJson<Responce_GetUserDetails>(request.downloadHandler.text);
                        if (updateprofileData.message == "success")
                        {
                            if (success != null)
                                success();
                        }
                        else
                        {
                            if (failure != null)
                                failure();
                        }
                        break;
                    case UnityWebRequest.Result.ConnectionError:
                        if (failure != null)
                            failure();
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        if (failure != null)
                            failure();
                        break;
                    case UnityWebRequest.Result.DataProcessingError:
                        if (failure != null)
                            failure();
                        break;
                    default:
                        break;
                }
            }
        }

        IEnumerator Call_UpdateUpiDetails(string upi, Action success = null, Action failure = null)
        {
            Loading.instance.ShowLoading();

            WWWForm formdata = new WWWForm();
            formdata.AddField("upi", upi);

            using (UnityWebRequest request = UnityWebRequest.Post(URL_UpdateUpiDetails, formdata))
            {
                request.SetRequestHeader("TokenNo", SessionToken);
                request.SetRequestHeader("AppKey", APP_KEY);
                yield return request.SendWebRequest();
                while (!request.isDone)
                    yield return null;

                Debug.Log(request.downloadHandler.text);

                Loading.instance.HideLoading();

                switch (request.result)
                {
                    case UnityWebRequest.Result.InProgress:
                        break;
                    case UnityWebRequest.Result.Success:
                        Responce_GetUserDetails updateprofileData = JsonUtility.FromJson<Responce_GetUserDetails>(request.downloadHandler.text);
                        if (updateprofileData.message == "success")
                        {
                            if (success != null)
                                success();
                        }
                        else
                        {
                            if (failure != null)
                                failure();
                        }
                        break;
                    case UnityWebRequest.Result.ConnectionError:
                        if (failure != null)
                            failure();
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        if (failure != null)
                            failure();
                        break;
                    case UnityWebRequest.Result.DataProcessingError:
                        if (failure != null)
                            failure();
                        break;
                    default:
                        break;
                }
            }
        }
    }




    [System.Serializable]
    public class Response_LoginSignup
    {
        public int tempotp;
        public string phoneno;
        public string status;
        public string msg;
    }



    [System.Serializable]
    public class Responce_OtpLogin
    {
        public string status;
        public string message;
        public Responce_OtpLogin_Data data;
    }
    [System.Serializable]
    public class Responce_OtpLogin_Data
    {
        public string otp;
        public string mobile;
        public Responce_Userdata userdata;
    }
    [System.Serializable]
    public class Responce_Userdata
    {
      
        public int id;
        public string referel_code;
        public string verification_code;
        public int lang_id;
        public string name;
        public string email;
        public string mobile;
        public string paytmNo;
        public string amazonPayNo;
        public string upi;
        public string bankAccountNo;
        public string bankName;
        public string bankBranch;
        public string ifsc;
        public string accountHolderName;
        public int status;
        public string avtar;
        public string about;
        public string gender;
        public float wallet;
        public int BonusWallet;
        public int MainWallet;
        public int WinningWallet;
        public string dob;
        public string password;
        public string added_on;
        public bool is_active;
        public bool is_deleted;
        public string token;
        
    }



    [System.Serializable]
    public class Responce_UpdateProfile
    {
        public Responce_OtpLogin_Data data;
        public string status;
        public string message;
    }

    [System.Serializable]
    public class Responce_GetUserDetails
    {
        public string status;
        public string message;
        public Responce_OtpLogin_Data data;
    }


    [System.Serializable]
    public class Responce_AddMinusWallet
    {
        public string status;
        public string message;
    }


    [System.Serializable]
    public class Responce_Referral
    {
        public int ReferFromBonus;
        public int ReferToBonus;
        public string ReferalCode;
        public string ReferByUser;
        public string Message;
    }


    [System.Serializable]
    public class Responce_SaveTempPayment
    {
        public string TransactionId;
        public string Message;
      
    }




    [System.Serializable]
    public class Responce_Offer
    {
        public int OfferId;
        public string OfferTypeName;
        public string OfferName;
        public string Description;
        public string Promocode;
        public string Thumbnail;
        public int DiscountAmount;
        public int Amount;
    }
    [System.Serializable]
    public class Responce_OfferList
    {
        public List<Responce_Offer> OfferList;
        public string Message;
    }


    [System.Serializable]
    public class Responce_ApplyPromo
    {
        public int CustomerOfferId;
        public int DiscountAmount;
        public string Message;
        public int Amount;
    }




    [System.Serializable]
    public class Responce_TransactionHistory
    {
        public List<Responce_Transaction> TransactionHistory;
        public string Message;
    }
    [System.Serializable]
    public class Responce_Transaction
    {
        public float  Amount;
        public string Wallet;
        public string Remarks;
        public string Date;
        public string TransactionId;
        public string PaymentStatus;
    }
    [System.Serializable]
    public class Responce_Payout
    {
        public string Message;

    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    [System.Serializable]
    public class AppDetail
    {
        public string AppVersion;
        public string Description;
        public string AppUrl;
    }

    [System.Serializable]
    public class Response_App_Update
    {
        public AppDetail AppDetail;
        public bool IsUpdateAvailable;
        public string Message;

    }
}