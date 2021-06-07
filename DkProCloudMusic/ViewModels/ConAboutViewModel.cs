using System.Reflection;
using System.Windows.Input;
using Dk.Common;
using DkProCloudMusic.Models;

namespace DkProCloudMusic.ViewModels
{
    public class ConAboutViewModel
    {
        public ConAboutModel Model { get; set; }

        public ConAboutViewModel()
        {
            this.Model = new ConAboutModel();
        }

        /// <summary>
        /// 软件更新
        /// </summary>
        public ICommand SoftwareUpdateCommand => new DelegateCommand<object>(delegate (object obj)
        {
            //this.Model.NowVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            //var scriptJson = IOHelper.GetScriptJsonModel();
            //this.Model.NowScriptVersion = scriptJson.Version;
        });
        /// <summary>
        /// 软件更新
        /// </summary>
        public ICommand ScriptUpdateCommand => new DelegateCommand<object>(delegate (object obj)
        {
            //this.Model.NowVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            //var scriptJson = IOHelper.GetScriptJsonModel();
            //this.Model.NowScriptVersion = scriptJson.Version;
        });



        public ICommand LoadedCommand
        {
            get
            {
                return new DelegateCommand<object>(delegate (object obj)
                {
                    this.Model.NowVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                    var scriptJson = IOHelper.GetScriptJsonModel();
                    this.Model.NowScriptVersion = scriptJson.Version;
                });
            }
        }
    }
}