using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        String Username;
        String Password;

        protected void Page_Load(object sender, EventArgs e)
        {
            
           Body.Attributes.Add("bgcolor", "blue");
        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            if (Select1.Items.FindByText("Team Member").Value == Select1.SelectedValue ){
                Response.Redirect("~/SignUpTm.aspx");
            }
            else if (Select1.Items.FindByText("Team Leader").Value == Select1.SelectedValue)
            {
                Response.Redirect("~/SignUpTL.aspx");
            }
            else if (Select1.Items.FindByText("Admin").Value == Select1.SelectedValue)
            {
                Response.Redirect("~/SignUpAd.aspx");
            }


        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //INSERT METHOD
            /*MySqlConnection con = new MySqlConnection("server=localhost;user id=root;database=us_states;password=Mysql");
            con.Open();
            string input = "INSERT INTO teammember (userName,userPassword,phoneNumber,address,gender,birthDate) Values('" + txtUsername.Text + "', AES_ENCRYPT('" + txtPsBox.Text + "','.a.'), '1-708-819-5816', '514 S Dodge St', 'Male', '2/19/1994');"; 
            MySqlCommand cmd = new MySqlCommand(input, con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();*/

            //SELECTING USERNAME
            MySqlConnection con = new MySqlConnection("server=localhost;user id=root;database=us_states;password=Mysql");
            con.Open();
            string input = "SELECT userName,AES_DECRYPT(userPassword,'.a.') FROM teammember WHERE userName = '" + txtUsername.Text + "'AND userPassword = AES_ENCRYPT('" + txtPsBox.Text + "','.a.');";
            MySqlCommand cmd = new MySqlCommand(input, con);
            MySqlDataReader dar = cmd.ExecuteReader();
            if (dar.Read())
            {
                
                Username = dar.GetString("userName");
                Password = dar.GetString("AES_DECRYPT(userPassword,'.a.')");
                lbUser.Text = Username + " is now logged";
                
            }
            else
            {
                lbUser.Text = "Not Valid Login and Password";
            }

            con.Close();
            cmd.Dispose();
            dar.Close();
        }

    }
}