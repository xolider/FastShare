using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FastShare.UI.Mobile.Forms.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(FastShare.UI.Mobile.Forms.Droid.Impl.DeviceFilePicker))]
namespace FastShare.UI.Mobile.Forms.Droid.Impl
{
    public class DeviceFilePicker : IDeviceFilePicker
    {
        public async Task<FileResult> PickFile()
        {
            var pickOptions = new PickOptions();
            pickOptions.PickerTitle = "Select a file to send";

            var result = await FilePicker.PickAsync(pickOptions);
            return result;
        }
    }
}