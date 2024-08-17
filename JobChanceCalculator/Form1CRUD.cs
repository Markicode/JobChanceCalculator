using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobChanceCalculator
{
    public partial class Form1
    {
        private async void DeleteButton1_Click(object sender, EventArgs e)
        {
            if (peopleList.Count == 0)
            {
                if (this.validateInput(1)) 
                {
                    MessageBox.Show("added");
                    await Person.AddPerson(FirstNameTextBox1.Text, LastNameTextBox1.Text);
                    await ReAssign();
                }
            }
            else
            {
                MessageBox.Show("deleted");
                await Person.DeletePerson(peopleList[0]);
                await ReAssign();
            }
        }

        private async void DeleteButton2_Click(object sender, EventArgs e)
        {
            if (peopleList.Count <= 1)
            {
                if (this.validateInput(2))
                {
                    await Person.AddPerson(FirstNameTextBox2.Text, LastNameTextBox2.Text);
                    await ReAssign();
                }
            }
            else
            {
                await Person.DeletePerson(peopleList[1]);
                await ReAssign();
            }
        }

        private async void DeleteButton3_Click(object sender, EventArgs e)
        {
            if (peopleList.Count <= 2)
            {
                if (this.validateInput(3))
                {
                    await Person.AddPerson(FirstNameTextBox3.Text, LastNameTextBox3.Text);
                    await ReAssign();
                }
            }
            else
            {
                await Person.DeletePerson(peopleList[2]);
                await ReAssign();
            }
        }

        private async void DeleteButton4_Click(object sender, EventArgs e)
        {
            if (peopleList.Count <= 3)
            {
                if (this.validateInput(4))
                {
                    await Person.AddPerson(FirstNameTextBox4.Text, LastNameTextBox4.Text);
                    await ReAssign();
                }
            }
            else
            {
                await Person.DeletePerson(peopleList[3]);
                await ReAssign();
            }
        }

        private async void DeleteButton5_Click(object sender, EventArgs e)
        {
            if (peopleList.Count <= 4)
            {
                if (this.validateInput(5))
                {
                    await Person.AddPerson(FirstNameTextBox5.Text, LastNameTextBox5.Text);
                    await ReAssign();
                }
            }
            else
            {
                await Person.DeletePerson(peopleList[4]);
                await ReAssign();
            }
        }

        private async void DeleteButton6_Click(object sender, EventArgs e)
        {
            if (peopleList.Count <= 5)
            {
                if (this.validateInput(6))
                {
                    await Person.AddPerson(FirstNameTextBox6.Text, LastNameTextBox6.Text);
                    await ReAssign();
                }
            }
            else
            {
                await Person.DeletePerson(peopleList[5]);
                await ReAssign();
            }
        }

        private async void DeleteButton7_Click(object sender, EventArgs e)
        {
            if (peopleList.Count <= 6)
            {
                if (this.validateInput(7))
                {
                    await Person.AddPerson(FirstNameTextBox7.Text, LastNameTextBox7.Text);
                    await ReAssign();
                }
            }
            else
            {
                await Person.DeletePerson(peopleList[6]);
                await ReAssign();
            }
        }

        private async void DeleteButton8_Click(object sender, EventArgs e)
        {
            if (peopleList.Count <= 7)
            {
                if (this.validateInput(8))
                {
                    await Person.AddPerson(FirstNameTextBox8.Text, LastNameTextBox8.Text);
                    await ReAssign();
                }
            }
            else
            {
                await Person.DeletePerson(peopleList[7]);
                await ReAssign();
            }
        }

        private async void DeleteButton9_Click(object sender, EventArgs e)
        {
            if (peopleList.Count <= 8)
            {
                if (this.validateInput(9))
                {
                    await Person.AddPerson(FirstNameTextBox9.Text, LastNameTextBox9.Text);
                    await ReAssign();
                }
            }
            else
            {
                await Person.DeletePerson(peopleList[8]);
                await ReAssign();
            }
        }

        private async void DeleteButton10_Click(object sender, EventArgs e)
        {
            if (peopleList.Count <= 9)
            {
                if (this.validateInput(10))
                {
                    string firstName = FirstNameTextBox10.Text;
                    string lastName = LastNameTextBox10.Text;
                    MessageBox.Show($"added {firstName} {lastName}");
                    await Person.AddPerson(firstName, lastName);
                    await ReAssign();
                }
            }
            else
            {
                MessageBox.Show("deleted");
                await Person.DeletePerson(peopleList[9]);
                await ReAssign();
            }
        }

    }
}
