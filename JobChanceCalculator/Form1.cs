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
        List<Person> peopleList;
        Assignment assignment;

        public Form1()
        {
            InitializeComponent();
            this.dbConn = new DbConnection();
            this.assignment = new Assignment();
            assignment.Assigned += AddLogMessage;

            this.firstNameLabels = new List<Label>() {FirstNameLabel1, FirstNameLabel2, FirstNameLabel3, FirstNameLabel4, FirstNameLabel5, FirstNameLabel6,
            FirstNameLabel7, FirstNameLabel8, FirstNameLabel9, FirstNameLabel10};
            this.lastNameLabels = new List<Label>() {LastNameLabel1, LastNameLabel2, LastNameLabel3, LastNameLabel4, LastNameLabel5, LastNameLabel6,
            LastNameLabel7, LastNameLabel8, LastNameLabel9, LastNameLabel10};
            this.editButtons = new List<Button>() {EditButton1, EditButton2, EditButton3, EditButton4, EditButton5, EditButton6, EditButton7,
            EditButton8, EditButton9, EditButton10};
            this.deleteButtons = new List<Button>() { DeleteButton1, DeleteButton2, DeleteButton3, DeleteButton4, DeleteButton5, DeleteButton6,
            DeleteButton7, DeleteButton8, DeleteButton9, DeleteButton10};
            this.calculateButtons = new List<Button>() { CalculateButton1, CalculateButton2, CalculateButton3, CalculateButton4, CalculateButton5, CalculateButton6,
            CalculateButton7, CalculateButton8, CalculateButton9, CalculateButton10};
            this.cancelButtons = new List<Button>() { CancelButton1, CancelButton2, CancelButton3, CancelButton4, CancelButton5, CancelButton6,
            CancelButton7, CancelButton8, CancelButton9, CancelButton10};

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
            LogTextBox.AppendText($"{message} \r\n");
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
        }

        private async Task ReAssign()
        {
            for(int i = 0; i < 10; i++)
            {
                firstNameLabels[i].Text = "";
                lastNameLabels[i].Text = "";
                editButtons[i].Enabled = false;
                deleteButtons[i].Enabled = false;
                calculateButtons[i].Enabled = false;
            }

            peopleList = await assignment.AssignPeopleAsync();

            for (int i = 0; i < peopleList.Count; i++)
            {
                firstNameLabels[i].Text = peopleList[i].firstName;
                lastNameLabels[i].Text = peopleList[i].lastName;
                editButtons[i].Enabled = true;
                deleteButtons[i].Enabled = true;
                calculateButtons[i].Enabled = true;
            }
        }

        
    }
}
