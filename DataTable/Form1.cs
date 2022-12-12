using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;

namespace DataTable
{
    public partial class Form1 : Form
    {
        private List<DataT> dataList;

        public Form1()
        {
            InitializeComponent();
            JsonParsing();
            CreatingGrid();
        }

        public void CreatingGrid()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.DataSource = dataList;

            DataGridViewTextBoxColumn column0 = new DataGridViewTextBoxColumn();
            column0.Name = "ZonedTimestamp";
            column0.HeaderText = "Zoned timestamp";
            column0.DataPropertyName = "ZonedTimestamp";
            dataGridView1.Columns.Add(column0);

            DataGridViewTextBoxColumn column1 = new DataGridViewTextBoxColumn();
            column1.Name = "EventDescription";
            column1.HeaderText = "Event description";
            column1.DataPropertyName = "EventDescription";
            dataGridView1.Columns.Add(column1);

            DataGridViewTextBoxColumn column2 = new DataGridViewTextBoxColumn();
            column2.Name = "ChannelId";
            column2.HeaderText = "Channel id";
            column2.DataPropertyName = "ChannelId";
            dataGridView1.Columns.Add(column2);

            DataGridViewTextBoxColumn column3 = new DataGridViewTextBoxColumn();
            column3.Name = "LastName";
            column3.HeaderText = "Last name";
            column3.DataPropertyName = "LastName";
            dataGridView1.Columns.Add(column3);

            DataGridViewTextBoxColumn column4 = new DataGridViewTextBoxColumn();
            column4.Name = "FirstName";
            column4.HeaderText = "First name";
            column4.DataPropertyName = "FirstName";
            dataGridView1.Columns.Add(column4);

            DataGridViewTextBoxColumn column5 = new DataGridViewTextBoxColumn();
            column5.Name = "Patronymic";
            column5.HeaderText = "Patronymic";
            column5.DataPropertyName = "Patronymic";
            dataGridView1.Columns.Add(column5);

            DataGridViewTextBoxColumn column6 = new DataGridViewTextBoxColumn();
            column6.Name = "Age";
            column6.HeaderText = "Age";
            column6.DataPropertyName = "Age";
            dataGridView1.Columns.Add(column6);

            DataGridViewTextBoxColumn column7 = new DataGridViewTextBoxColumn();
            column7.Name = "Gender";
            column7.HeaderText = "Gender";
            column7.DataPropertyName = "Gender";
            dataGridView1.Columns.Add(column7);
        }

        public void JsonParsing()
        {

            WebClient client = new WebClient();
            string strPageCode = client.DownloadString("http://localhost:8080/specialarchiveevents?startTime=30.11.2021+13:50:13&endtime=30.11.2022%2014:07:47&speed=1&channel=Канал1&login=root&eventid=427f1cc3-2c2f-4f50-8865-56ae99c3610d");

            for (int i = 0; i < strPageCode.Length; i++)
            {
                if (strPageCode[i] == '}')
                {
                    strPageCode = strPageCode.Insert(++i, ",");
                }
            }
            strPageCode = strPageCode.Remove(strPageCode.Length - 1);
            strPageCode = "[" + strPageCode + "]";

            dynamic dobj = JsonConvert.DeserializeObject<dynamic>(strPageCode);

            dataList = new List<DataT>();
            for (int i = 0; i < 16; i++)
            {
                DataT dataT = new DataT();

                dataT.ZonedTimestamp = dobj[i]["ZonedTimestamp"].ToString();
                dataT.EventDescription = dobj[i]["EventDescription"].ToString();
                dataT.ChannelId = dobj[i]["ChannelId"].ToString();
                dataT.Age = dobj[i]["Age"].ToString();
                dataT.Gender = dobj[i]["Gender"].ToString();

                if (dobj[i]["lastName"].ToString() == "")
                    dataT.LastName = "No record";
                else
                    dataT.LastName = dobj[i]["lastName"].ToString();

                if (dobj[i]["firstName"].ToString() == "")
                    dataT.FirstName = "No record";
                else
                    dataT.FirstName = dobj[i]["firstName"].ToString();

                if (dobj[i]["patronymic"].ToString() == "")
                    dataT.Patronymic = "No record";
                else
                    dataT.Patronymic = dobj[i]["patronymic"].ToString();

                dataList.Add(dataT);
            }
        }
    }

    struct DataT
    {
        public string ZonedTimestamp
        {
            get;
            set;
        }

        public string EventDescription
        {
            get;
            set;
        }

        public string ChannelId
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }

        public string Patronymic
        {
            get;
            set;
        }

        public string Age
        {
            get;
            set;
        }

        public string Gender
        {
            get;
            set;
        }
    }
}