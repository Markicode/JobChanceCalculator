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
            await HandleAddDelete(0, sender);
        }

        private async void AddDeleteButton2_Click(object sender, EventArgs e)
        {
            await HandleAddDelete(1, sender);
        }

        private async void AddDeleteButton3_Click(object sender, EventArgs e)
        {
            await HandleAddDelete(2, sender);
        }

        private async void AddDeleteButton4_Click(object sender, EventArgs e)
        {
            await HandleAddDelete(3, sender);
        }

        private async void AddDeleteButton5_Click(object sender, EventArgs e)
        {
            await HandleAddDelete(4, sender);
        }

        private async void AddDeleteButton6_Click(object sender, EventArgs e)
        {
            await HandleAddDelete(5, sender);
        }

        private async void AddDeleteButton7_Click(object sender, EventArgs e)
        {
            await HandleAddDelete(6, sender);
        }

        private async void AddDeleteButton8_Click(object sender, EventArgs e)
        {
            await HandleAddDelete(7, sender);
        }

        private async void AddDeleteButton9_Click(object sender, EventArgs e)
        {
            await HandleAddDelete(8, sender);
        }

        private async void AddDeleteButton10_Click(object sender, EventArgs e)
        {
            await HandleAddDelete(9, sender);
        }

        private async Task HandleAddDelete(int position, object sender)
        {
            if (peopleArray[position] != null)
            {
                calculateButtons[position].Enabled = false;
                addDeleteButtons[position].Enabled = false;
                editButtons[position].Enabled = false;
                Person currentPerson = peopleArray[position];
                //peopleList.Remove((Person p) => { p.Id == currentPerson.id; });
                await dbConn.DeletePerson(peopleArray[position]);
                peopleArray[position] = null;
                addDeleteButtons[position].Text = "Add";
                addDeleteButtons[position].Enabled = true;
                firstNameLabels[position].Text = "";
                lastNameLabels[position].Text = "";
                firstNameTextBoxes[position].Visible = true;
                lastNameTextBoxes[position].Visible = true;
            }
            else
            {
                if(this.validateInput(position))
                {
                    addDeleteButtons[position].Enabled = false;
                    await dbConn.AddPerson(firstNameTextBoxes[position].Text, lastNameTextBoxes[position].Text);
                    Person personAdded = await dbConn.FindPerson();
                    if (personAdded != null)
                    {
                        peopleArray[position] = personAdded;
                        editButtons[position].Enabled = true;
                        addDeleteButtons[position].Text = "Delete";
                        addDeleteButtons[position].Enabled = true;
                        firstNameLabels[position].Text = peopleArray[position].firstName;
                        lastNameLabels[position].Text = peopleArray[position].lastName;
                        firstNameTextBoxes[position].Text = "";
                        lastNameTextBoxes[position].Text = "";
                        firstNameTextBoxes[position].Visible = false;
                        lastNameTextBoxes[position].Visible = false;
                        calculateButtons[position].Enabled = true;
                    }
                    
                }
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
