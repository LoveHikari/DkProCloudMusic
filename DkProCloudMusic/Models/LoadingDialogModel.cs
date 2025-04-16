using CommunityToolkit.Mvvm.ComponentModel;

namespace DkProCloudMusic.Models;

public class LoadingDialogModel : ObservableObject
{
    private string _title = "请稍等";
    public string Title
    {
        get => _title;
        set { _title = value; OnPropertyChanged(); }
    }
}