using ClientApp.Src.ViewModels;

namespace ClientApp.Src.Views;

public partial class CommentsPage : ContentPage
{
	public CommentsPage(int newsId)
	{
		InitializeComponent();

		if (BindingContext is CommentsViewModel cvm)
		{
			cvm.NewsId = newsId;
			cvm.FetchMoreCommentsCommand.ExecuteAsync(null);
		}
	}
}