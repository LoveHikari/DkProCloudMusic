using DkProCloudMusic.ViewModels;
using DotNetLocalizedTool.Views;
using HandyControl.Controls;
using HandyControl.Tools.Extension;

namespace DkProCloudMusic;

public class DialogHelper
{
    private static Dialog? _loadingDialog;
    public static Dialog ShowLoading(string title = "请稍等...")
    {
        _loadingDialog = Dialog.Show(new LoadingDialogView(), "123")
            .Initialize<LoadingDialogViewModel>(vm =>
            {
                vm.Model.Title = title;
            });
        return _loadingDialog;
    }

    public static void CloseLoading()
    {
        _loadingDialog?.Close();
    }
}