using ClientApp.Src.ViewModels;
using RequestsLibrary.Models;

namespace ClientApp.Src.Views;

public partial class NewsPage : ContentPage
{
	public NewsPage(MinNews news)
	{
		InitializeComponent();
        //md.MarkdownText = "# Welcome to MarkdownView\n" +
        //                  "This is **bold text**, and this is *italic text*.\n\n" +
        //                  "Here's a blockquote:\n\n" +
        //                  "> This is a blockquote\n\n" +
        //                  "Here's a list:\n" +
        //                  "- Item 1\n" +
        //                  "- Item 2\n\n" +
        //                  "Here's a code block:\n\n" +
        //                  "```cs\n" +
        //                  "var code = \"This is a code block\";\n" +
        //                  "```\n\n" +
        //                  "Here's a link: [Click here](https://example.com)\n\n" +
        //                  "Here's an image:\n" +
        //                  "![Alt text](https://sites.kopchan7.keenetic.link/wepics/api/albums/root/images/4da1007263d7861a14603e910c5aa980/orig)\n";

        if (BindingContext is NewsPageViewModel npvm)
        {
            npvm.News = news;
            npvm.UpdateNewsCommand.Execute(null);
        }
    }
}