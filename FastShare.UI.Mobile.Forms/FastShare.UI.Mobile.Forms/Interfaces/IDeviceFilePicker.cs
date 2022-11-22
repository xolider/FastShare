using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace FastShare.UI.Mobile.Forms.Interfaces
{
    public interface IDeviceFilePicker
    {
        Task<FileResult> PickFile();
    }
}
