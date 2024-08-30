using Microsoft.Data.Sqlite;

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
        List<ProgressBar> progressBars;
        List<Person> peopleList;
        List<Progress<int>> progresses;
        List<CancellationTokenSource> cancelTokenSources;
        List<CancellationToken> cancelTokens;
        Person?[] peopleArray;
        Assignment assignment;
        int mainProgress;

        public Form1()
        {
            InitializeComponent();
            this.dbConn = new DbConnection();
            this.assignment = new Assignment();

            assignment.Assigned += AddLogMessage;
            dbConn.PersonAdded += AddLogMessage;
            dbConn.PersonDeleted += AddLogMessage;
            dbConn.PersonUpdated += AddLogMessage;
            dbConn.PeopleDeleted += AddLogMessage;

            this.peopleList = new List<Person>();
            this.peopleArray = new Person?[10];

            Progress<int> progress1 = new Progress<int>(percent =>
            {
                ProgressBar1.Value = percent;
            });
            Progress<int> progress2 = new Progress<int>(percent =>
            {
                ProgressBar2.Value = percent;
            });
            Progress<int> progress3 = new Progress<int>(percent =>
            {
                ProgressBar3.Value = percent;
            });
            Progress<int> progress4 = new Progress<int>(percent =>
            {
                ProgressBar4.Value = percent;
            });
            Progress<int> progress5 = new Progress<int>(percent =>
            {
                ProgressBar5.Value = percent;
            });
            Progress<int> progress6 = new Progress<int>(percent =>
            {
                ProgressBar6.Value = percent;
            });
            Progress<int> progress7 = new Progress<int>(percent =>
            {
                ProgressBar7.Value = percent;
            });
            Progress<int> progress8 = new Progress<int>(percent =>
            {
                ProgressBar8.Value = percent;
            });
            Progress<int> progress9 = new Progress<int>(percent =>
            {
                ProgressBar9.Value = percent;
            });
            Progress<int> progress10 = new Progress<int>(percent =>
            {
                ProgressBar10.Value = percent;
            });
            Progress<int> progressMain = new Progress<int>(percent =>
            {
                MainProgressBar.Value = percent;
            });



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
            this.progressBars = new List<ProgressBar>() { ProgressBar1, ProgressBar2, ProgressBar3, ProgressBar4, ProgressBar5, ProgressBar6, ProgressBar7,
            ProgressBar8, ProgressBar9, ProgressBar10};
            this.progresses = new List<Progress<int>>() { progress1, progress2, progress3, progress4, progress5, progress6, progress7,
            progress8, progress9, progress10, progressMain};

            this.mainProgress = 0;
            this.taskCanceled += AddLogMessage;
            this.exceptionOccured += AddLogMessage;

            for (int i = 0; i < 10; i++)
            {
                firstNameLabels[i].Text = "";
                lastNameLabels[i].Text = "";
                graduationLabels[i].Text = "";
                jobLabels[i].Text = "";
                factorLabels[i].Text = "";
            }
            MainProgressBar.Maximum = 0;

        }

        public delegate void taskCanceledDelegate(string message);
        public delegate void exceptionOccuredDelegate(string message);

        public event taskCanceledDelegate taskCanceled;
        public event exceptionOccuredDelegate exceptionOccured;

        private async void ButtonStart_Click(object sender, EventArgs e)
        {
            try
            {
                await this.Initialize();
            }
            catch (Exception ex)
            {
                this.HandleException(ex, 0, "Initialize");
            }
            ButtonStart.Enabled = false;
        }

        private void AddLogMessage(string message)
        {
            if (LogTextBox.InvokeRequired)
            {
                LogTextBox.Invoke(new Action<string>(AddLogMessage), new object[] { message });
                return;
            }
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
                firstNameTextBoxes[i].Visible = false;
                lastNameTextBoxes[i].Visible = false;
                addDeleteButtons[i].Text = "Delete";
            }
            for (int i = peopleList.Count; i < 10; i++)
            {
                addDeleteButtons[i].Text = "Add";
                addDeleteButtons[i].Enabled = true;
                firstNameTextBoxes[i].Visible = true;
                lastNameTextBoxes[i].Visible = true;
                calculateButtons[i].Enabled = false;
            }
            CancellationTokenSource cts1 = new CancellationTokenSource();
            CancellationTokenSource cts2 = new CancellationTokenSource();
            CancellationTokenSource cts3 = new CancellationTokenSource();
            CancellationTokenSource cts4 = new CancellationTokenSource();
            CancellationTokenSource cts5 = new CancellationTokenSource();
            CancellationTokenSource cts6 = new CancellationTokenSource();
            CancellationTokenSource cts7 = new CancellationTokenSource();
            CancellationTokenSource cts8 = new CancellationTokenSource();
            CancellationTokenSource cts9 = new CancellationTokenSource();
            CancellationTokenSource cts10 = new CancellationTokenSource();

            CancellationToken cancelToken1 = cts1.Token;
            CancellationToken cancelToken2 = cts2.Token;
            CancellationToken cancelToken3 = cts3.Token;
            CancellationToken cancelToken4 = cts4.Token;
            CancellationToken cancelToken5 = cts5.Token;
            CancellationToken cancelToken6 = cts6.Token;
            CancellationToken cancelToken7 = cts7.Token;
            CancellationToken cancelToken8 = cts8.Token;
            CancellationToken cancelToken9 = cts9.Token;
            CancellationToken cancelToken10 = cts10.Token;

            cancelTokenSources = new List<CancellationTokenSource>() { cts1, cts2, cts3, cts4, cts5, cts6, cts7, cts8, cts9, cts10 };
            cancelTokens = new List<CancellationToken>() { cancelToken1, cancelToken2, cancelToken3, cancelToken4, cancelToken5, cancelToken6, cancelToken7,
            cancelToken8, cancelToken9, cancelToken10};

            RebuildButton.Enabled = true;
        }



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

            // TODO: implement regex for user input
        }

        private async void CalculateButton1_Click(object sender, EventArgs e)
        {
            try
            {
                await HandleCalculation(0);
            }
            catch (Exception ex)
            {
                this.HandleException(ex, 1, "Calculate");
            }

        }

        private async void CalculateButton2_Click(object sender, EventArgs e)
        {
            try
            {
                await HandleCalculation(1);
            }
            catch (Exception ex)
            {
                this.HandleException(ex, 2, "Calculate");
            }
        }

        private async void CalculateButton3_Click(object sender, EventArgs e)
        {
            try
            {
                await HandleCalculation(2);
            }
            catch (Exception ex)
            {
                this.HandleException(ex, 3, "Calculate");
            }
        }

        private async void CalculateButton4_Click(object sender, EventArgs e)
        {
            try
            {
                await HandleCalculation(3);
            }
            catch (Exception ex)
            {
                this.HandleException(ex, 4, "Calculate");
            }
        }

        private async void CalculateButton5_Click(object sender, EventArgs e)
        {
            try
            {
                await HandleCalculation(4);
            }
            catch (Exception ex)
            {
                this.HandleException(ex, 5, "Calculate");
            }
        }

        private async void CalculateButton6_Click(object sender, EventArgs e)
        {
            try
            {
                await HandleCalculation(5);
            }
            catch (Exception ex)
            {
                this.HandleException(ex, 6, "Calculate");
            }
        }

        private async void CalculateButton7_Click(object sender, EventArgs e)
        {
            try
            {
                await HandleCalculation(6);
            }
            catch (Exception ex)
            {
                this.HandleException(ex, 7, "Calculate");
            }
        }

        private async void CalculateButton8_Click(object sender, EventArgs e)
        {
            try
            {
                await HandleCalculation(7);
            }
            catch (Exception ex)
            {
                this.HandleException(ex, 8, "Calculate");
            }
        }

        private async void CalculateButton9_Click(object sender, EventArgs e)
        {
            try
            {
                await HandleCalculation(8);
            }
            catch (Exception ex)
            {
                this.HandleException(ex, 9, "Calculate");
            }
        }

        private async void CalculateButton10_Click(object sender, EventArgs e)
        {
            try
            {
                await HandleCalculation(9);
            }
            catch (Exception ex)
            {
                this.HandleException(ex, 10, "Calculate");
            }
        }


        private async Task HandleCalculation(int position)
        {
            MainProgressBar.Maximum += 10000;
            graduationLabels[position].Text = "";
            jobLabels[position].Text = "";
            factorLabels[position].Text = "";
            calculateButtons[position].Enabled = false;
            cancelButtons[position].Enabled = true;
            addDeleteButtons[position].Enabled = false;
            editButtons[position].Enabled = false;

            progressBars[position].Value = 0;
            progressBars[position].Maximum = 10000;

            Person selectedPerson = peopleArray[position];
            selectedPerson.factor = 0;
            selectedPerson.graduationChanceCalculated += AddLogMessage;
            selectedPerson.jobChanceCalculated += AddLogMessage;
            selectedPerson.calculationCompleted += AddLogMessage;
            selectedPerson.mainProgressUpdated += updateMainProgressBar;

            Task calculationTask = Task.Run(() => selectedPerson.CalculateChances(progresses[position], progresses[10], cancelTokens[position]));
            try
            {
                await calculationTask;
            }
            catch (OperationCanceledException oce)
            {
                this.taskCanceled($"{selectedPerson.firstName} {selectedPerson.lastName}: Calculations canceled.");
            }

            if (!cancelTokens[position].IsCancellationRequested)
            {
                if (progressBars[position].Value == progressBars[position].Maximum)
                {
                    mainProgress -= 10000;
                    progressBars[position].Maximum += 1;
                    MainProgressBar.Maximum += 1;
                    progressBars[position].Value += 1;
                    MainProgressBar.Value += 1;
                    progressBars[position].Value -= 1;
                    MainProgressBar.Value -= 1;
                    progressBars[position].Maximum -= 1;
                    MainProgressBar.Maximum -= 1;
                    MainProgressBar.Value -= 10000;
                    MainProgressBar.Maximum -= 10000;
                }
                graduationLabels[position].Text = $"{selectedPerson.graduate} ({(selectedPerson.graduateChance * 100).ToString("##.#")} %)";
                jobLabels[position].Text = $"{selectedPerson.job} ({(selectedPerson.jobChance * 100).ToString("##.#")} %)";
                factorLabels[position].Text = $"{(selectedPerson.factor * 100).ToString("##.#")} %";
                Task sleep = Task.Run(() => Thread.Sleep(100));
                await sleep;
                progressBars[position].Value = 0;
                calculateButtons[position].Enabled = true;
                cancelButtons[position].Enabled = false;
                addDeleteButtons[position].Enabled = true;
                editButtons[position].Enabled = true;
            }
            else
            {
                MainProgressBar.Maximum -= 10000;
                graduationLabels[position].Text = "";
                jobLabels[position].Text = "";
                factorLabels[position].Text = "";
                if (selectedPerson.cancelOrigin == "CancelButton")
                {
                    calculateButtons[position].Enabled = true;
                    cancelButtons[position].Enabled = false;
                    addDeleteButtons[position].Enabled = true;
                    editButtons[position].Enabled = true;
                    cancelTokenSources[position].Dispose();
                    cancelTokenSources[position] = new CancellationTokenSource();
                    cancelTokens[position] = cancelTokenSources[position].Token;
                }
                if (selectedPerson.cancelOrigin == "RebuildButton")
                {
                    calculateButtons[position].Enabled = false;
                    cancelButtons[position].Enabled = false;
                    addDeleteButtons[position].Enabled = false;
                    editButtons[position].Enabled = false;
                }
            }
            
            selectedPerson.graduationChanceCalculated -= AddLogMessage;
            selectedPerson.jobChanceCalculated -= AddLogMessage;
            selectedPerson.calculationCompleted -= AddLogMessage;
            selectedPerson.mainProgressUpdated -= updateMainProgressBar;


        }

        private void updateMainProgressBar(int progressCount, IProgress<int> progress)
        {
            this.mainProgress += progressCount;
            lock (MainProgressBar)
            {
                if (progress != null)
                {
                    progress.Report(this.mainProgress);
                }
            }
        }

        private async void CancelButton1_Click(object sender, EventArgs e)
        {
            await this.HandleCancellation(0);
        }

        private async void CancelButton2_Click(object sender, EventArgs e)
        {
            await this.HandleCancellation(1);
        }

        private async void CancelButton3_Click(object sender, EventArgs e)
        {
            await this.HandleCancellation(2);
        }

        private async void CancelButton4_Click(object sender, EventArgs e)
        {
            await this.HandleCancellation(3);
        }

        private async void CancelButton5_Click(object sender, EventArgs e)
        {
            await this.HandleCancellation(4);
        }

        private async void CancelButton6_Click(object sender, EventArgs e)
        {
            await this.HandleCancellation(5);
        }

        private async void CancelButton7_Click(object sender, EventArgs e)
        {
            await this.HandleCancellation(6);
        }

        private async void CancelButton8_Click(object sender, EventArgs e)
        {
            await this.HandleCancellation(7);
        }

        private async void CancelButton9_Click(object sender, EventArgs e)
        {
            await this.HandleCancellation(8);
        }

        private async void CancelButton10_Click(object sender, EventArgs e)
        {
            await this.HandleCancellation(9);
        }

        private async Task HandleCancellation(int position)
        {
            Person selectedPerson = peopleArray[position];
            if (selectedPerson != null)
            {
                Task cancelTask = Task.Run(() => selectedPerson.CancelCalculation("CancelButton", cancelTokenSources[position]));
                await cancelTask;
            }
        }

        private async void RebuildButton_Click(object sender, EventArgs e)
        {
            RebuildButton.Enabled = false;
            for (int i = 0; i < 10; i++)
            {
                if (peopleArray[i] != null)
                {
                    peopleArray[i].CancelCalculation("RebuildButton", cancelTokenSources[i]);
                }
            }

            for (int i = 0; i < 10; i++)
            {
                editButtons[i].Enabled = false;
                addDeleteButtons[i].Enabled = false;
                calculateButtons[i].Enabled = false;
                cancelButtons[i].Enabled = false;
                firstNameTextBoxes[i].Visible = false;
                lastNameTextBoxes[i].Visible = false;
                firstNameTextBoxes[i].Text = "";
                lastNameTextBoxes[i].Text = "";
            }
            try
            {
                await dbConn.DeleteAll();
                await this.Initialize();
            }
            catch (Exception ex)
            {
                this.HandleException(ex, 0, "rebuild");
            }
        }

        private void HandleException(Exception e, int position, string operation)
        {
            if (e is SqliteException)
            {
                if (position == 0)
                {
                    MessageBox.Show($"SQL Error found on: Main during operation \"{operation}\".");
                    this.exceptionOccured(e.Message);
                }
                else
                {
                    MessageBox.Show($"SQL Error found on position {position.ToString()} during operation \"{operation}\".");
                    this.exceptionOccured(e.Message);
                }
            }
            if (e is IndexOutOfRangeException)
            {
                MessageBox.Show("Index out of range.");
            }

        }

        private void GraduationHeaderLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}
