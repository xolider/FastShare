using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Content;
using FastShare.UI.Shared.Interfaces;
using FastShare.UI.Mobile.Forms.Pages.Nav;
using FastShare.UI.Mobile.Forms.Pages;
using FastShare.UI.Shared.ViewModels;
using System.Diagnostics;
using Java.Lang;
using Android.Provider;

namespace FastShare.UI.Mobile.Forms.Droid
{
    [Activity(Label = "FastShare", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize ),
        IntentFilter(new[] {"android.intent.action.SEND"}, Categories = new[] {Intent.CategoryDefault}, DataMimeTypes = new[] {"image/*"})]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            if(Intent.Action == Intent.ActionSend && Intent.Type != null)
            {
                var uri = this.Intent.GetParcelableExtra(Intent.ExtraStream) as Android.Net.Uri;
                (App.Current as IApp).CurrentWindow.NavigateToPage(Shared.Components.NavPage.SEND);
                ((((App.Current as IApp).CurrentWindow as NavPage).CurrentPage as SendPage).BindingContext as SendViewModel).SelectedFile = GetFilePathFromUri(uri);

            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private string GetFilePathFromUri(Android.Net.Uri uri)
        {
            string[] proj = new[] { MediaStore.Images.Media.InterfaceConsts.Data };
            var cursor = ContentResolver.Query(uri, proj, null, null, null);

            int column_index = cursor.GetColumnIndex(MediaStore.Images.Media.InterfaceConsts.Data);
            cursor.MoveToFirst();

            var path = cursor.GetString(column_index);
            cursor.Close();
            return path;
        }
    }
}