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
            await HandleAddDelete(0);
        }

        private async void AddDeleteButton2_Click(object sender, EventArgs e)
        {
            await HandleAddDelete(1);
        }

        private async void AddDeleteButton3_Click(object sender, EventArgs e)
        {
            await HandleAddDelete(2);
        }

        private async void AddDeleteButton4_Click(object sender, EventArgs e)
        {
            await HandleAddDelete(3);
        }

        private async void AddDeleteButton5_Click(object sender, EventArgs e)
        {
            await HandleAddDelete(4);
        }

        private async void AddDeleteButton6_Click(object sender, EventArgs e)
        {
            await HandleAddDelete(5);
        }

        private async void AddDeleteButton7_Click(object sender, EventArgs e)
        {
            await HandleAddDelete(6);
        }

        private async void AddDeleteButton8_Click(object sender, EventArgs e)
        {
            await HandleAddDelete(7);
        }

        private async void AddDeleteButton9_Click(object sender, EventArgs e)
        {
            await HandleAddDelete(8);
        }

        private async void AddDeleteButton10_Click(object sender, EventArgs e)
        {
            await HandleAddDelete(9);
        }

        private async Task HandleAddDelete(int position)
        {
            if (peopleArray[position] != null)
            {
                calculateButtons[position].Enabled = false;
                addDeleteButtons[position].Enabled = false;
                editButtons[position].Enabled = false;
                Person currentPerson = peopleArray[position];
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
                    firstNameTextBoxes[position].Enabled = false;
                    lastNameTextBoxes[position].Enabled = false;
                    addDeleteButtons[position].Enabled = false;
                    await dbConn.AddPerson(firstNameTextBoxes[position].Text, lastNameTextBoxes[position].Text);
                    Person personAdded = await dbConn.FindAddedPerson();
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

        private async void EditSubmitButton1_Click(object sender, EventArgs e)
        {
            await HandleEdit(0);
        }

        private async void EditSubmitButton2_Click(object sender, EventArgs e)
        {
            await HandleEdit(1);
        }

        private async void EditSubmitButton3_Click(object sender, EventArgs e)
        {
            await HandleEdit(2);
        }

        private async void EditSubmitButton4_Click(object sender, EventArgs e)
        {
            await HandleEdit(3);
        }

        private async void EditSubmitButton5_Click(object sender, EventArgs e)
        {
            await HandleEdit(4);
        }

        private async void EditSubmitButton6_Click(object sender, EventArgs e)
        {
            await HandleEdit(5);
        }

        private async void EditSubmitButton7_Click(object sender, EventArgs e)
        {
            await HandleEdit(6);
        }

        private async void EditSubmitButton8_Click(object sender, EventArgs e)
        {
            await HandleEdit(7);
        }

        private async void EditSubmitButton9_Click(object sender, EventArgs e)
        {
            await HandleEdit(8);
        }

        private async void EditSubmitButton10_Click(object sender, EventArgs e)
        {
            await HandleEdit(9);
        }

        private async Task HandleEdit(int position)
        {
            if (editButtons[position].Text == "Edit")
            {
                firstNameTextBoxes[position].Text = peopleArray[position].firstName;
                lastNameTextBoxes[position].Text = peopleArray[position].lastName;
                addDeleteButtons[position].Enabled = false;
                calculateButtons[position].Enabled = false;
                editButtons[position].Text = "Submit";
                firstNameLabels[position].Visible = false;
                lastNameLabels[position].Visible = false;
                firstNameTextBoxes[position].Visible = true;
                lastNameTextBoxes[position].Visible = true;
                return;
            }
            if(editButtons[position].Text == "Submit")
            {
                if(this.validateInput(position))
                {
                    firstNameTextBoxes[position].Enabled = false;
                    lastNameTextBoxes[position].Enabled = false;
                    editButtons[position].Enabled = false;
                    await dbConn.UpdatePerson(peopleArray[position], firstNameTextBoxes[position].Text, lastNameTextBoxes[position].Text);
                    peopleArray[position].firstName = firstNameTextBoxes[position].Text;
                    peopleArray[position].lastName = lastNameTextBoxes[position].Text;
                    editButtons[position].Text = "Edit";
                    editButtons[position].Enabled = true;
                    firstNameLabels[position].Text = peopleArray[position].firstName;
                    lastNameLabels[position].Text = peopleArray[position].lastName;
                    addDeleteButtons[position].Enabled = true;
                    calculateButtons[position].Enabled = true;
                    firstNameLabels[position].Visible = true;
                    lastNameLabels[position].Visible = true;
                    firstNameTextBoxes[position].Visible = false;
                    lastNameTextBoxes[position].Visible = false;
                    firstNameTextBoxes[position].Enabled = true;
                    lastNameTextBoxes[position].Enabled = true;
                }
                return;
            }
        }

     }
}
