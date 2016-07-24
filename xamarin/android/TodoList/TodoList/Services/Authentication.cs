using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TodoList.Helpers;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;


namespace TodoList.Services
{
    public class Authentication : IAuthentication
    {
        public async Task<MobileServiceUser> LoginAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider)
        {
            try
            {

                Settings.LoginAttempts++;
                var user = await client.LoginAsync(Forms.Context, provider);
                Settings.AuthToken = user?.MobileServiceAuthenticationToken ?? string.Empty;
                Settings.UserId = user?.UserId ?? string.Empty;
                return user;
            }
            catch (Exception e)
            {
                e.Data["method"] = "LoginAsync";
                //Xamarin.Insights.Report(e);
            }

            return null;
        }

        public void ClearCookies()
        {
            try
            {
                if ((int)global::Android.OS.Build.VERSION.SdkInt >= 21)
                    global::Android.Webkit.CookieManager.Instance.RemoveAllCookies(null);
            }
            catch (Exception ex)
            {
            }
        }

       
    }
}