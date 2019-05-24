using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication1.pastpaperRepo
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string gone = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\lms.mdf;Integrated Security=True  ";

        protected void Page_Load(object sender, EventArgs e)
        {
            filldata();
        }

        protected void OpenDocument(object sender, EventArgs e)
        {
            LinkButton Ink = (LinkButton)sender;
            GridViewRow gr = (GridViewRow)Ink.NamingContainer;
            int id = int.Parse(GridView1.DataKeys[gr.RowIndex].Value.ToString());
            Download(id);
        }

        private void Download(int id)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(gone))
            {
                SqlCommand cmd = new SqlCommand("getPastpaper", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
            }
            string name = dt.Rows[0]["File_Name"].ToString();
            byte[] documentBytes = (byte[])dt.Rows[0]["File_content"];
            Response.ClearContent();
            Response.ContentType = "application/octetstream";
            Response.AddHeader("content-Disposition", string.Format("attachment; File_Name={0}", name));
            Response.AddHeader("content-Length", documentBytes.Length.ToString());
            Response.BinaryWrite(documentBytes);
            Response.Flush();
            Response.Close();
        }

        private void filldata()
        {
            DataTable dt = new DataTable();
            string mia = "select Id, File_Name, File_Content from [pastpapers]";
            using (SqlConnection cn = new SqlConnection(gone))
            {
                SqlCommand cmd = new SqlCommand(mia, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
            }
            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            FileInfo f1 = new FileInfo(FileUpload1.FileName);
            byte[] documentContent = FileUpload1.FileBytes;
            String name = f1.Name;

            using (SqlConnection cn = new SqlConnection(gone))
            {
                SqlCommand cmd = new SqlCommand("savePastpaper", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@filename", SqlDbType.VarChar).Value = name;
                cmd.Parameters.Add("@filecontent", SqlDbType.VarBinary).Value = documentContent;
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }

        

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(gone))
            {
                cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from pastpapers where File_Name='" + TextBox1.Text + "'";
                cmd.ExecuteNonQuery();
                cn.Close();

            }
        }

        //protected void Button3_Click(object sender, EventArgs e)
        //{
        //    using (SqlConnection cn = new SqlConnection(gone))
        //    {
        //        cn.Open();
        //        SqlCommand cmd = cn.CreateCommand();
        //        cmd.CommandType = CommandType.Text;
        //        cmd.CommandText = "select * from table where File_Name='" + TextBox2.Text + "'";
        //        cmd.ExecuteNonQuery();
        //        DataTable dt = new DataTable();
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        da.Fill(dt);
        //        GridView1.DataSource = dt;
        //        cn.Close();
        //    }
        //}

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}