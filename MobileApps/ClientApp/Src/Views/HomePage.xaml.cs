using ClientApp.Src.ViewModels;
using System.Diagnostics;

namespace ClientApp.Src.Views;

public partial class HomePage : ContentPage
{
	double previousPositionScrollY = 0;
	double currentPositionScrollY = 0;
	long previousUpdateTime = 0;
	const double accuracy = 0.001;


    enum Direction
	{
		Top,
		Bottom
	}
    public HomePage()
	{
		InitializeComponent();
	}

    private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
    {
		double currentScrollY = e.ScrollY;
		Direction d = Direction.Top;
		long currentTime = DateTime.Now.Ticks;

        if (currentScrollY < 1)
		{
            if (previousPositionScrollY > 1)
                previousUpdateTime = currentTime;

            previousPositionScrollY = currentPositionScrollY = accuracy;
            ((ScrollView)sender).ScrollToAsync(0, accuracy, false);

			DetectToExecute(currentTime, d);
			//Debug.WriteLine("Scrolled skipped");
			return;
        }

        if (currentPositionScrollY != currentScrollY)
		{
			previousPositionScrollY = currentPositionScrollY;
			currentPositionScrollY = currentScrollY;
		}

        //Debug.WriteLine($"Previous: {previousPositionScrollY}, Current: {currentPositionScrollY}, Direction: {d.ToString()}");
    }

	private void DetectToExecute(long currentTime, Direction d)
	{
        if (d != Direction.Top || previousPositionScrollY > 1)
            return;

        //Debug.WriteLine($"Time1: {previousUpdateTime}, Time2: {currentTime}, ms: {(previousUpdateTime - currentTime) / 10_000}");

        if (BindingContext is HomeViewModel hvm
			&& hvm.IsFetching == false
			&& (currentTime - previousUpdateTime) / 10_000 > 450)
        {
            Debug.WriteLine("Execute update ...");
			hvm.TryFetchNewsCommand.Execute(null);
        }

        previousUpdateTime = DateTime.Now.Ticks;
    }
}