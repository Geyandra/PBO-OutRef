using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tugas_PBO
{
    public partial class Poli : System.Web.UI.Page
    {
        protected void btnInsertion_Click(object sender, EventArgs e)
        {
            if (SqlDBHelper.ExecuteNonQuery("Insert into poli values(@id_poli,@nama_poli,@jumlah_dokter)", CommandType.Text, new NpgsqlParameter("@id_poli", Convert.ToInt32(txtPoliID.Text)), new NpgsqlParameter("@nama_poli", txtNamaPoli.Text), new NpgsqlParameter("@jumlah_dokter", Convert.ToInt32(txtJumlahDokter.Text))))
            {
                txtPoliID.Text = ""; txtNamaPoli.Text = ""; txtJumlahDokter.Text = "";
                GridView1.Visible = false;
                lblmessage2.Text = String.Empty;
                lblmessage.Text = String.Empty;
                lblmsg2.Text = String.Empty;
                lblmsg.Text = "Data Has been Saved";
            }

            else
            {
                txtPoliID.Text = ""; txtNamaPoli.Text = ""; txtJumlahDokter.Text = "";
                GridView1.Visible = false;
                lblmessage2.Text = String.Empty;
                lblmessage.Text = String.Empty;
                lblmsg.Text = String.Empty;
                lblmsg2.Text = "Failed, ID Should Be Unique";
            }

        }
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            DataTable dt;
            if (SqlDBHelper.ExecuteDataSet(out dt, "Select * from poli order by id_poli", CommandType.Text))
            {
                lblmsg.Text = String.Empty;
                lblmsg2.Text = String.Empty;
                lblmessage.Text = String.Empty;
                lblmessage2.Text = String.Empty;
                GridView1.DataSource = dt;
                GridView1.Visible = true;
                GridView1.DataBind();
            }
        }
        protected void btnUpdation_Click(object sender, EventArgs e)
        {
            if (SqlDBHelper.ExecuteNonQuery("update poli set id_poli=@id_poli, nama_poli=@nama_poli, jumlah_dokter=@jumlah_dokter where id_poli=@id_poli", CommandType.Text, new NpgsqlParameter("@id_poli", Convert.ToInt32(txtPoliID.Text)), new NpgsqlParameter("@nama_poli", txtNamaPoli.Text), new NpgsqlParameter("@jumlah_dokter", Convert.ToInt32( txtJumlahDokter.Text))))
            {
                txtNamaPoli.Text = ""; txtPoliID.Text = ""; txtJumlahDokter.Text = "";
                GridView1.Visible = false;
                lblmsg2.Text = String.Empty;
                lblmsg.Text = "Data Has been Updated";
            }
            else
            {
                txtNamaPoli.Text = ""; txtPoliID.Text = ""; txtJumlahDokter.Text = "";
                GridView1.Visible = false;
                lblmsg2.Text = String.Empty;
                lblmsg2.Text = "Failed, ID Should Be Unique";
            }

        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string var;
            var = txtPolID.Text;
            if (var == "")
            {
                txtPolID.Text = "";
                GridView1.Visible = false;
                lblmsg.Text = String.Empty;
                lblmsg2.Text = String.Empty;
                lblmessage.Text = String.Empty;
                lblmessage2.Text = "Please Write the Correct ID";
            }
            else if (SqlDBHelper.ExecuteNonQuery("Delete from poli where id_poli=@id_poli", CommandType.Text, new NpgsqlParameter("@id_poli", Convert.ToInt32(txtPolID.Text))))
            {
                txtPolID.Text = "";
                GridView1.Visible = false;
                lblmsg.Text = String.Empty;
                lblmsg2.Text = String.Empty;
                lblmessage2.Text = String.Empty;
                lblmessage.Text = "Data Has been Deleted";
            }
        }
        public static class SqlDBHelper
        {
            public static bool ExecuteDataSet(out DataTable dt, string sql, CommandType cmdType, params NpgsqlParameter[] parameters)
            {
                using (DataSet ds = new DataSet())
                using (NpgsqlConnection connStr = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString))
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connStr))
                {
                    cmd.CommandType = cmdType;
                    foreach (var item in parameters)
                    {
                        cmd.Parameters.Add(item);
                    }

                    try
                    {
                        cmd.Connection.Open();
                        new NpgsqlDataAdapter(cmd).Fill(ds);
                    }
                    catch (NpgsqlException ex)
                    {
                        dt = null;
                        return false;

                    }


                    dt = ds.Tables[0];
                    return true;
                }
            }
            public static bool ExecuteNonQuery(string sql, CommandType cmdType, params NpgsqlParameter[] parameters)
            {
                using (DataSet ds = new DataSet())
                using (NpgsqlConnection connStr = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString))
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connStr))
                {
                    cmd.CommandType = cmdType;
                    foreach (var item in parameters)
                    {
                        cmd.Parameters.Add(item);
                    }

                    try
                    {

                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (NpgsqlException ex)
                    {

                        return false;

                    }

                    return true;

                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
