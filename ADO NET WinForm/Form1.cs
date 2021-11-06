using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ADO_NET_WinForm
{
    public partial class Form1 : Form
    {
        string connectionString;
        SqlConnection connection;
        DataTable data;
        public Form1()
        {
            connectionString = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;
            connection = new SqlConnection(connectionString);
            InitializeComponent();
            FillData("SELECT TypeStationery.Type FROM TypeStationery");
            foreach (DataRow column in data.Rows)
            {
                comboBoxTask8.Items.Add(column.ItemArray[0]);
            }
            FillData("SELECT Managers.Name FROM Managers");
            foreach (DataRow column in data.Rows)
            {
                comboBoxTask9.Items.Add(column.ItemArray[0]);
            }
            FillData("SELECT Customers.Name FROM Customers");
            foreach (DataRow column in data.Rows)
            {
                comboBoxTask10.Items.Add(column.ItemArray[0]);
            }
            if (comboBoxTask8.Items.Count > 0)
            {
                comboBoxTask8.SelectedItem = comboBoxTask8.Items[0];
            }
            if (comboBoxTask9.Items.Count > 0)
            {
                comboBoxTask9.SelectedItem = comboBoxTask9.Items[0];
            }
            if (comboBoxTask10.Items.Count > 0)
            {
                comboBoxTask10.SelectedItem = comboBoxTask10.Items[0];
            }
        }

        private void ShowTable(string query)
        {
            FillData(query);
            dataGridView1.DataSource = data;
        }

        private void FillData(string query)
        {
            data = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            {
                adapter.Fill(data);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                switch ((sender as Button).Text)
                {
                    case "Task1":
                        ShowTable("SELECT Stationery.Name, TypeStationery.Type, Stationery.CostPrice FROM Stationery, TypeStationery WHERE Stationery.TypeStationeryId = TypeStationery.Id");
                        break;
                    case "Task2":
                        ShowTable("SELECT TypeStationery.Type[Type of Stationary] FROM TypeStationery");
                        break;
                    case "Task3":
                        ShowTable("SELECT Managers.Name FROM Managers");
                        break;
                    case "Task4":
                        ShowTable("SELECT Stationery.Name, TypeStationery.Type, Stationery.CostPrice FROM Stationery, TypeStationery " +
                            "WHERE Stationery.TypeStationeryId = TypeStationery.Id " +
                            "AND TypeStationery.Quantity = (SELECT MAX(Quantity) FROM TypeStationery)");
                        break;
                    case "Task5":
                        ShowTable("SELECT Stationery.Name, TypeStationery.Type, Stationery.CostPrice FROM Stationery, TypeStationery " +
                            "WHERE Stationery.TypeStationeryId = TypeStationery.Id " +
                            "AND TypeStationery.Quantity = (SELECT MIN(Quantity) FROM TypeStationery)");
                        break;
                    case "Task6":
                        ShowTable("SELECT Stationery.Name, Stationery.CostPrice FROM Stationery " +
                            "WHERE Stationery.CostPrice = (SELECT MIN(CostPrice) FROM Stationery)");
                        break;
                    case "Task7":
                        ShowTable("SELECT Stationery.Name, Stationery.CostPrice FROM Stationery " +
                            "WHERE Stationery.CostPrice = (SELECT MAX(CostPrice) FROM Stationery)");
                        break;
                    case "Task8":
                        ShowTable("SELECT Stationery.Name, TypeStationery.Type, Stationery.CostPrice FROM Stationery, TypeStationery " +
                            $"WHERE TypeStationery.Type = '{comboBoxTask8.SelectedItem.ToString()}' AND Stationery.TypeStationeryId = TypeStationery.Id");
                        break;
                    case "Task9":
                        ShowTable("SELECT Managers.Name, Stationery.Name, TypeStationery.Type, Sales.Price_of_one, Sales.Quantity, Sales.Date_of_sale FROM Managers, Sales, Stationery, TypeStationery " +
                            $"WHERE Managers.Name = '{comboBoxTask9.SelectedItem.ToString()}' " +
                            "AND Sales.ManagerId = Managers.Id AND Sales.StationaryId = Stationery.Id AND Stationery.TypeStationeryId = TypeStationery.Id");
                        break;
                    case "Task10":
                        ShowTable("SELECT Customers.Name, Stationery.Name, TypeStationery.Type, Sales.Price_of_one, Sales.Quantity, Sales.Date_of_sale FROM Customers, Sales, Stationery, TypeStationery " +
                            $"WHERE Customers.Name = '{comboBoxTask10.SelectedItem.ToString()}' " +
                            "AND Sales.CustomerId = Customers.Id AND Sales.StationaryId = Stationery.Id AND Stationery.TypeStationeryId = TypeStationery.Id");
                        break;
                    case "Task11":
                        ShowTable("SELECT TOP 1 Managers.Name, Customers.Name, Sales.Price_of_one, Sales.Quantity, Stationery.Name, TypeStationery.Type, Sales.Date_of_sale FROM Sales, Managers, Customers, Stationery, TypeStationery " +
                            "WHERE Sales.CustomerId = Customers.Id AND Sales.ManagerId = Managers.Id AND Sales.StationaryId = Stationery.Id AND Stationery.TypeStationeryId = TypeStationery.Id " +
                            "ORDER BY Sales.Date_of_sale DESC");
                        break;
                    case "Task12":
                        ShowTable("SELECT TypeStationery.Type, (AVG(TypeStationery.Quantity) / COUNT(TypeStationery.Type))[Average_amount] FROM TypeStationery, Stationery " +
                            "WHERE Stationery.TypeStationeryId = TypeStationery.Id " +
                            "GROUP BY TypeStationery.Type");
                        break;
                    default:
                        break;
                }
            }
        }




    }
}
