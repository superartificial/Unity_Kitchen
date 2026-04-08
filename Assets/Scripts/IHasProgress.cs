using System;

public interface IHasProgress
{
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChange;

    public class OnProgressChangedEventArgs : EventArgs {
        public float progressNormalized;
    }
}
