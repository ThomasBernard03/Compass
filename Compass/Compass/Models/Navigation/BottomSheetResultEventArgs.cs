using System;
namespace Compass.Models.Navigation;

public class BottomSheetResultEventArgs : EventArgs
{
    public object Result { get; set; }

    public BottomSheetResultEventArgs(object result)
    {
        Result = result;
    }
}

