using System.Timers;

namespace ClientApp.Src.Utils;
public class Debouncer
{
    private readonly System.Timers.Timer _timer;
    private Action? _action;

    public Debouncer(int delayMilliseconds)
    {
        _timer = new System.Timers.Timer(delayMilliseconds);
        _timer.Elapsed += OnTimerElapsed;
        _timer.AutoReset = false;
    }

    public void Debounce(Action action)
    {
        _timer.Stop();
        _timer.Start();
        _action = action;
    }

    private void OnTimerElapsed(object sender, ElapsedEventArgs e)
    {
        try
        {
            _action?.Invoke();
        }
        catch { }
    }
}