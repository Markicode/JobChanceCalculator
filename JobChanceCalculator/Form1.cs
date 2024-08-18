namespace JobChanceCalculator
{
    public partial class Form1 : Form
    {
        DbConnection dbConn;
        List<Label> firstNameLabels;
        List<Label> lastNameLabels;
        List<Button> editButtons;
        List<Button> deleteButtons;
        List<Button> calculateButtons;
        List<Button> cancelButtons;
        List<TextBox> firstNameTextBoxes;
        List<TextBox> lastNameTextBoxes;
        List<Person> peopleList;
        Assignment assignment;

        public Form1()
        {
            InitializeComponent();
            this.dbConn = new DbConnection();
            this.assignment = new Assignment();
            assignment.Assigned += AddLogMessage;
            dbConn.PersonAdded += AddLogMessage;
            dbConn.PersonDeleted += AddLogMessage;
            dbConn.PersonUpdated += AddLogMessage;

            this.peopleList = new List<Person>();
            this.firstNameLabels = new List<Label>() {FirstNameLabel1, FirstNameLabel2, FirstNameLabel3, FirstNameLabel4, FirstNameLabel5, FirstNameLabel6,
            FirstNameLabel7, FirstNameLabel8, FirstNameLabel9, FirstNameLabel10};
            this.lastNameLabels = new List<Label>() {LastNameLabel1, LastNameLabel2, LastNameLabel3, LastNameLabel4, LastNameLabel5, LastNameLabel6,
            LastNameLabel7, LastNameLabel8, LastNameLabel9, LastNameLabel10};
            this.editButtons = new List<Button>() {EditButton1, EditButton2, EditButton3, EditButton4, EditButton5, EditButton6, EditButton7,
            EditButton8, EditButton9, EditButton10};
            this.deleteButtons = new List<Button>() { AddDeleteButton1, AddDeleteButton2, AddDeleteButton3, AddDeleteButton4, AddDeleteButton5, AddDeleteButton6,
            AddDeleteButton7, AddDeleteButton8, AddDeleteButton9, AddDeleteButton10};
            this.calculateButtons = new List<Button>() { CalculateButton1, CalculateButton2, CalculateButton3, CalculateButton4, CalculateButton5, CalculateButton6,
            CalculateButton7, CalculateButton8, CalculateButton9, CalculateButton10};
            this.cancelButtons = new List<Button>() { CancelButton1, CancelButton2, CancelButton3, CancelButton4, CancelButton5, CancelButton6,
            CancelButton7, CancelButton8, CancelButton9, CancelButton10};
            this.firstNameTextBoxes = new List<TextBox>() { FirstNameTextBox1, FirstNameTextBox2, FirstNameTextBox3, FirstNameTextBox4, FirstNameTextBox5,
            FirstNameTextBox6, FirstNameTextBox7, FirstNameTextBox8, FirstNameTextBox9, FirstNameTextBox10};
            this.lastNameTextBoxes = new List<TextBox>() { LastNameTextBox1, LastNameTextBox2, LastNameTextBox3, LastNameTextBox4, LastNameTextBox5,
            LastNameTextBox6, LastNameTextBox7, LastNameTextBox8, LastNameTextBox9, LastNameTextBox10};

            for (int i = 0; i < 10; i++)
            {
                firstNameLabels[i].Text = "";
                lastNameLabels[i].Text = "";
            }


        }

        private async void ButtonStart_Click(object sender, EventArgs e)
        {
            await this.Initialize();
            ButtonStart.Enabled = false;
        }

        private void AddLogMessage(string message)
        {
            LogTextBox.AppendText($"{DateTime.Now} - {message} \r\n");
        }

        private async Task Initialize()
        {
            AddLogMessage("Initialization started.");
            await dbConn.SetupDb();
            await dbConn.FillDb();
            peopleList = await assignment.AssignPeopleAsync();

            for (int i = 0; i < peopleList.Count; i++)
            {
                firstNameLabels[i].Text = peopleList[i].firstName;
                lastNameLabels[i].Text = peopleList[i].lastName;
                editButtons[i].Enabled = true;
                deleteButtons[i].Enabled = true;
                calculateButtons[i].Enabled = true;
            }
            for (int i = peopleList.Count; i < 10; i++)
            {
                deleteButtons[i].Text = "Add";
                deleteButtons[i].Enabled = true;
                firstNameTextBoxes[i].Visible = true;
                lastNameTextBoxes[i].Visible = true;
            }
        }

        private async Task ReAssign()
        {
            for (int i = 0; i < 10; i++)
            {
                firstNameLabels[i].Text = "";
                lastNameLabels[i].Text = "";
                editButtons[i].Enabled = false;
                deleteButtons[i].Enabled = false;
                calculateButtons[i].Enabled = false;
                firstNameTextBoxes[i].Text = "";
                lastNameTextBoxes[i].Text = "";
            }

            peopleList = await assignment.AssignPeopleAsync();

            for (int i = 0; i < peopleList.Count; i++)
            {
                firstNameLabels[i].Text = peopleList[i].firstName;
                lastNameLabels[i].Text = peopleList[i].lastName;
                editButtons[i].Enabled = true;
                deleteButtons[i].Enabled = true;
                deleteButtons[i].Text = "Delete";
                calculateButtons[i].Enabled = true;
                firstNameTextBoxes[i].Visible = false;
                lastNameTextBoxes[i].Visible = false;
            }
            for (int i = peopleList.Count; i < 10; i++)
            {
                deleteButtons[i].Text = "Add";
                deleteButtons[i].Enabled = true;
                firstNameTextBoxes[i].Visible = true;
                lastNameTextBoxes[i].Visible = true;
            }
        }

        private bool validateInput(int textBoxNumber)
        {
            string firstName = firstNameTextBoxes[textBoxNumber - 1].Text;
            string lastName = lastNameTextBoxes[textBoxNumber - 1].Text;
            if (firstName == "" || lastName == "")
            {
                MessageBox.Show("Enter a first and a last name.");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
