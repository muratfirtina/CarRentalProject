using System.Reflection;
using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors;

public class AspectInterceptorSelector:IInterceptorSelector
{
    public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
    {
        var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>
            (true).ToList();
        var methodAttributes = type.GetMethod(method.Name)
            .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
        classAttributes.AddRange(methodAttributes);
        // classAttributes.Add(new ExceptionLogAspect(typeof(FileLogger)));

        return classAttributes.OrderBy(x => x.Priority).ToArray();
    }
}

/*public class ExceptionLogAspect : MethodInterceptionBaseAttribute
{
    private Type _loggerType;

    public ExceptionLogAspect(Type loggerType)
    {
        if (!typeof(ILogger).IsAssignableFrom(loggerType))
        {
            throw new System.Exception("Wrong logger type");
        }

        _loggerType = loggerType;
    }

    public override void OnException(IInvocation invocation)
    {
        var logger = (ILogger)Activator.CreateInstance(_loggerType);
        logger.Error(invocation.Exception.Message);
        base.OnException(invocation);
    }
}*/