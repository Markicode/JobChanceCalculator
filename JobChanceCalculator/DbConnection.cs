using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using DbUtil;


namespace JobChanceCalculator
{
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

        public Task SetupDb()
        {
            Task t = Task.Run(() =>
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
            return t;
        }

        public Task FillDb()
        {
            Task t = Task.Run(() =>
            {
                Database database = new Database("Careerpath");
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
            return t;
        }

        public List<object> PerformQuery(string queryStatement)
        {
            return this.database.PerformQuery(queryStatement);
        }

        public void PerformNonQuery(string nonQueryStatement)
        {
            this.database.PerformNonQuery(nonQueryStatement);
        }

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

        public async Task AddPerson(string firstName, string lastName)
        {
            Person personAdded;
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

        public async Task<Person> FindAddedPerson()
        {
            Person person = null;
            Task<Person?> retrievePersonTask = Task.Run(() => RetrievePerson(RetrieveLatestEntry())
            );
            person = await retrievePersonTask;
            return person;
        }

        public async Task<Person> FindPerson(int id)
        {
            Person person = null;
            Task<Person?> retrievePersonTask = Task.Run(() => RetrievePerson(id)
            );
            person = await retrievePersonTask;
            return person;
        }

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

        public int RetrieveLatestEntry()
        {
            List<object> result = this.PerformQuery(@"SELECT last_insert_rowid() FROM person");
            List<object> value = result[0] as List<object>;
            int id = Convert.ToInt32(value[0]);
            return id;
        }

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

        public async Task DeleteAll()
        {
            Task deleteAllTask = Task.Run(() =>
            {
                this.PerformNonQuery($"DELETE FROM person");
                Thread.Sleep(2000);
            });
            await deleteAllTask;
            if (PeopleDeleted != null)
            {
                this.PeopleDeleted($"All people deleted.");
            }
        }
    }
}
