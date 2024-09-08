using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobChanceCalculator
{
    public partial class Form1
    {
        /// <summary>
        /// The AddDeleteButtons have 2 seperate functions depending on whether the position is null, or has a person assigned to it.
        /// The code to perform is handled in the function called upon by clicking the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void AddDeleteButton1_Click(object sender, EventArgs e)
        {
            Task addDelete1 = HandleAddDelete(0);
            activeTasks.Add(addDelete1);    
            await addDelete1;
            activeTasks.Remove(addDelete1);
        }

        private async void AddDeleteButton2_Click(object sender, EventArgs e)
        {
            Task addDelete2 = HandleAddDelete(1);
            activeTasks.Add(addDelete2);
            await addDelete2    ;
            activeTasks.Remove(addDelete2);
        }

        private async void AddDeleteButton3_Click(object sender, EventArgs e)
        {
            Task addDelete3 = HandleAddDelete(2);
            activeTasks.Add(addDelete3);
            await addDelete3;
            activeTasks.Remove(addDelete3);
        }

        private async void AddDeleteButton4_Click(object sender, EventArgs e)
        {
            Task addDelete4 = HandleAddDelete(3);
            activeTasks.Add(addDelete4);
            await addDelete4;
            activeTasks.Remove(addDelete4);
        }

        private async void AddDeleteButton5_Click(object sender, EventArgs e)
        {
            Task addDelete5 = HandleAddDelete(4);
            activeTasks.Add(addDelete5);
            await addDelete5;
            activeTasks.Remove(addDelete5);
        }

        private async void AddDeleteButton6_Click(object sender, EventArgs e)
        {
            Task addDelete6 = HandleAddDelete(5);
            activeTasks.Add(addDelete6);
            await addDelete6;
            activeTasks.Remove(addDelete6);
        }

        private async void AddDeleteButton7_Click(object sender, EventArgs e)
        {
            Task addDelete7 = HandleAddDelete(6);
            activeTasks.Add(addDelete7);
            await addDelete7;
            activeTasks.Remove(addDelete7);
        }

        private async void AddDeleteButton8_Click(object sender, EventArgs e)
        {
            Task addDelete8 = HandleAddDelete(7);
            activeTasks.Add(addDelete8);
            await addDelete8;
            activeTasks.Remove(addDelete8);
        }

        private async void AddDeleteButton9_Click(object sender, EventArgs e)
        {
            Task addDelete9 = HandleAddDelete(8);
            activeTasks.Add(addDelete9);
            await addDelete9;
            activeTasks.Remove(addDelete9);
        }

        private async void AddDeleteButton10_Click(object sender, EventArgs e)
        {
            Task addDelete10 = HandleAddDelete(9);
            activeTasks.Add(addDelete10);
            await addDelete10;
            activeTasks.Remove(addDelete10);
        }

        /// <summary>
        /// Task checking if given position has a person assigned to it, and adds or deletes a person on execution. 
        /// Updates form accordingly.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private async Task HandleAddDelete(int position)
        {
            Person? personAdded = null;

            if (peopleArray[position] != null)
            {
                calculateButtons[position].Enabled = false;
                addDeleteButtons[position].Enabled = false;
                editButtons[position].Enabled = false;

                try
                {
                    await dbConn.DeletePerson(peopleArray[position]);
                    peopleArray[position] = null;
                    addDeleteButtons[position].Text = "Add";
                    addDeleteButtons[position].Enabled = true;
                    firstNameLabels[position].Text = "";
                    lastNameLabels[position].Text = "";
                    firstNameTextBoxes[position].Text = "";
                    lastNameTextBoxes[position].Text = "";
                    firstNameTextBoxes[position].Visible = true;
                    lastNameTextBoxes[position].Visible = true;
                }
                catch (Exception e)
                {
                    this.HandleException(e, (position+1), "Delete");
                    calculateButtons[position].Enabled = true;
                    addDeleteButtons[position].Enabled = true;
                    editButtons[position].Enabled = true;
                }
                
            }
            else
            {
                if(this.validateInput(position))
                {
                    
                    firstNameTextBoxes[position].Enabled = false;
                    lastNameTextBoxes[position].Enabled = false;
                    addDeleteButtons[position].Enabled = false;
                    try
                    {
                        await dbConn.AddPerson(firstNameTextBoxes[position].Text, lastNameTextBoxes[position].Text);
                        personAdded = await dbConn.FindAddedPerson();
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
                    catch (Exception e)
                    {
                        this.HandleException(e, (position + 1), "Add");
                        firstNameTextBoxes[position].Enabled = true;
                        lastNameTextBoxes[position].Enabled = true;
                        addDeleteButtons[position].Enabled = true;
                    }

                    
                    

                }
            }
        }

        private async void EditSubmitButton1_Click(object sender, EventArgs e)
        {
            Task editSubmit1 = HandleEdit(0);
            activeTasks.Add(editSubmit1);
            await editSubmit1;
            activeTasks.Remove(editSubmit1);
        }

        private async void EditSubmitButton2_Click(object sender, EventArgs e)
        {
            Task editSubmit2 = HandleEdit(1);
            activeTasks.Add(editSubmit2);
            await editSubmit2;
            activeTasks.Remove(editSubmit2);
        }

        private async void EditSubmitButton3_Click(object sender, EventArgs e)
        {
            Task editSubmit3 = HandleEdit(2);
            activeTasks.Add(editSubmit3);
            await editSubmit3;
            activeTasks.Remove(editSubmit3);
        }

        private async void EditSubmitButton4_Click(object sender, EventArgs e)
        {
            Task editSubmit4 = HandleEdit(3);
            activeTasks.Add(editSubmit4);
            await editSubmit4;
            activeTasks.Remove(editSubmit4);
        }

        private async void EditSubmitButton5_Click(object sender, EventArgs e)
        {
            Task editSubmit5 = HandleEdit(4);
            activeTasks.Add(editSubmit5);
            await editSubmit5;
            activeTasks.Remove(editSubmit5);
        }

        private async void EditSubmitButton6_Click(object sender, EventArgs e)
        {
            Task editSubmit6 = HandleEdit(5);
            activeTasks.Add(editSubmit6);
            await editSubmit6;
            activeTasks.Remove(editSubmit6);
        }

        private async void EditSubmitButton7_Click(object sender, EventArgs e)
        {
            Task editSubmit7 = HandleEdit(6);
            activeTasks.Add(editSubmit7);
            await editSubmit7;
            activeTasks.Remove(editSubmit7);
        }

        private async void EditSubmitButton8_Click(object sender, EventArgs e)
        {
            Task editSubmit8 = HandleEdit(7);
            activeTasks.Add(editSubmit8);
            await editSubmit8;
            activeTasks.Remove(editSubmit8);
        }

        private async void EditSubmitButton9_Click(object sender, EventArgs e)
        {
            Task editSubmit9 = HandleEdit(8);
            activeTasks.Add(editSubmit9);
            await editSubmit9;
            activeTasks.Remove(editSubmit9);
        }

        private async void EditSubmitButton10_Click(object sender, EventArgs e)
        {
            Task editSubmit10 = HandleEdit(9);
            activeTasks.Add(editSubmit10);
            await editSubmit10;
            activeTasks.Remove(editSubmit10);
        }

        /// <summary>
        /// Task executed when a EditSubmitButton is clicked. Updates person info in the database after validation approval.
        /// </summary>
        /// <param name="position"></param>
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
                firstNameTextBoxes[position].Enabled = true;
                lastNameTextBoxes[position].Enabled = true;
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
                    try
                    {
                        await dbConn.UpdatePerson(peopleArray[position], firstNameTextBoxes[position].Text, lastNameTextBoxes[position].Text);
                        peopleArray[position].firstName = firstNameTextBoxes[position].Text;
                        peopleArray[position].lastName = lastNameTextBoxes[position].Text;
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
                        editButtons[position].Text = "Edit";
                    }
                    catch (Exception e)
                    {
                        this.HandleException(e, (position + 1), "Edit");
                        firstNameTextBoxes[position].Enabled = true;
                        lastNameTextBoxes[position].Enabled = true;
                        editButtons[position].Enabled = true;
                    }
                    
                }
                return;
            }
        }

     }
}
