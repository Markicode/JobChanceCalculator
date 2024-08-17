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
        private static DbConnection DbConn = new DbConnection();

        public Person(int id, string firstName, string lastName) 
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
        }

        public double calculateGraduateChance()
        {
            Random random = new Random();
            double graduateChance = random.NextDouble();
            this.graduateChance = graduateChance;
            return graduateChance;
        }

        public double calculateJobChance() 
        {
            Random random = new Random();
            double jobChance = random.NextDouble();
            this.jobChance = jobChance;
            return jobChance;
        }

        public static async Task DeletePerson(Person person)
        {
            Task deletionTask = Task.Run(() =>
            {
                DbConn.PerformNonQuery($"DELETE FROM person WHERE id = {person.id}");
            });
            await deletionTask;
        }

        public static async Task AddPerson(string firstName, string lastName)
        {
            Task additionTask = Task.Run(() =>
            {
                DbConn.PerformNonQuery(@$"INSERT INTO person (first_name, last_name) VALUES ('{firstName}', '{lastName}')");
            });
            await additionTask;
        }
    }
}
