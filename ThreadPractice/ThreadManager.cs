namespace ThreadPractice;

public class ThreadManager
{
    private List<Thread> _threads = new List<Thread>();

    public void Manage()
    {
        Console.WriteLine($"Main thread: {Thread.CurrentThread.ManagedThreadId}");
        List<Thread> threads = new List<Thread>();
        int number = 0;
        for (int i = 0; i < 100; i++)
        {
            Thread thread = new Thread(() =>
            {
                Interlocked.Add(ref number, 5);
                //number = Method(number);
                MethodVoid(number);
            });
            thread.IsBackground = true;
            threads.Add(thread);
            thread.Start();
        }
        
        foreach (var thread in threads)
        {
            thread.Join();
        }

        Console.WriteLine("The End!");
    }
    
    public int Method(int number)
    {
        number += 5;
        Thread.Sleep(Random.Shared.Next(500));
        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}\t{DateTime.Now}\tResult: {number}");
        return number;
    }
    
    public void MethodVoid(int number)
    {
        Thread.Sleep(Random.Shared.Next(500));
        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}\t{DateTime.Now}\tResult: {number}");
    }

    public void AnnigilateAllThreads()
    {
        foreach (var thread in _threads)
        {
            try
            {
                thread.Interrupt();
            }
            finally
            {
                Console.WriteLine($"Thread {thread.ManagedThreadId} was annigilated =)");
            }
        }
    }
    
    public void WrittingFiles()
    {
        for (int i = 0; i < 2; i++)
        {
            try
            {
                Thread thread = new Thread(Write);
                _threads.Add(thread);
                thread.Start(i);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

    public void Write(object parameter)
    {
        if (parameter is not int)
        {
            throw new Exception("DEBIL!!!");
        }

        int threadId = (int)parameter;
        FileStream fileStream = new FileStream("threadId=" + threadId, FileMode.OpenOrCreate);
        StreamWriter streamWriter = new StreamWriter(fileStream);
        var count = 0;
        for (int i = 0; i < 50; i++)
        {
            try
            {
                Thread.Sleep(100);
                streamWriter.WriteLine(
                    $"ThreadId: {Thread.CurrentThread.ManagedThreadId}\tTime: {DateTime.Now}\tWrite #{count++}");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error");
                return;
            }
        }
        
        streamWriter.Close();
        fileStream.Close();
    }
}