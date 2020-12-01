namespace OS1
{
    class Program
    {
        static void Main(string[] args)
        {
            int processes = 6;
            Lab1 task = new Lab1(processes);
            task.ResetTable(processes);

            // Shortest Remaining Time First Algorithm
            task.SRTF();
            task.Output();

            // Round Robin Algorithm
            task.RR();
            task.Output();
        }
    }
}