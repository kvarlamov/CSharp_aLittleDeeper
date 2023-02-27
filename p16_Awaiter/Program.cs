// See https://aka.ms/new-console-template for more information

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