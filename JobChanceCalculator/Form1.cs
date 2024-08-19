namespace JobChanceCalculator
{
    public partial class Form1 : Form
    {
        DbConnection dbConn;
        List<Label> firstNameLabels;
        List<Label> lastNameLabels;
        List<Label> graduationLabels;
        List<Label> jobLabels;
        List<Label> factorLabels;
        List<Button> editButtons;
        List<Button> addDeleteButtons;
        List<Button> calculateButtons;
        List<Button> cancelButtons;
        List<TextBox> firstNameTextBoxes;
        List<TextBox> lastNameTextBoxes;
        List<Person> peopleList;
        Person?[] peopleArray;
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
            this.peopleArray = new Person?[10];

            this.firstNameLabels = new List<Label>() {FirstNameLabel1, FirstNameLabel2, FirstNameLabel3, FirstNameLabel4, FirstNameLabel5, FirstNameLabel6,
            FirstNameLabel7, FirstNameLabel8, FirstNameLabel9, FirstNameLabel10};
            this.lastNameLabels = new List<Label>() {LastNameLabel1, LastNameLabel2, LastNameLabel3, LastNameLabel4, LastNameLabel5, LastNameLabel6,
            LastNameLabel7, LastNameLabel8, LastNameLabel9, LastNameLabel10};
            this.graduationLabels = new List<Label>() { GraduationLabel1, GraduationLabel2, GraduationLabel3, GraduationLabel4, GraduationLabel5, GraduationLabel6,
            GraduationLabel7, GraduationLabel8, GraduationLabel9, GraduationLabel10 };
            this.jobLabels = new List<Label>() { JobLabel1, JobLabel2, JobLabel3, JobLabel4, JobLabel5, JobLabel6, JobLabel7, JobLabel8, JobLabel9, JobLabel10 };
            this.factorLabels = new List<Label> { FactorLabel1, FactorLabel2, FactorLabel3, FactorLabel4, FactorLabel5, FactorLabel6, FactorLabel7, FactorLabel8,
            FactorLabel9, FactorLabel10};
            this.editButtons = new List<Button>() {EditSubmitButton1, EditSubmitButton2, EditSubmitButton3, EditSubmitButton4, EditSubmitButton5, EditSubmitButton6, EditSubmitButton7,
            EditSubmitButton8, EditSubmitButton9, EditSubmitButton10};
            this.addDeleteButtons = new List<Button>() { AddDeleteButton1, AddDeleteButton2, AddDeleteButton3, AddDeleteButton4, AddDeleteButton5, AddDeleteButton6,
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
                graduationLabels[i].Text = "";
                jobLabels[i].Text = "";
                factorLabels[i].Text = "";
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
                peopleArray[i] = peopleList[i];
                firstNameLabels[i].Text = peopleList[i].firstName;
                lastNameLabels[i].Text = peopleList[i].lastName;
                editButtons[i].Enabled = true;
                addDeleteButtons[i].Enabled = true;
                calculateButtons[i].Enabled = true;
            }
            for (int i = peopleList.Count; i < 10; i++)
            {
                addDeleteButtons[i].Text = "Add";
                addDeleteButtons[i].Enabled = true;
                firstNameTextBoxes[i].Visible = true;
                lastNameTextBoxes[i].Visible = true;
                calculateButtons[i].Enabled = false;
            }
        }

        /**private async Task ReAssign()
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
        }**/

        private bool validateInput(int textBoxNumber)
        {
            string firstName = firstNameTextBoxes[textBoxNumber].Text;
            string lastName = lastNameTextBoxes[textBoxNumber].Text;
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

        private async void CalculateButton1_Click(object sender, EventArgs e)
        {
            await HandleCalculation(0);
        }

        private async void CalculateButton2_Click(object sender, EventArgs e)
        {
            await HandleCalculation(1);
        }

        private async void CalculateButton3_Click(object sender, EventArgs e)
        {
            await HandleCalculation(2);
        }

        private async void CalculateButton4_Click(object sender, EventArgs e)
        {
            await HandleCalculation(3);
        }

        private async void CalculateButton5_Click(object sender, EventArgs e)
        {
            await HandleCalculation(4);
        }

        private async void CalculateButton6_Click(object sender, EventArgs e)
        {
            await HandleCalculation(5);
        }

        private async void CalculateButton7_Click(object sender, EventArgs e)
        {
            await HandleCalculation(6);
        }

        private async void CalculateButton8_Click(object sender, EventArgs e)
        {
            await HandleCalculation(7);
        }

        private async void CalculateButton9_Click(object sender, EventArgs e)
        {
            await HandleCalculation(8);
        }

        private async void CalculateButton10_Click(object sender, EventArgs e)
        {
            await HandleCalculation(9);
        }


        private async Task HandleCalculation(int position)
        {
            Person selectedPerson = peopleArray[position];
            Task calculationTask = Task.Run(() => selectedPerson.CalculateChances());
            await calculationTask;
            graduationLabels[position].Text = $"{selectedPerson.graduate} ({(selectedPerson.graduateChance * 100).ToString()} %))";
            jobLabels[position].Text = $"{selectedPerson.job} ({(selectedPerson.jobChance * 100).ToString()} %))";
            factorLabels[position].Text = $"{(selectedPerson.factor * 100).ToString()} %";
        }
    }
}
