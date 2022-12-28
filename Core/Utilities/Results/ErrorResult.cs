namespace Core.Utilities.Results;

public class ErrorResult:Result
{
    public ErrorResult(string message, object usersInformationNotNull):base(false,message)
    {
        
    }
    public ErrorResult(string carNameInvalid):base(false)
    {
        
    }
}
