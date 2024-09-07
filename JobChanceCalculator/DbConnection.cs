using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using DbUtil;


namespace JobChanceCalculator
{
    /// <summary>
    /// The DbConnection class uses the Database class from the DbUtil DLL to setup a database and perform several SQL (non)queries.
    /// Events are added to register completed queries.
    /// </summary>
    internal class DbConnection
    {
        Database database;

        public delegate void PersonAddedDelegate(string message);
        public delegate void PersonDeletedDelegate(string message);
        public delegate void PersonUpdatedDelegate(string message);
        public delegate void PeopleDeletedDelegate(string message);

        public event PersonAddedDelegate? PersonAdded;
        public event PersonDeletedDelegate? PersonDeleted;
        public event PersonUpdatedDelegate? PersonUpdated;
        public event PeopleDeletedDelegate? PeopleDeleted;

        public DbConnection()
        {
            this.database = new Database("Careerpath");
        }

        /// <summary>
        /// Task responsible for setting up the applications database by constructing the tables.
        /// </summary>
        public Task SetupDb()
        {
            Task setupTask = Task.Run(() =>
            {
                // Create tables in case they dont exist.
                database.PerformNonQuery(@"
            CREATE TABLE IF NOT EXISTS person (
            id INTEGER PRIMARY KEY,
            first_name VARCHAR(16) NOT NULL,
            last_name VARCHAR(24) NOT NULL)");

                database.PerformNonQuery(@"
            CREATE TABLE IF NOT EXISTS education (
            id INTEGER PRIMARY KEY,
            education_name VARCHAR(16) NOT NULL)");

                database.PerformNonQuery(@"
            CREATE TABLE IF NOT EXISTS profession (
            id INTEGER PRIMARY KEY,
            profession_name VARCHAR(16) NOT NULL)");
            });
            return setupTask;
        }

        /// <summary>
        /// Task responsible for adding content to the applications database.
        /// Before adding rows to the tables, the tables are checked for content.
        /// </summary>
        /// <returns></returns>
        public Task FillDb()
        {
            Task fillTask = Task.Run(() =>
            {
                List<object> testList = new List<object>();
                testList = database.PerformQuery(@"SELECT * FROM person");
                if (testList.Count == 0)
                {
                    database.PerformNonQuery(@"
                INSERT INTO person (first_name, last_name) VALUES
                ('Robert', 'Yversen'),
                ('Petra', 'de Jong'),
                ('Koen', 'Vermeer'),
                ('Wilma', 'den Ouden'),
                ('Vera', 'Teunissen'),
                ('Teun', 'Groot'),
                ('Yvonne', 'Zegwaard'),
                ('Mike', 'de Ruiter'),
                ('Rosa', 'Boomsma'),
                ('Ferry', 'de Vries')");
                }
                testList = database.PerformQuery(@"SELECT * FROM education");
                if (testList.Count == 0)
                {
                    database.PerformNonQuery(@"
                INSERT INTO education (education_name) VALUES
                ('Science'),
                ('Medicine'),
                ('Construction'),
                ('Astronomy'),
                ('Cosmetics'),
                ('Sports'),
                ('Engineering'),
                ('Computer Science'),
                ('Retail'),
                ('Teaching')");
                }
                testList = database.PerformQuery(@"SELECT * FROM profession");
                if (testList.Count == 0)
                {
                    database.PerformNonQuery(@"
                INSERT INTO profession (profession_name) VALUES
                ('Scientist'),
                ('Doctor'),
                ('Carpenter'),
                ('Astronomist'),
                ('Beauty Expert'),
                ('Coach'),
                ('Architect'),
                ('Data Analist'),
                ('Salesman'),
                ('Teacher')");
                }
            });
            return fillTask;
        }

        /// <summary>
        /// Function using the Database class to perform a query.
        /// </summary>
        /// <param name="queryStatement"></param>
        /// <returns>List of objects</returns>
        public List<object> PerformQuery(string queryStatement)
        {
            return this.database.PerformQuery(queryStatement);
        }

        /// <summary>
        /// Function using the Database class to perform a non query.
        /// </summary>
        /// <param name="nonQueryStatement"></param>
        public void PerformNonQuery(string nonQueryStatement)
        {
            this.database.PerformNonQuery(nonQueryStatement);
        }

        /// <summary>
        /// Task deleting a person from the database and raising the PersonDeleted event on completion.
        /// </summary>
        /// <param name="person"></param>
        public async Task DeletePerson(Person person)
        {
            Task deletionTask = Task.Run(() =>
            {
                this.PerformNonQuery($"DELETE FROM person WHERE id = {person.id}");
                Thread.Sleep(2000);
            });
            await deletionTask;
            if (this.PersonDeleted != null)
            {
                this.PersonDeleted($"{person.firstName} {person.lastName} deleted.");
            }
        }

        /// <summary>
        /// Task adding a person to the database and raising the PersonAdded event on completion.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        public async Task AddPerson(string firstName, string lastName)
        {
                Task additionTask = Task.Run(() =>
                {
                    this.PerformNonQuery(@$"INSERT INTO person (first_name, last_name) VALUES ('{firstName}', '{lastName}')");
                    Thread.Sleep(2000);
                });
                await additionTask;
                if (PersonAdded != null)
                {
                    this.PersonAdded($"{firstName} {lastName} added.");
                }
        }

        /// <summary>
        /// Task updating person info in the database and raising the PersonUpdated event on completion.
        /// </summary>
        /// <param name="person"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        public async Task UpdatePerson(Person person, string firstName, string lastName)
        {
            Task updateTask = Task.Run(() =>
            {
                this.PerformNonQuery(@$"UPDATE person SET first_name = '{firstName}', last_name = '{lastName}' WHERE id = {person.id}");
                Thread.Sleep(2000);
            });
            await updateTask;
            if (PersonUpdated != null)
            {
                this.PersonUpdated($"{firstName} {lastName} updated.");
            }
        }

        /// <summary>
        /// Task used to make a Person object with the information stored in the database.
        /// To provide the object with the auto incremented id, the latest entry is retrieved from the database. 
        /// </summary>
        /// <returns>Person</returns>
        public async Task<Person> FindAddedPerson()
        {
            Person person = null;
            Task<Person?> retrievePersonTask = Task.Run(() => RetrievePerson(RetrieveLatestEntry())
            );
            person = await retrievePersonTask;
            return person;
        }

        /// <summary>
        /// Function that takes an id parameter to get information from the database and make a Person object.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Person</returns>
        public Person? RetrievePerson(int id)
        {
            List<object> result = this.PerformQuery(@$"SELECT * FROM person WHERE id='{id}'");
            if (result.Count() > 0)
            {
                List<object> valueList = result[0] as List<object>;
                Person person = new Person(Convert.ToInt32(valueList[0]), valueList[1].ToString(), valueList[2].ToString());
                return person;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Function that retrieves the id of the latest entry in the person table from the database.
        /// </summary>
        /// <returns>int id</returns>
        public int RetrieveLatestEntry()
        {
            List<object> result = this.PerformQuery(@"SELECT last_insert_rowid() FROM person");
            List<object> value = result[0] as List<object>;
            int id = Convert.ToInt32(value[0]);
            return id;
        }

        /// <summary>
        /// Database query method returning all entries from the educations table.
        /// </summary>
        /// <returns>List of string objects</returns>
        public List<string> GetEducations()
        {
            List<string> educations = new List<string>();
            List<object> results = this.PerformQuery("SELECT * FROM education");
            foreach (List<object> result in results)
            {
                educations.Add(result[1].ToString());
            }
            return educations;
        }

        /// <summary>
        /// Database query method returning all entries from the profession table.
        /// </summary>
        /// <returns>List of string objects</returns>
        public List<string> GetProfessions()
        {
            List<string> professions = new List<string>();
            List<object> results = this.PerformQuery("SELECT * FROM profession");
            foreach (List<object> result in results)
            {
                professions.Add(result[1].ToString());
            }
            return professions;
        }

        /// <summary>
        /// Task responsible for the deletion of all entries in the person table. On completion the PeopleDeleted event is raised. 
        /// </summary>
        /// <returns></returns>
        public async Task DeleteAll()
        {
            Task deleteAllTask = Task.Run(() =>
            {
                Thread.Sleep(2000);
                this.PerformNonQuery($"DELETE FROM person");
                
            });
            await deleteAllTask;
            if (PeopleDeleted != null)
            {
                this.PeopleDeleted($"All people deleted.");
            }
        }
    }
}
