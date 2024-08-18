using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobChanceCalculator
{
    public partial class Form1
    {
        private async void AddDeleteButton1_Click(object sender, EventArgs e)
        {
            await HandleAddDelete(1, sender);
        }

        private async void AddDeleteButton2_Click(object sender, EventArgs e)
        {
            await HandleAddDelete(2, sender);
        }

        private async void AddDeleteButton3_Click(object sender, EventArgs e)
        {
            await HandleAddDelete(3, sender);
        }

        private async void AddDeleteButton4_Click(object sender, EventArgs e)
        {
            await HandleAddDelete(4, sender);
        }

        private async void AddDeleteButton5_Click(object sender, EventArgs e)
        {
            await HandleAddDelete(5, sender);
        }

        private async void AddDeleteButton6_Click(object sender, EventArgs e)
        {
            await HandleAddDelete(6, sender);
        }

        private async void AddDeleteButton7_Click(object sender, EventArgs e)
        {
            await HandleAddDelete(7, sender);
        }

        private async void AddDeleteButton8_Click(object sender, EventArgs e)
        {
            await HandleAddDelete(8, sender);
        }

        private async void AddDeleteButton9_Click(object sender, EventArgs e)
        {
            await HandleAddDelete(9, sender);
        }

        private async void AddDeleteButton10_Click(object sender, EventArgs e)
        {
            await HandleAddDelete(10, sender);
        }

        private async Task HandleAddDelete(int position, object sender)
        {
                if (peopleList.Count <= position - 1)
                {
                    if (this.validateInput(position))
                    {
                        await dbConn.AddPerson(firstNameTextBoxes[position - 1].Text, lastNameTextBoxes[position - 1].Text);
                        await ReAssign();
                    }
                }
                else
                {
                    await dbConn.DeletePerson(peopleList[position - 1]);
                    await ReAssign();
                }
        }

        private void EditButton1_Click(object sender, EventArgs e)
        {

        }

        private void EditButton2_Click(object sender, EventArgs e)
        {

        }

        private void EditButton3_Click(object sender, EventArgs e)
        {

        }

        private void EditButton4_Click(object sender, EventArgs e)
        {

        }

        private void EditButton5_Click(object sender, EventArgs e)
        {

        }

        private void EditButton6_Click(object sender, EventArgs e)
        {

        }

        private void EditButton7_Click(object sender, EventArgs e)
        {

        }

        private void EditButton8_Click(object sender, EventArgs e)
        {

        }

        private void EditButton9_Click(object sender, EventArgs e)
        {

        }

        private void EditButton10_Click(object sender, EventArgs e)
        {

        }

    }
}
