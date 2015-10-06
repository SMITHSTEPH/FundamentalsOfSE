using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//sqlconncection
using System.Data.SqlClient;
using System.Configuration;


public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString);//connecting registration submission to my database

            //Write query
            conn.Open(); //open the database

            string checkuser = "select count(*) from [UserData] where UserName='" + TextBoxUName.Text + "'";
            SqlCommand com = new SqlCommand(checkuser, conn);

            // check if the username exists.
            int temp = Convert.ToInt32(com.ExecuteScalar().ToString());
            if (temp == 1)
            {
                Response.Write("User already Exists.");

            }
            conn.Close(); // close the database

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        try {

            Guid newGUID = Guid.NewGuid(); // create new id




            
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString);//connecting registration submission to my database
        
            //Write query
            conn.Open(); //open the database

            string insertQuery = "insert into [UserData] ([id],[UserName],[Email],[Password],[TeamLeaderCode],[TeamNumber]) values (@id,@Uname,@email,@password,@teamLeaderCode,@teamNo)";
            SqlCommand com = new SqlCommand(insertQuery, conn);

            //execute query
            com.Parameters.AddWithValue("@id", newGUID.ToString());
            com.Parameters.AddWithValue("@Uname", TextBoxUName.Text);
            com.Parameters.AddWithValue("@email", TextBoxEmail.Text);
            com.Parameters.AddWithValue("@password", TextBoxPS.Text);
            com.Parameters.AddWithValue("@teamLeaderCode", TextBoxTLCode.Text);
            com.Parameters.AddWithValue("@teamNo", TextBoxTeamNo.Text);

            com.ExecuteNonQuery();
            Response.Write("Registration is successful! ");
            Response.Redirect("Manager.aspx"); // redirect to another page 
            conn.Close(); // close the database


            }

        catch (Exception ex)
        {
            Response.Write("Error:" + ex.ToString());
        }
        //Response.Write("Registration is successful! ");
    }
}