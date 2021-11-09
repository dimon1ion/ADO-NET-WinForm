using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ADO_NET_WinForm
{
    public partial class Form2 : Form
    {
        Form1 form1;
        string connectionString;
        SqlDataAdapter adapterUpdate;
        SqlConnection connection;
        SqlCommandBuilder builderUpdate;
        public Form2(Form1 form1)
        {
            this.form1 = form1;
            connectionString = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;
            connection = new SqlConnection(connectionString);
            InitializeComponent();
            UpdateComboBox(comboBox1, "SELECT TypeStationery.Type FROM TypeStationery");
            UpdateComboBox(comboBox2, "SELECT Stationery.Name FROM Stationery");
            UpdateComboBox(comboBox3, "SELECT Managers.Name FROM Managers");
            UpdateComboBox(comboBox4, "SELECT TypeStationery.Type FROM TypeStationery");
            UpdateComboBox(comboBox5, "SELECT Customers.Name FROM Customers");
        }

        private void UpdateComboBox(ComboBox comboBox, string query)
        {
            DataTable table = new DataTable();
            comboBox.Items.Clear();
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            {
                adapter.Fill(table);
            }
            foreach (DataRow column in table.Rows)
            {
                comboBox.Items.Add(column.ItemArray[0]);
            }
            if (comboBox.Items.Count > 0)
            {
                comboBox.SelectedItem = comboBox.Items[0];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != String.Empty && textBox2.Text != String.Empty && comboBox1.SelectedItem.ToString() != String.Empty && textBox3.Text != String.Empty)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        SqlCommand command = new SqlCommand("Task2_1", connection);
                        command.CommandType = CommandType.StoredProcedure;

                        SqlParameter paramName = new SqlParameter("@nameOfStationary", textBox1.Text);

                        SqlParameter paramCost = new SqlParameter("@costPrice", SqlDbType.Money);
                        paramCost.Value = textBox2.Text;

                        SqlParameter paramType = new SqlParameter("@type", comboBox1.SelectedItem.ToString());

                        SqlParameter paramQuantity = new SqlParameter("@quantity", SqlDbType.Int);
                        paramQuantity.Value = textBox3.Text;

                        command.Parameters.Add(paramName);
                        command.Parameters.Add(paramCost);
                        command.Parameters.Add(paramType);
                        command.Parameters.Add(paramQuantity);
                        connection.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Insert was successful", "Complete", MessageBoxButtons.OK);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Insert was not successful", "Error", MessageBoxButtons.OK);
                    }
                }
            }
            else
            {
                MessageBox.Show("Enter data", "Warning", MessageBoxButtons.OK);
            }
            UpdateComboBox(comboBox1, "SELECT TypeStationery.Type FROM TypeStationery");
            UpdateComboBox(comboBox2, "SELECT Stationery.Name FROM Stationery");
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            form1.buttonMoreFunctions.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != String.Empty)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        SqlCommand command = new SqlCommand("Task2_2", connection);
                        command.CommandType = CommandType.StoredProcedure;

                        SqlParameter paramType = new SqlParameter("@type", SqlDbType.NVarChar);
                        paramType.Value = textBox4.Text;

                        command.Parameters.Add(paramType);

                        connection.Open();

                        command.ExecuteNonQuery();

                        MessageBox.Show("Insert was successful", "Complete", MessageBoxButtons.OK);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Insert was not successful", "Error", MessageBoxButtons.OK);
                    }
                }
            }
            else
            {
                MessageBox.Show("Enter data", "Warning", MessageBoxButtons.OK);
            }
            UpdateComboBox(comboBox1, "SELECT TypeStationery.Type FROM TypeStationery");
            UpdateComboBox(comboBox4, "SELECT TypeStationery.Type FROM TypeStationery");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox5.Text != String.Empty)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        SqlCommand command = new SqlCommand("Task2_3", connection);
                        command.CommandType = CommandType.StoredProcedure;

                        SqlParameter paramName = new SqlParameter("@name", SqlDbType.NVarChar);
                        paramName.Value = textBox5.Text;

                        command.Parameters.Add(paramName);

                        connection.Open();

                        command.ExecuteNonQuery();

                        MessageBox.Show("Insert was successful", "Complete", MessageBoxButtons.OK);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Insert was not successful", "Error", MessageBoxButtons.OK);
                    }
                }
            }
            else
            {
                MessageBox.Show("Enter data", "Warning", MessageBoxButtons.OK);
            }
            UpdateComboBox(comboBox3, "SELECT Managers.Name FROM Managers");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox6.Text != String.Empty)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        SqlCommand command = new SqlCommand("Task2_4", connection);
                        command.CommandType = CommandType.StoredProcedure;

                        SqlParameter paramName = new SqlParameter("@nameCompany", SqlDbType.NVarChar);
                        paramName.Value = textBox6.Text;

                        command.Parameters.Add(paramName);

                        connection.Open();

                        command.ExecuteNonQuery();

                        MessageBox.Show("Insert was successful", "Complete", MessageBoxButtons.OK);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Insert was not successful", "Error", MessageBoxButtons.OK);
                    }
                }
            }
            else
            {
                MessageBox.Show("Enter data", "Warning", MessageBoxButtons.OK);
            }
            UpdateComboBox(comboBox5, "SELECT Customers.Name FROM Customers");
        }

        private void InitForUpdateAndShow(string query)
        {
            form1.data = new DataTable();

            SqlCommand command = new SqlCommand(query, connection);

            adapterUpdate = new SqlDataAdapter(command);

            builderUpdate = new SqlCommandBuilder(adapterUpdate);

            adapterUpdate.Fill(form1.data);

            form1.dataGridView1.DataSource = form1.data;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            switch ((sender as Button).Name)
            {
                case "button5":
                    InitForUpdateAndShow("Select * FROM Stationery");
                    break;
                case "button8":
                    InitForUpdateAndShow("Select * FROM Customers");
                    break;
                case "button10":
                    InitForUpdateAndShow("Select * FROM Managers");
                    break;
                case "button12":
                    InitForUpdateAndShow("Select * FROM TypeStationery");
                    break;
                default:
                    break;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                adapterUpdate.Update(form1.data);
                MessageBox.Show("Update completed!", "Info", MessageBoxButtons.OK);
            }
            catch (Exception)
            {
                MessageBox.Show("Update failed", "Info", MessageBoxButtons.OK);
            }
        }

        private void DeleteWithOneParam(string execName, string paramVariable, string value)
        {
            if (value != String.Empty)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        SqlCommand command = new SqlCommand(execName, connection);
                        command.CommandType = CommandType.StoredProcedure;

                        SqlParameter paramName = new SqlParameter(paramVariable, SqlDbType.NVarChar);
                        paramName.Value = value;

                        command.Parameters.Add(paramName);

                        connection.Open();

                        command.ExecuteNonQuery();

                        MessageBox.Show("Delete was successful", "Complete", MessageBoxButtons.OK);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Delete was not successful", "Error", MessageBoxButtons.OK);
                    }
                }
            }
            else
            {
                MessageBox.Show("Enter data", "Warning", MessageBoxButtons.OK);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            switch ((sender as Button).Name)
            {
                case "button13":
                    DeleteWithOneParam("Task2_8", "@nameStationary", comboBox2.SelectedItem.ToString());
                    UpdateComboBox(comboBox2, "SELECT Stationery.Name FROM Stationery");
                    break;
                case "button14":
                    DeleteWithOneParam("Task2_9", "@nameManager", comboBox3.SelectedItem.ToString());
                    UpdateComboBox(comboBox3, "SELECT Managers.Name FROM Managers");
                    break;
                case "button15":
                    DeleteWithOneParam("Task2_10", "@nameType", comboBox4.SelectedItem.ToString());
                    UpdateComboBox(comboBox4, "SELECT TypeStationery.Type FROM TypeStationery");
                    break;
                case "button16":
                    DeleteWithOneParam("Task2_11", "@nameCustomer", comboBox5.SelectedItem.ToString());
                    UpdateComboBox(comboBox5, "SELECT Customers.Name FROM Customers");
                    break;
                default:
                    break;
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            switch ((sender as Button).Text)
            {
                case "Task12":
                    form1.ShowTable("SELECT TOP 1 Managers.Name, SUM(Quantity)[CountOfSales] FROM Managers, Sales " +
                        "WHERE Sales.ManagerId = Managers.Id " +
                        "GROUP BY Managers.Name " +
                        "ORDER BY SUM(Quantity) DESC");
                    break;
                case "Task13":
                    form1.ShowTable("SELECT TOP 1 Managers.Name, SUM(Sales.Price_of_one * Sales.Quantity)[SalaryOfSales] FROM Managers, Sales " +
                        "WHERE Sales.ManagerId = Managers.Id " +
                        "GROUP BY Managers.Name " +
                        "ORDER BY SUM(Sales.Price_of_one * Sales.Quantity) DESC");
                    break;
                case "Task14":
                    {
                        string minDate;
                        string maxDate;
                        if (dateTimePicker1.Value > dateTimePicker2.Value)
                        {
                            maxDate = $"{dateTimePicker1.Value.Year}-{dateTimePicker1.Value.Month}-{dateTimePicker1.Value.Day}";
                            minDate = $"{dateTimePicker2.Value.Year}-{dateTimePicker2.Value.Month}-{dateTimePicker2.Value.Day}";
                        }
                        else
                        {
                            maxDate = $"{dateTimePicker2.Value.Year}-{dateTimePicker2.Value.Month}-{dateTimePicker2.Value.Day}";
                            minDate = $"{dateTimePicker1.Value.Year}-{dateTimePicker1.Value.Month}-{dateTimePicker1.Value.Day}";
                        }
                        form1.ShowTable("SELECT TOP 1 Managers.Name, SUM(Sales.Price_of_one * Sales.Quantity)[SalaryOfSales] FROM Managers, Sales " +
                            $"WHERE Sales.ManagerId = Managers.Id AND Sales.Date_of_sale > '{minDate}' AND Sales.Date_of_sale < '{maxDate}' " +
                            "GROUP BY Managers.Name " +
                            "ORDER BY SUM(Sales.Price_of_one * Sales.Quantity) DESC");
                    }
                    break;
                case "Task15":
                    form1.ShowTable("SELECT TOP 1 Customers.Name, SUM(Sales.Price_of_one * Sales.Quantity)[SalaryOfSales] FROM Customers, Sales " +
                        "WHERE Sales.CustomerId = Customers.Id " +
                        "GROUP BY Customers.Name " +
                        "ORDER BY SUM(Sales.Price_of_one * Sales.Quantity) DESC");
                    break;
                case "Task16":
                    form1.ShowTable("SELECT TOP 1 TypeStationery.Type, SUM(TypeStationery.Id)[CountOfSales] FROM TypeStationery, Stationery, Sales " +
                        "WHERE Stationery.TypeStationeryId = TypeStationery.Id AND Sales.StationaryId = Stationery.Id " +
                        "GROUP BY TypeStationery.Type " +
                        "ORDER BY SUM(TypeStationery.Id) DESC");
                    break;
                case "Task17":
                    form1.ShowTable("SELECT TOP 1 TypeStationery.Type, SUM(Sales.Price_of_one * Sales.Quantity)[SalaryOfSales] FROM TypeStationery, Stationery, Sales " +
                        "WHERE Stationery.TypeStationeryId = TypeStationery.Id AND Sales.StationaryId = Stationery.Id " +
                        "GROUP BY TypeStationery.Type " +
                        "ORDER BY SUM(Sales.Price_of_one * Sales.Quantity) DESC");
                    break;
                case "Task18":
                    form1.ShowTable("SELECT TypeStationery.Type, SUM(TypeStationery.Id)[CountOfSales] FROM TypeStationery, Stationery, Sales " +
                        "WHERE Stationery.TypeStationeryId = TypeStationery.Id AND Sales.StationaryId = Stationery.Id " +
                        "GROUP BY TypeStationery.Type " +
                        "ORDER BY SUM(TypeStationery.Id) DESC");
                    break;
                case "Task19":
                    form1.ShowTable("SELECT Stationery.Name, MAX(Sales.Date_of_sale) FROM Stationery, Sales " +
                        "WHERE Sales.StationaryId = Stationery.Id " +
                        "GROUP BY Stationery.Name " +
                        $"HAVING DATEDIFF(day, MAX(Sales.Date_of_sale), GETDATE()) = {numericUpDown1.Value}");
                    break;
                default:
                    break;
            }
        }
    }
}
