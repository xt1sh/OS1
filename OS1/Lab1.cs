using System;
using System.Collections.Generic;
using System.Text;

namespace OS1
{
    public class Lab1
    {
        public readonly int NumberOfProcesses;
        public int CurrentTime = 0;
        public int[,] ContentTable;
        public int TimeQuantum = 2;

        public Lab1(int numberOfProcesses)
        {
            NumberOfProcesses = numberOfProcesses;
            ContentTable = new int[NumberOfProcesses, 6];

        }

        public void SRTF()
        {
            int[] RemainingTimeArray = new int[NumberOfProcesses];
            int i;
            for (i = 0; i < NumberOfProcesses; i++)
            {
                RemainingTimeArray[i] = ContentTable[i, 1];
            }
            i = 0;
            while (i < NumberOfProcesses)
            {
                int lowestTime = 1000000;
                int lowestTimeCount = 0;
                for (int k = 0; k < NumberOfProcesses; k++)
                {
                    if (RemainingTimeArray[k] > 0 && ContentTable[k, 0] <= CurrentTime && RemainingTimeArray[k] < lowestTime)
                    {
                        lowestTimeCount = k;
                        lowestTime = RemainingTimeArray[k];
                    }
                }
                if (ContentTable[lowestTimeCount, 2] == 0)
                {
                    ContentTable[lowestTimeCount, 2] = CurrentTime;
                }
                RemainingTimeArray[lowestTimeCount] -= TimeQuantum;
                CurrentTime += TimeQuantum;
                if (RemainingTimeArray[lowestTimeCount] <= 0)
                {
                    i++;
                    ContentTable[lowestTimeCount, 3] = CurrentTime;
                    ContentTable[lowestTimeCount, 4] = CurrentTime - ContentTable[lowestTimeCount, 0];
                    ContentTable[lowestTimeCount, 5] = ContentTable[lowestTimeCount, 3] - ContentTable[lowestTimeCount, 2] + ContentTable[lowestTimeCount, 4];
                }
            }
        }
        public void Output()
        {
            double AverageDelay = 0;
            double AverageTotalTime = 0;
            for (int i = 0; i < NumberOfProcesses; i++)
            {
                AverageDelay += ContentTable[i, 4];
                AverageTotalTime += ContentTable[i, 5];
            }
            Console.WriteLine("    Time comes  | Lead Time | Start t | End Time |   Delay   |  Total time");
            for (int i = 0; i < NumberOfProcesses; i++)
            {
                for (int j = 0; j < 6; j++)
                    Console.Write("{0, 12}", ContentTable[i, j]);
                Console.WriteLine();

            }
            Console.WriteLine();
            Console.WriteLine("Average delay: " + AverageDelay / NumberOfProcesses);
            Console.WriteLine("Average time: " + AverageTotalTime / NumberOfProcesses);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }
        public void ResetTable(int RowCount)
        {
            Random Rand = new Random();
            CurrentTime = 100000;
            for (int i = 0; i < RowCount; i++)
            {
                ContentTable[i, 0] = i + Rand.Next(0, 57576);
                if (ContentTable[i, 0] < CurrentTime)
                    CurrentTime = ContentTable[i, 0];
                ContentTable[i, 1] = Rand.Next(1, 1000000);
            }
        }
        public void RR()
        {
            int i = 0;
            int[] RemainingTimeArray = new int[NumberOfProcesses];
            CurrentTime = ContentTable[0, 0];
            bool WorkWasDone = false;
            for (i = 0; i < NumberOfProcesses; i++)
            {
                RemainingTimeArray[i] = ContentTable[i, 1];
            }
            i = 0;
            while (i < NumberOfProcesses)
            {
                for (int j = 0; j < NumberOfProcesses; j++)
                {
                    if (RemainingTimeArray[j] > TimeQuantum && ContentTable[j, 0] <= CurrentTime)
                    {
                        if (RemainingTimeArray[j] == ContentTable[j, 1])
                        {
                            ContentTable[j, 2] = CurrentTime;
                        }
                        RemainingTimeArray[j] -= TimeQuantum;
                        CurrentTime += TimeQuantum;
                        WorkWasDone = true;
                    }
                    else
                    {
                        if (RemainingTimeArray[j] > 0 && RemainingTimeArray[j] <= TimeQuantum && ContentTable[j, 0] <= CurrentTime)
                        {
                            if (RemainingTimeArray[j] == ContentTable[j, 1])
                            {
                                ContentTable[j, 2] = CurrentTime;
                            }
                            CurrentTime += RemainingTimeArray[j];
                            RemainingTimeArray[j] = 0;
                            ContentTable[j, 3] = CurrentTime;
                            i++;
                            WorkWasDone = true;
                        }
                    }
                }
                if (!WorkWasDone)
                {
                    CurrentTime += TimeQuantum;
                }
                WorkWasDone = false;

            }
            for (i = 0; i < NumberOfProcesses; i++)
            {
                ContentTable[i, 4] = ContentTable[i, 3] - ContentTable[i, 0] - ContentTable[i, 1];
                ContentTable[i, 5] = ContentTable[i, 3] - ContentTable[i, 2] + ContentTable[i, 4];
            }
        }
    }
}