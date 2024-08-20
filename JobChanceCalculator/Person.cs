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

        public delegate void OnGraduationChanceCalculatedDelegate(string message);
        public delegate void OnJobChanceCalculatedDelegate(string message);
        public delegate void OnCalculationCompletedDelegate(string message);
        public delegate void OnMainProgressUpdatedDelegate(int progressCount, IProgress<int> progress);

        public event OnGraduationChanceCalculatedDelegate OnGraduationChanceCalculated;
        public event OnJobChanceCalculatedDelegate OnJobChanceCalculated;
        public event OnCalculationCompletedDelegate OnCalculationCompleted;
        public event OnMainProgressUpdatedDelegate OnMainProgressUpdated;

        public Person(int id, string firstName, string lastName) 
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.factor = 0;
        }

        public void CalculateChances(IProgress<int> progress, IProgress<int> progressMain)
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
                        progressCounter += 10;
                        if (progress != null)
                        {
                            progress.Report(progressCounter);
                        }
                        if (OnMainProgressUpdated != null)
                        {
                            OnMainProgressUpdated(10, progressMain);
                        }
                            
                    }
                }
                double graduationChance = random.NextDouble();
                if(OnGraduationChanceCalculated != null)
                {
                    OnGraduationChanceCalculated($"{this.firstName} {this.lastName}: Chance of getting graduate in the field of {educations[i]} calculated: {graduationChance.ToString("0.##")}");
                }

                int sleep2 = random.Next(50, 500);
                for (int j = 1; j < sleep2; j++)
                {
                    Thread.Sleep(1);
                    if (j % 10 == 0)
                    {
                        progressCounter += 10;
                        if (progress != null)
                        {
                            progress.Report(progressCounter);
                        }
                        if (OnMainProgressUpdated != null)
                        {
                            OnMainProgressUpdated(10, progressMain);
                        }
                    }
                }
                double professionChance = random.NextDouble();
                if (OnGraduationChanceCalculated != null)
                {
                    OnGraduationChanceCalculated($"{this.firstName} {this.lastName}: Chance of getting job as {professions[i]} calculated: {professionChance.ToString("0.##")}");
                }

                currentFactor = graduationChance * professionChance;
                counterDifference = ((i + 1) * 1000) - progressCounter;

                progressCounter = (i + 1) * 1000;
                if (progress != null)
                {
                    progress.Report(progressCounter);
                }
                if (OnMainProgressUpdated != null)
                {
                    OnMainProgressUpdated(counterDifference, progressMain);
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
            if(OnCalculationCompleted != null)
            {
                OnCalculationCompleted($"All calculations for {this.firstName} {this.lastName} completed.");
            }
        }

    }
}
