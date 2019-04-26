using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using Xamarin.Forms;

namespace UEAssistantMobile
{
    public partial class SchedulePage : ContentPage
    {
        public SchedulePage(string localPath)
        {
            InitializeComponent();
            Debug.WriteLine($"Przekazana do SchedulePage ścieżka:file:///{localPath}");
            PdfView.Source = $"file:///android_asset/pdfjs/web/viewer.html?file=file:///{localPath}";
            //PdfView.Source = $"file:///android_asset/pdfjs/web/viewer.html?file=file:///android_asset/Content/schedule.pdf";

        }
    }
}
