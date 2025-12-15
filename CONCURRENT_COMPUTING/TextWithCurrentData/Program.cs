using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskWithCustomData
{
    class CustomData
    {
        public long CreationTime;
        public int Name;
        public int ThreadNum;
    }

    class Program
    {
        public static void Main()
        {
            Task[] taskArray = new Task[10];
            for (int i = 0; i < taskArray.Length; i++)
            {
                taskArray[i] = Task.Factory.StartNew((object obj) =>
                {
                    CustomData data = obj as CustomData;
                    if (data == null)
                        return;

                    data.ThreadNum = Thread.CurrentThread.ManagedThreadId;
                },new CustomData() { Name = i, CreationTime = DateTime.Now.Ticks });
            }
            Task.WaitAll(taskArray);
            foreach (var task in taskArray)
            {
                //AsyncState restituisce l'oggetto che è stato passato quando il Task è stato creato, oppure null se non è stato passato nulla
                var data = task.AsyncState as CustomData;
                if (data != null)
                    Console.WriteLine($"Task Name = {data.Name}, Task Id = {task.Id}, Task status = {task.Status},  created at {data.CreationTime}, ran on Thread Id = {data.ThreadNum}.");
            }
        }

    }
}
// The example displays output like the following:
// Task Name = 0, Task Id = 1, Task status = RanToCompletion,  created at 637737183135068576, ran on Thread Id = 5.
// Task Name = 1, Task Id = 2, Task status = RanToCompletion,  created at 637737183135317927, ran on Thread Id = 6.
// Task Name = 2, Task Id = 3, Task status = RanToCompletion,  created at 637737183135345356, ran on Thread Id = 4.
// Task Name = 3, Task Id = 4, Task status = RanToCompletion,  created at 637737183135369204, ran on Thread Id = 7.
// Task Name = 4, Task Id = 5, Task status = RanToCompletion,  created at 637737183135391197, ran on Thread Id = 11.
// Task Name = 5, Task Id = 6, Task status = RanToCompletion,  created at 637737183135415846, ran on Thread Id = 13.
// Task Name = 6, Task Id = 7, Task status = RanToCompletion,  created at 637737183135794745, ran on Thread Id = 8.
// Task Name = 7, Task Id = 8, Task status = RanToCompletion,  created at 637737183135879399, ran on Thread Id = 10.
// Task Name = 8, Task Id = 9, Task status = RanToCompletion,  created at 637737183135879724, ran on Thread Id = 12.
// Task Name = 9, Task Id = 10, Task status = RanToCompletion,  created at 637737183135879799, ran on Thread Id = 9.