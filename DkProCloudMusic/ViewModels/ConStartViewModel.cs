using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Input;
using Dk.Common;
using DkProCloudMusic.Models;

namespace DkProCloudMusic.ViewModels
{
    public class ConStartViewModel
    {
        public ConStartModel Model { get; set; }
        public ConStartViewModel()
        {
            this.Model = new ConStartModel();
        }

        
        
    }
}