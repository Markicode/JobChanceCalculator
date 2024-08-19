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

        public Person(int id, string firstName, string lastName) 
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.factor = 0;
        }

        public void CalculateChances()
        {
            List<string> educations = DbConn.GetEducations();
            List<string> professions = DbConn.GetProfessions();
            double currentFactor;

            Random random = new Random();

            for (int i = 0; i < 10; i++)
            {
                int sleep1 = random.Next(10, 15);
                for (int j = 0; j < sleep1; j++)
                {
                    Thread.Sleep(1);
                }
                double graduationChance = random.NextDouble();

                int sleep2 = random.Next(10, 15);
                for (int j = 0; j < sleep2; j++)
                {
                    Thread.Sleep(1);
                }
                double professionChance = random.NextDouble();
                currentFactor = graduationChance * professionChance;

                if(currentFactor > this.factor)
                {
                    this.factor = currentFactor;
                    this.graduateChance = graduationChance;
                    this.jobChance = professionChance;
                    this.graduate = educations[i];
                    this.job = professions[i];
                }



            }
        }
    }
}
