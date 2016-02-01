using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using part = Autoparts.SharedClasses.part;
using tag = Autoparts.SharedClasses.tag;
using conhelp = Autoparts.SharedClasses.connectionhelper;
using filter = Autoparts.SharedClasses.filter;

namespace Autoparts {
    public partial class PartSearch : Form {
        public PartSearch() {
            InitializeComponent();
            conn = new conhelp();
            fil = new filter();
        }
        conhelp conn;
        filter fil;
        private void btnSearch_Click(object sender, EventArgs e) {
            conn.connect();
            try {
                string stm = "SELECT * FROM parts WHERE LOWER(name) LIKE @name AND LOWER(description) LIKE @desc AND msrp >= @min AND msrp <= @max";
                MySqlCommand cmd = new MySqlCommand(stm, conn.conn);
                cmd.Parameters.AddWithValue("@name", "%" + txtName.Text.ToLower() + "%");
                cmd.Parameters.AddWithValue("@desc", "%" + txtDesc.Text.ToLower() + "%");
                decimal max;
                if (decimal.TryParse(txtMax.Text, out max)) {
                    if (max <= 0)
                        max = 999999999;
                } else {
                    max = 999999999;
                }
                cmd.Parameters.AddWithValue("@max", max.ToString()); 
                cmd.Parameters.AddWithValue("@min", txtMin.Text);
                
                conn.rdr = cmd.ExecuteReader();
                fil.active = true;
                fil.ids.Clear();
                lstResult.Items.Clear();
                while (conn.rdr.Read()) {
                    int id = conn.rdr.GetInt32(0);
                    part temp = new part();
                    temp.id = id;
                    temp.name = conn.rdr.GetString(1);
                    temp.msrp = conn.rdr.GetDecimal(2);
                    temp.inv = conn.rdr.GetDecimal(3);
                    temp.wt = conn.rdr.GetDecimal(4);
                    temp.desc = conn.rdr.GetString(5);

                    fil.ids.Add(id);
                    lstResult.Items.Add(temp.disp());
                }
                conn.Close();

            } catch (Exception ex){
                MessageBox.Show(ex.Message);
            }

        }

        private void btnEdit_Click(object sender, EventArgs e) {
            PartEditor n = new PartEditor();
            n.fil = fil;
            
            n.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e) {
            txtName.Clear();
            txtDesc.Clear();
            txtMax.Clear();
            txtMin.Clear();
            lstResult.Items.Clear();
        }
    }
}
