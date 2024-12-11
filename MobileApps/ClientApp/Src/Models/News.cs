using CommunityToolkit.Mvvm.ComponentModel;

namespace ClientApp.Src.Models
{
    public partial class News : ObservableObject
    {
        [ObservableProperty]
        private string? category;

        [ObservableProperty]
        private string? imgUrl;

        [ObservableProperty]
        private string? title;

        [ObservableProperty]
        private string? companyImgUrl;

        [ObservableProperty]
        private string? companyName;

        [ObservableProperty]
        private string? date;
    }
}
