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
    public partial class PartEditor : Form {

        conhelp conn; //connection helper class, contains login info etc...

        public PartEditor() {
            InitializeComponent();
            conn = new conhelp(); //Initialize helper object
            partlist = new List<part>();
            taglist = new List<tag>();
            fil = new filter();
            dbRefresh();

        }

        List<part> partlist; //List of all parts
        List<tag> taglist; //List of part tags
        public filter fil;

        //Function to refresh part data from the database
        private void dbRefresh() {
            lstParts.Items.Clear();

            conn.connect();
            try {
                //MessageBox.Show("Connection success!");
                string stm = "SELECT * FROM parts";
                MySqlCommand cmd = new MySqlCommand(stm, conn.conn);
                conn.rdr = cmd.ExecuteReader();
                partlist.Clear();
                clearInput();
                while (conn.rdr.Read()) {
                    part temp = new part();
                    temp.id = conn.rdr.GetInt32(0);
                    temp.name = conn.rdr.GetString(1);
                    temp.msrp = conn.rdr.GetDecimal(2);
                    temp.inv = conn.rdr.GetDecimal(3);
                    temp.wt = conn.rdr.GetDecimal(4);
                    temp.desc = conn.rdr.GetString(5);
                    partlist.Add(temp);
                }
                displist();
            } catch (MySql.Data.MySqlClient.MySqlException ex) {
                MessageBox.Show(ex.Message);
            }
            if (conn != null) {
                conn.Close();
            }
        }

        //Function to refresh tag list from the database 
        private void tagRefresh() {
            lstTags.Items.Clear();
            taglist.Clear();
            try {
                conn.connect();
                string stm = "SELECT * FROM tags";
                MySqlCommand cmd = new MySqlCommand(stm, conn.conn);
                conn.rdr = cmd.ExecuteReader();
                taglist.Clear();
                while (conn.rdr.Read()) {
                    tag temp = new tag();
                    temp.id = conn.rdr.GetString(0);
                    temp.name = conn.rdr.GetString(1);
                    temp.desc = conn.rdr.GetString(2);
                    taglist.Add(temp);
                }
            } catch (MySql.Data.MySqlClient.MySqlException ex) {
                MessageBox.Show(ex.Message);
            }
            if (conn != null) {
                conn.Close();
            }
        }

        //Function to set the displayed tag checkboxes based on the id of the currently selected part
        private void checkTag(string partid){ 
            try {
                //connect();

                    conn.connect();
                    string stm = "SELECT * FROM tags t JOIN parts_tags j on t.id=j.tagid JOIN parts p on j.partid = p.id WHERE p.id=@id";
                    using (MySqlCommand cmd = new MySqlCommand(stm, conn.conn)) {
                        //MessageBox.Show(cmd.CommandText);
                        cmd.Parameters.AddWithValue("@id", partid);
                        cmd.CommandType = CommandType.Text;
                        conn.rdr = cmd.ExecuteReader();
                        while (conn.rdr.Read()) {
                            string id = conn.rdr.GetString(0);
                            foreach (tag item in taglist) {
                                if (id == item.id) {
                                    item.check = true;
                                }
                            }
                        }
                    } // cmd closes itself
                    conn.Close();
            } catch (MySql.Data.MySqlClient.MySqlException ex) {
                MessageBox.Show(ex.Message);
            }

        }

        //Function to update the displayed list of parts
        private void displist() {
            lstParts.Items.Clear();
            foreach (part item in partlist) {
                if (!fil.active) {
                    //If filter not active, add to list
                    lstParts.Items.Add(item.disp());
                } else {
                    //If filter is active, check to see if the ID matches the list of IDs we want to display
                    if (fil.ids.Contains(item.id)) {
                        lstParts.Items.Add(item.disp());
                    }
                }
            }
        }

        //Function to update the displayed list of tags
        private void disptags() {
            foreach (tag item in taglist) {
                lstTags.Items.Add(item.name);
                if (item.check) {
                    lstTags.SetItemChecked(lstTags.Items.Count - 1, true); //Check the most recently added tag, and set its check mark state
                }
            }
        }

        //Function to clear display
        private void clearInput() {
            txtName.Clear();
            txtMSRP.Clear();
            txtInv.Clear();
            txtWT.Clear();
            txtDesc.Clear();
        }

        //Function to update the tags of an item, based on the item id, and the state of the tag checkmarks.
        //type=old(default) if the item is already in the database and may have existing tags, type=new if it isn't
        private void updatetags(string partid, string type="old") {
            for (int i = 0; i < lstTags.Items.Count; i++) {
                //Loop through tags checking states
                if (lstTags.GetItemCheckState(i) == CheckState.Checked && (!taglist[i].check || type=="new")) {
                    //Add tag to table if either new, or state changed and now checked
                    conn.connect();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn.conn;
                    cmd.CommandText = "INSERT INTO parts_tags (partid, tagid) VALUES(@partid,@tagid);";
                    cmd.Parameters.AddWithValue("@partid", partid);
                    cmd.Parameters.AddWithValue("@tagid", taglist[i].id);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                } else if (lstTags.GetItemCheckState(i) == CheckState.Unchecked && taglist[i].check) {
                    //Remove tag from table if state changed and now unchecked
                    conn.connect();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn.conn;
                    cmd.CommandText = "DELETE FROM parts_tags WHERE tagid=@tagid AND partid=@partid";
                    cmd.Parameters.AddWithValue("@partid", partid);
                    cmd.Parameters.AddWithValue("@tagid", taglist[i].id);
                    //MessageBox.Show(cmd.CommandText);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }

            tagRefresh();
            disptags();
            
        }

        private void Form1_Load(object sender, EventArgs e) {
            displist();
            tagRefresh();
            disptags();
        }

        private void btnRefresh_Click(object sender, EventArgs e) {
            dbRefresh();
            tagRefresh();
            disptags();
        }

        private void lstParts_SelectedIndexChanged(object sender, EventArgs e) {
            int index = lstParts.SelectedIndex;
            if (index != -1) {
                txtName.Text = partlist[index].name;
                txtMSRP.Text = partlist[index].msrp.ToString();
                txtInv.Text = partlist[index].inv.ToString();
                txtWT.Text = partlist[index].wt.ToString();
                txtDesc.Text = partlist[index].desc;
                tagRefresh();
                checkTag(partlist[index].id.ToString());
                disptags();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e) {
            if (lstParts.SelectedIndex == partlist.Count) {
                
            } else {
                part temp = partlist[lstParts.SelectedIndex];

                conn.connect();

                try {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn.conn;
                    cmd.CommandText = "UPDATE parts SET name=@name, msrp=@msrp, invoice=@invoice, weight=@weight, description=@desc WHERE id=@id;";
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@msrp", txtMSRP.Text);
                    cmd.Parameters.AddWithValue("@invoice", txtInv.Text);
                    cmd.Parameters.AddWithValue("@weight", txtWT.Text);
                    cmd.Parameters.AddWithValue("@desc", txtDesc.Text);
                    cmd.Parameters.AddWithValue("@id", temp.id.ToString());
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    updatetags(temp.id.ToString());
                    dbRefresh();

                } catch (MySql.Data.MySqlClient.MySqlException ex) {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        
        private void btnAdd_Click(object sender, EventArgs e) {

            conn.connect();

            try {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn.conn;
                cmd.CommandText = "INSERT INTO parts (name, msrp, invoice, weight, description) VALUES(@name, @msrp, @invoice, @weight, @desc);";
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@msrp", txtMSRP.Text);
                cmd.Parameters.AddWithValue("@invoice", txtInv.Text);
                cmd.Parameters.AddWithValue("@weight", txtWT.Text);
                cmd.Parameters.AddWithValue("@desc", txtDesc.Text);
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.connect();
                cmd.Connection = conn.conn;
                cmd.CommandText = "SELECT id FROM parts WHERE id = LAST_INSERT_ID()";
                conn.rdr = cmd.ExecuteReader();
                string id = "";
                while (conn.rdr.Read()) {
                    id = conn.rdr.GetString(0);
                }
                updatetags(id, "new");
                dbRefresh();

            } catch (MySql.Data.MySqlClient.MySqlException ex) {
                MessageBox.Show(ex.Message);
            }
            if (conn != null) {
                conn.Close();
            }
        }

        private void btnClear_Click(object sender, EventArgs e) {
            clearInput();
            lstParts.ClearSelected();
            tagRefresh();
            disptags();
        }
        
    }
}
