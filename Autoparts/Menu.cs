﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Autoparts {
    public partial class Menu : Form {
        public Menu() {
            InitializeComponent();
        }

        private void btnPartEditor_Click(object sender, EventArgs e) {
            new PartEditor().ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnPartSearch_Click(object sender, EventArgs e) {
            new PartSearch().ShowDialog();
        }
    }
}
