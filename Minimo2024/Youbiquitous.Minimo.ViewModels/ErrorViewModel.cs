///////////////////////////////////////////////////////////////////
//
// Projects MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//

using Youbiquitous.Minimo.Shared.Exceptions;

namespace Youbiquitous.Minimo.ViewModels;

public class ErrorViewModel : MainViewModelBase
{
    public MinimoException Error { get; private set; }

    public string Path { get; set; }

    public void SetError(Exception error)
    {
        if (error is MinimoException exception)
            Error = exception;
        else
            Error = new MinimoException(error);
    }

    public bool HasError()
    {
        return Error != null;
    }
}