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

        public event PersonAddedDelegate? PersonAdded;
        public event PersonDeletedDelegate? PersonDeleted;
        public event PersonUpdatedDelegate? PersonUpdated;

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
                ('Robert'),
                ('Petra'),
                ('Koen'),
                ('Wilma'),
                ('Vera'),
                ('Teun'),
                ('Yvonne'),
                ('Mike'),
                ('Rosa'),
                ('Ferry')");
                }
                testList = database.PerformQuery(@"SELECT * FROM profession");
                if (testList.Count == 0)
                {
                    database.PerformNonQuery(@"
                INSERT INTO profession (profession_name) VALUES
                ('Robert'),
                ('Petra'),
                ('Koen'),
                ('Wilma'),
                ('Vera'),
                ('Teun'),
                ('Yvonne'),
                ('Mike'),
                ('Rosa'),
                ('Ferry')");
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
                this.PerformNonQuery(@$"UPDATE person (first_name, last_name) VALUES ('{firstName}', '{lastName}') WHERE id ={person.id}");
                Thread.Sleep(2000);
            });
            await updateTask;
            if (PersonUpdated != null)
            {
                this.PersonUpdated($"{firstName} {lastName} updated.");
            }
        }
    }
}
