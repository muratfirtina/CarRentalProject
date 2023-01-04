using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public abstract class MethodInterceptionBaseAttribute:Attribute,IInterceptor
{
    public int Priority { get; set; }

    public virtual void Intercept(IInvocation invocation)
    {

    }
}

/*public class ExceptionLogAspect:MethodInterception
{
    private LoggerServiceBase _loggerServiceBase;

    public ExceptionLogAspect(Type loggerService)
    {
        if (loggerService.BaseType != typeof(LoggerServiceBase))
        {
            throw new System.Exception("Wrong logger type");
        }

        _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggerService);
    }

    protected override void OnException(IInvocation invocation, System.Exception e)
    {
        var logDetailWithException = GetLogDetail(invocation);
        logDetailWithException.ExceptionMessage = e.Message;
        _loggerServiceBase.Error(logDetailWithException);
    }

    private LogDetailWithException GetLogDetail(IInvocation invocation)
    {
        var logParameters = new List<LogParameter>();
        for (int i = 0; i < invocation.Arguments.Length; i++)
        {
            logParameters.Add(new LogParameter
            {
                Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                Type = invocation.Arguments[i].GetType().Name,
                Value = invocation.Arguments[i]
            });
        }

        var logDetailWithException = new LogDetailWithException
        {
            MethodName = invocation.Method.Name,
            LogParameters = logParameters
        };
        return logDetailWithException;
    }
}*/