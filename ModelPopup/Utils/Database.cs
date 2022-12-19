using ModelPopup.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ModelPopup.Utils
{
    public class Database
    {
        SqlConnection cnn;
        public void Connect()
        {
            string connetionString;
            connetionString = @"Data Source=DESKTOP-5JIHN72\SQLEXPRESS;Initial Catalog=testdb;User ID=sa;Password=acer4$";
            cnn = new SqlConnection(connetionString);
        }


        public List<Employee> GetEmployee()
        {
            Connect();
            String query = "select * from employee where isDeleted=0";
            DataTable dt = new DataTable();
            List<Employee> empList = new List<Employee>();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(
            query, cnn);
            cnn.Open();
            try
            {
                adapter.Fill(dt);
                JArray array=ConvertDataTableToJSON(dt);
                for(int i=0; i< array.Count; i++)
                {
                    Employee employee = JsonConvert.DeserializeObject<Employee>(array[i].ToString());
                    empList.Add(employee);
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return empList;
        }

        public Employee GetEmployeeData(String id)
        {
            Connect();
            String query = "select * from employee where and isDeleted=0 and id='" + id+"'";
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            Employee employee = new Employee();
            adapter.SelectCommand = new SqlCommand(
            query, cnn);
            cnn.Open();
            try
            {
                adapter.Fill(dt);
                JArray array =ConvertDataTableToJSON(dt);
                employee = JsonConvert.DeserializeObject<Employee>(array[0].ToString());
                cnn.Close();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return employee;
        }

        public void uploadEmployeeDetail(Employee employee)
        {
            Connect();
            String query = "insert into employee (Name,Email,Phone_No,Address,state,city) values('"+employee.Name+ "','" 
                + employee.Email + "','" + employee.Phone_No + "','" + employee.Address + "','" + employee.state + "','" 
                + employee.city + "')";

            SqlCommand cmd = new SqlCommand(query,cnn);
            cnn.Open();
            int rawAffect=cmd.ExecuteNonQuery();
            cnn.Close();
            if (rawAffect > 0)
            {
                //Inserted
            }else
            {
                //Inserted Fail!
            }
        }

        public void DeleteEmployeeData(String id)
        {
            Connect();
            String query = "update employee set isDeleted=1 where id='"+ id + "'";

            SqlCommand cmd = new SqlCommand(query, cnn);
            cnn.Open();
            int rawAffect = cmd.ExecuteNonQuery();
            cnn.Close();
            if (rawAffect > 0)
            {
                //Inserted
            }
            else
            {
                //Inserted Fail!
            }
        }

        public JArray ConvertDataTableToJSON(DataTable dt)
        {
            JArray array = new JArray();
            for (int i=0;i<dt.Rows.Count;i++)
            {
                JObject object_ = new JObject();
                for(int j = 0; j < dt.Columns.Count; j++)
                {
                    object_.Add(dt.Columns[j].ToString(), dt.Rows[i].ItemArray[j].ToString());
                }
                array.Add(object_);
            }
            return array;
        }
    }
}