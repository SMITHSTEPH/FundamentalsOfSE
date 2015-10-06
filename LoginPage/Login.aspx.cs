using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//sqlconncection
using System.Data.SqlClient;
using System.Configuration;



public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString);//connecting registration submission to my database

        //Write query
        conn.Open(); //open the database

        string checkuser = "select count(*) from [UserData] where UserName='" + TBusername.Text + "'";
        SqlCommand com = new SqlCommand(checkuser, conn);
        
        // check if the username exists.
        int temp = Convert.ToInt32(com.ExecuteScalar().ToString());
        if (temp == 1)
        {
            conn.Open();
            //query
            string checkPasswordQuery = "select password from UserData where UserName='" + TBusername.Text + "'";
            SqlCommand passComm = new SqlCommand(checkPasswordQuery, conn);
            string password = passComm.ExecuteScalar().ToString().Replace(" ", "");

            if (password == TBpassword.Text) 
            {


                Response.Write("Password is correct.");
                Session["New"] = TBusername.Text;
                //Response.Redirect();
            }
            else
            {
                Response.Write("Password is NOT correct.");
            }


        }
        else
        {
            Response.Write("User name is NOT correct.");
        }
        conn.Close(); // close the database
    }
}