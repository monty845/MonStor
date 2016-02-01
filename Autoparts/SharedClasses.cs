using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace Autoparts {
    public class SharedClasses {
        public class part {
            public int id;
            public string name;
            public decimal msrp;
            public decimal inv;
            public decimal wt;
            public string desc;

            //Return formatted string containing all part info
            public string disp() {
                return name + " | " + msrp.ToString() + " | " + inv.ToString() + " | " + wt.ToString() + " | " + desc;
            }
        }
        public class tag {
            public string id;
            public string name;
            public string desc;
            public bool check;
            public tag() {
                check = false; //Default tag initialization = false
            }
        }
        public class connectionhelper {
            public connectionhelper() {
                myConnectionString = "server=ec2-52-34-46-27.us-west-2.compute.amazonaws.com;uid=appaccess;" +
                "pwd=lLlL1234;database=autoparts;";
                conn = null;
                rdr = null;
            }
            public void connect() {
                try {
                    conn = new MySql.Data.MySqlClient.MySqlConnection();
                    conn.ConnectionString = myConnectionString;
                    conn.Open();
                } catch (MySql.Data.MySqlClient.MySqlException ex) {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }
            public void Close() {
                if (conn != null) {
                    conn.Close();
                }
            }
            private string myConnectionString;
            public MySqlConnection conn;
            public MySqlDataReader rdr;
        }
        public class filter{
            public bool active;
            public List<int> ids;
            public filter() {
                active = false;
                ids = new List<int>();
            }
        }
    }
}
