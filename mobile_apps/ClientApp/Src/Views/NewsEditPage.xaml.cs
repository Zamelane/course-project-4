using ClientApp.Src.ViewModels;
using RequestsLibrary.Models;

namespace ClientApp.Src.Views;

public partial class NewsEditPage : ContentPage
{
	public NewsEditPage(FullNews? editableNews = null)
	{
		InitializeComponent();

		if (BindingContext is NewsEditViewModel nevm){
			nevm.EditableNews = editableNews ?? new();
		}
	}
}