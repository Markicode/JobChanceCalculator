using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobChanceCalculator
{
    internal class Assignment
    {
        private static DbConnection DbConn = new DbConnection();

        public delegate void PeopleAssignedDelegate(string message);

        public event PeopleAssignedDelegate Assigned;

        public async Task<List<Person>> AssignPeopleAsync()
        {
            Task<List<Person>> generatePeopleList = Task.Run(() => AssignPeople());
            await generatePeopleList;
            this.Assigned("Data imported from database.");
            return generatePeopleList.Result;

        }

        public static List<Person> AssignPeople()
        {
            List<Person> people = new List<Person>();
            List<object> results = DbConn.PerformQuery("SELECT * FROM person");
            if (results.Count() > 0)
            {
                foreach (var result in results)
                {
                    //if (result != null)
                    //{
                    List<object> valueList = result as List<object>;
                    Person person = new Person(Convert.ToInt32(valueList[0]), valueList[1].ToString(), valueList[2].ToString());
                    people.Add(person);
                    //Thread.Sleep(100);
                    //}
                }
            }

            return people;
        }
    }
}
