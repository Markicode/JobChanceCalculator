using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobChanceCalculator
{
    /// <summary>
    /// Assignment class is used for creating Person objects using information extracted from the database.
    /// </summary>
    internal class Assignment
    {
        private static DbConnection DbConn = new DbConnection();

        public delegate void PeopleAssignedDelegate(string message);

        public event PeopleAssignedDelegate? Assigned;

        /// <summary>
        /// Task running the AssignPeople method which returns a List of Person objects.
        /// On completion, the Assigned event is raised.
        /// </summary>
        /// <returns>List of Person objects</returns>
        public async Task<List<Person>> AssignPeopleAsync()
        {
            Task<List<Person>> generatePeopleList = Task.Run(() => AssignPeople());
            await generatePeopleList;
            if (this.Assigned != null)
            {
                this.Assigned("Read data from database and ordered list of people.");
            }
            return generatePeopleList.Result;

        }

        /// <summary>
        /// Function that retrieves information from the person table in the database and stores the information in Person objects.
        /// </summary>
        /// <returns>List of Person objects</returns>
        public static List<Person> AssignPeople()
        {
            List<Person> people = new List<Person>();
            List<object> results = DbConn.PerformQuery("SELECT * FROM person");
            if (results.Count() > 0)
            {
                foreach (var result in results)
                {
                    List<object> valueList = result as List<object>;
                    Person person = new Person(Convert.ToInt32(valueList[0]), valueList[1].ToString(), valueList[2].ToString());
                    people.Add(person);
                }
            }

            return people;
        }
    }
}
