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
            await Person.DeletePerson(peopleList[0]);
            await ReAssign();
        }

        private async void DeleteButton2_Click(object sender, EventArgs e)
        {
            await Person.DeletePerson(peopleList[1]);
            await ReAssign();
        }

        private async void DeleteButton3_Click(object sender, EventArgs e)
        {
            await Person.DeletePerson(peopleList[2]);
            await ReAssign();
        }

        private async void DeleteButton4_Click(object sender, EventArgs e)
        {
            await Person.DeletePerson(peopleList[3]);
            await ReAssign();
        }

        private async void DeleteButton5_Click(object sender, EventArgs e)
        {
            await Person.DeletePerson(peopleList[4]);
            await ReAssign();
        }

        private async void DeleteButton6_Click(object sender, EventArgs e)
        {
            await Person.DeletePerson(peopleList[5]);
            await ReAssign();
        }

        private async void DeleteButton7_Click(object sender, EventArgs e)
        {
            await Person.DeletePerson(peopleList[6]);
            await ReAssign();
        }

        private async void DeleteButton8_Click(object sender, EventArgs e)
        {
            await Person.DeletePerson(peopleList[7]);
            await ReAssign();
        }

        private async void DeleteButton9_Click(object sender, EventArgs e)
        {
            await Person.DeletePerson(peopleList[8]);
            await ReAssign();
        }

        private async void DeleteButton10_Click(object sender, EventArgs e)
        {
            await Person.DeletePerson(peopleList[9]);
            await ReAssign();
        }
    }
}
