using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JobChanceCalculator
{
    /// <summary>
    /// The Person class represents a person whose graduation and job chances can be calculated. All data tied to a person are stored in the object.
    /// </summary>
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
        public string cancelOrigin { get; set; }

        private static DbConnection DbConn = new DbConnection();

        public delegate void GraduationChanceCalculatedDelegate(string message);
        public delegate void JobChanceCalculatedDelegate(string message);
        public delegate void CalculationCompletedDelegate(string message);
        public delegate void MainProgressUpdatedDelegate(int progressCount, IProgress<int> progress);

        public event GraduationChanceCalculatedDelegate graduationChanceCalculated;
        public event JobChanceCalculatedDelegate jobChanceCalculated;
        public event CalculationCompletedDelegate calculationCompleted;
        public event MainProgressUpdatedDelegate mainProgressUpdated;

        /// <summary>
        /// Constructor for the Person class. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        public Person(int id, string firstName, string lastName) 
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.factor = 0;
        }

        /// <summary>
        /// Function calculating random graduation and job chances, storing the results in the object. 
        /// The progress of the calculations is reported to the progress bar tied to this object, and to the main progress bar.
        /// </summary>
        /// <param name="progress"></param>
        /// <param name="progressMain"></param>
        /// <param name="token"></param>
        public void CalculateChances(IProgress<int> progress, IProgress<int> progressMain, CancellationToken token)
        {
            List<string> educations = DbConn.GetEducations();
            List<string> professions = DbConn.GetProfessions();
            double currentFactor;
            int progressCounter = 0;
            int counterDifference = 0;

            Random random = new Random();

            // Loop of 10 iterations. Each iteration calculates a graduation and job chance for all entries from the education and profession db tables.
            // And a factor is calculated.
            for (int i = 0; i < 10; i++)
            {
                // Within each of the 10 i-iterations, both the graduation and the job chance calculations are provided with a random number.
                // This number is used to iterate and sleep over, to simulate an amount of time that the calculations cost.
                // These iterations are also used to update the progress bars, and check if a cancel request is made.
                int sleep1 = random.Next(50, 500);
                for (int j = 1; j < sleep1; j++)
                {
                    Thread.Sleep(1);
                    // Every 10 iterations the progress bar connected to this object is updated and the MainProgressUpdated event is raised.
                    // The main progress bar is subscribed in the form class and updated from there.
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
                // After finishing the calculations in a i-iteration, the progresscounter is updated to 1000 times the iteration to make sure 
                // the progressbars are updated accordingly (having a maximum value of 10000).
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

        /// <summary>
        /// Function calling the Cancel method of the CancellationTokenSource from which the CancellationToken is created used for this object. 
        /// The origin of the Cancel request is required to further handle the cancellation for different cases.
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="cts"></param>
        public void CancelCalculation(string origin, CancellationTokenSource cts)
        {
            cts.Cancel();
            this.cancelOrigin = origin;
        }

    }
}
