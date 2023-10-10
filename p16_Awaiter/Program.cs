// See https://aka.ms/new-console-template for more information

/* Awaitable pattern:
 
metdod run to await expression
1 - bool IsCompleted() - check if operation is completed

2 - TResult GetResult() - fetch result when operation completed

3 - void OnCompleted(Action continuation) - attach continuation to not completed operation

Conditions of awaiter pattern:

Your type needs a GetAwaiter() method that returns an awaiter type.
The awaiter type must implement the System.Runtime.INotifyCompletion interface. That interface contains a single method: void OnCompleted(Action).
The awaiter type must have a readable instance property bool IsCompleted.
The awaiter type must have an instance method GetResult(). This method can return data, or it can be void. The return type of this method determines the result of the async operation.

*/

using System.Runtime.CompilerServices;

Console.WriteLine("before first await");
var r1 = await new MyAwait(true);
Console.WriteLine(r1);

Console.WriteLine("before second await");
var r2 = await new MyAwait(false);
Console.WriteLine(r2);


public class MyAwait
{
    private readonly bool _flag;

    public MyAwait(bool flag)
    {
        _flag = flag;
    }

    public MyAwaiter GetAwaiter() => new MyAwaiter(_flag);
}

public class MyAwaiter : INotifyCompletion
{
    private bool _flag;

    public MyAwaiter(bool flag)
    {
        _flag = flag;
    }

    public bool IsCompleted
    {
        get
        {
            Console.WriteLine("IsCompleted call");
            return _flag;
        }
    }

    public int GetResult()
    {
        Console.WriteLine("GetResult call");
        return 1;
    }

    // Continuation - like Continue With
    public void OnCompleted(Action continuation)
    {
        Console.WriteLine("Continuation called");
        continuation();
    }
}