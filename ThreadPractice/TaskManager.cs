namespace ThreadPractice;
public class TaskManager
{
    private List<char> _symbols = new List<char>();
    private object locker = new object();
    private AutoResetEvent waitHandler = new AutoResetEvent(true);

    public async void Manage()
    {
        Console.WriteLine($"Main thread: {Thread.CurrentThread.ManagedThreadId}");

        Task[] tasks = new Task[100];
        for (int i = 0; i < tasks.Length; i++)
        {
            tasks[i] = MethodVoid(i + 1);
        }

        await Task.WhenAll(tasks);
        {}
        Console.WriteLine("The End!");
    }

    public async Task<int[]> GetRandomNumbers()
    {
        Console.WriteLine($"Main thread: {Thread.CurrentThread.ManagedThreadId}");

        Task<int>[] tasks = new Task<int>[100];
        for (var i = 0; i < tasks.Length; i++)
        {
            tasks[i] = GetRandomNumber(i + 1);
        }

        await Task.WhenAll();

        /*int[] result = new int[100];
        for (var i = 0; i < tasks.Length; i++)
        {
            result[i] = await tasks[i];
        }*/
        int[] result = await Task.WhenAll(tasks);
        return result;
    }

    private async Task<int> GetRandomNumber(int counter)
    {
        await Task.Delay(Random.Shared.Next(500));
        Console.WriteLine($"Current task: {counter}");
        return Random.Shared.Next(1, 100);
    }
    
    private async Task MethodVoid(int counter)
    {
        await Task.Delay(Random.Shared.Next(500));
        Console.WriteLine($"Current task: {counter}");
        // return Task.CompletedTask;
    }

    public async Task<List<char>> GetRandomSymbols()
    {
        Task[] tasks = new Task[3600];
        for (var i = 0; i < tasks.Length; i++)
        {
            tasks[i] = GetRandomSymbol();
        }
        
        await Task.WhenAll(tasks);
        
        return _symbols;
    }

    private async Task GetRandomSymbol()
    {
        /*await Task.Delay(1);
        lock (locker)
        {
            _symbols.Add((char)Random.Shared.Next(97, 123));
        }*/

        try
        {
            waitHandler.WaitOne();
            
            await Task.Delay(1);
            _symbols.Add((char)Random.Shared.Next(97, 123));
        }
        finally
        {
            waitHandler.Set();
        }
    }

    public void WritingFiles()
    {
        Task[] tasks = new Task[2];
        for (var i = 0; i < tasks.Length; i++)
        {
            tasks[i] = WriteFile(i + 1);
        }
    }

    private Task WriteFile(int counter)
    {
        FileStream fileStream = new FileStream($"task {counter}", FileMode.OpenOrCreate);
        StreamWriter streamWriter = new StreamWriter(fileStream);

        for (int i = 0; i < 50; i++)
        {
            streamWriter.WriteLine($"task {counter} - data {i + 1} - {DateTime.Now}");
        }
        
        streamWriter.Close();
        fileStream.Close();
        
        return Task.CompletedTask;
    }
}