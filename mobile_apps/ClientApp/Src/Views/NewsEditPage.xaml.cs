using ClientApp.Src.ViewModels;
using RequestsLibrary.Models;
using System.Diagnostics;

namespace ClientApp.Src.Views;

public partial class NewsEditPage : ContentPage
{
	public NewsEditPage(FullNews? editableNews = null)
	{
		InitializeComponent();

		BindingContext = new NewsEditViewModel(editableNews);
	}
}