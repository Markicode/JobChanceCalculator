using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JobChanceCalculator
{
    internal class Person
    {
        public int id {  get; set; }
        public string firstName {  get; set; }
        public string lastName { get; set; }
        public double graduateChance { get; set; }
        public double jobChance {  get; set; }
        public double factor { get; set; }
        public string graduate {  get; set; }
        public string job { get; set; }

        private static DbConnection DbConn = new DbConnection();

        public delegate void GraduationChanceCalculatedDelegate(string message);
        public delegate void JobChanceCalculatedDelegate(string message);
        public delegate void CalculationCompletedDelegate(string message);
        public delegate void MainProgressUpdatedDelegate(int progressCount, IProgress<int> progress);

        public event GraduationChanceCalculatedDelegate graduationChanceCalculated;
        public event JobChanceCalculatedDelegate jobChanceCalculated;
        public event CalculationCompletedDelegate calculationCompleted;
        public event MainProgressUpdatedDelegate mainProgressUpdated;

        public Person(int id, string firstName, string lastName) 
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.factor = 0;
        }

        public void CalculateChances(IProgress<int> progress, IProgress<int> progressMain, CancellationToken token)
        {
            List<string> educations = DbConn.GetEducations();
            List<string> professions = DbConn.GetProfessions();
            double currentFactor;
            int progressCounter = 0;
            int counterDifference = 0;

            Random random = new Random();

            for (int i = 0; i < 10; i++)
            {
                int sleep1 = random.Next(50, 500);
                for (int j = 1; j < sleep1; j++)
                {
                    Thread.Sleep(1);
                    if (j % 10 == 0)
                    {
                        if (token.IsCancellationRequested)
                        {
                            if (mainProgressUpdated != null)
                            {
                                mainProgressUpdated(-progressCounter, progressMain);
                            }
                                progressCounter = 0;
                            if (progress != null)
                            {
                                progress.Report(progressCounter);
                            }
                            token.ThrowIfCancellationRequested();
                            return;
                        }
                        progressCounter += 10;
                        if (progress != null)
                        {
                            progress.Report(progressCounter);
                        }
                        if (mainProgressUpdated != null)
                        {
                            mainProgressUpdated(10, progressMain);
                        }
                            
                    }
                }
                double graduationChance = random.NextDouble();
                if(graduationChanceCalculated != null)
                {
                    graduationChanceCalculated($"{this.firstName} {this.lastName}: Chance of getting graduate in the field of {educations[i]} calculated: {graduationChance.ToString("0.##")}");
                }

                int sleep2 = random.Next(50, 500);
                for (int j = 1; j < sleep2; j++)
                {
                    Thread.Sleep(1);
                    if (j % 10 == 0)
                    {
                        if (token.IsCancellationRequested)
                        {
                            if (mainProgressUpdated != null)
                            {
                                mainProgressUpdated(-progressCounter, progressMain);
                            }
                            progressCounter = 0;
                            if (progress != null)
                            {
                                progress.Report(progressCounter);
                            }
                            token.ThrowIfCancellationRequested();
                            return;
                        }
                        progressCounter += 10;
                        if (progress != null)
                        {
                            progress.Report(progressCounter);
                        }
                        if (mainProgressUpdated != null)
                        {
                            mainProgressUpdated(10, progressMain);
                        }
                    }
                }
                double professionChance = random.NextDouble();
                if (jobChanceCalculated != null)
                {
                    jobChanceCalculated($"{this.firstName} {this.lastName}: Chance of getting job as {professions[i]} calculated: {professionChance.ToString("0.##")}");
                }

                currentFactor = graduationChance * professionChance;
                counterDifference = ((i + 1) * 1000) - progressCounter;

                progressCounter = (i + 1) * 1000;
                if (progress != null)
                {
                    progress.Report(progressCounter);
                }
                if (mainProgressUpdated != null)
                {
                    mainProgressUpdated(counterDifference, progressMain);
                }
                if (currentFactor > this.factor)
                {
                    this.factor = currentFactor;
                    this.graduateChance = graduationChance;
                    this.jobChance = professionChance;
                    this.graduate = educations[i];
                    this.job = professions[i];
                }
            }
            if(calculationCompleted != null)
            {
                calculationCompleted($"All calculations for {this.firstName} {this.lastName} completed.");
            }
        }

    }
}
