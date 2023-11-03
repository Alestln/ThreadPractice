using ThreadPractice;

internal class Program
{
    public static async Task Main(string[] args)
    {
        // Создать 100 потоквов и запустить их. У каждого своя задержка до 500 мс. Выполняются хер пойми как.
        // Разница между Main Thread и Background Thread.
        // Что такое Join и как его применять.
        // Как передать параметры в поток. Плюсы минусы этого дела. Подводные камни.
        // Возврат значения из потока. Применять кастыли нужно =)
        
        // Создать два потока и каждый из них в отдельный файл записать какие-то текстовые данные. Они пишут в разные файлы.
        // Каждый пишет 10 сообщений с задержкой в секунду.
        // Показать что это дело работает =)
        // Организовать запись в файл отдельным потоком в бесконечном цикле каждую секунду. (Формат: текущее время, ИД потока и тра-ля-ля)
        // В определенный момент времени прервать это.
        
        // Внимательно смотрим, что получилось.
        // Много думаем...
        // Посел того, как много подумали, пытаемся это дело объяснить.
        
        // После этого выполняем все то же самое используя Task.
        // Сравнение объема кода, удобства и адекватности решения.

        /*ThreadManager threadManager = new ThreadManager();
        
        threadManager.Manage();

        threadManager.WrittingFiles();

        while (true)
        {
            Thread.Sleep(3000);
            threadManager.AnnigilateAllThreads();
            break;
        }*/

        TaskManager taskManager = new TaskManager();
        
        //await taskManager.Manage();

        //int[] randomNumbers = await taskManager.GetRandomNumbers();
        //var randomChars = await taskManager.GetRandomSymbols();
        taskManager.WritingFiles();
        // А Олексій запізнився на роботу
    }
}
